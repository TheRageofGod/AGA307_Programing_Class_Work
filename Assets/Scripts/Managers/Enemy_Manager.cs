using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    OneHand,
    TwoHand,
    Archer
}

public enum PatrolType { Linear, Random, Loop};
public class Enemy_Manager : GameBehaviour<Enemy_Manager>
{
    public string[] enemyNames;
    public Transform[] spawnPoints;
    public GameObject[] enemyTypes;

    public List<GameObject> enemies;
   
public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }


    /// <summary>
    /// kills a specific enemy
    /// </summary>
    /// <param name="_condition"></param>
    void KillSpecific(string _condition) // broken wont destroy all of them at once
    {
        int eCount = enemies.Count;
        for (int i = 0; i< eCount; i++)
        {
            if (enemies[i].name.Contains(_condition) )
            {
                EnemyDestroy(enemies[i]);
            }
        }
    }

    /// <summary>
    /// Spawns random enemy Types in scene at spawnPoints
    /// </summary>
    void SpawnEnemy()
    {
       
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject go = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemies.Add(go);
        }

    }

/// <summary>
/// Destroys an enemy based on the GameObject Passed Through
/// </summary>
/// <param name="_enemy"></param>
   public void EnemyDestroy(GameObject _enemy)
    {
        if (enemies.Count == 0)
            return;
        Destroy(_enemy);
        enemies.Remove(_enemy);
    }


    /// <summary>
    /// Kills all enemies in scene
    /// </summary>
    void KillAllEnemies()
    {
        int eCount = enemies.Count;
        for (int i =0; i< eCount; i++)
        {
            EnemyDestroy(enemies[0]);
        }
    }

    IEnumerator EnemyDelaySpawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject go = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemies.Add(go);
            yield return new WaitForSeconds(2);
        }
    }
    void OnEnemyDied(Enemy _enemy)
    {
        EnemyDestroy(_enemy.gameObject);
    }
    void OnGameStateChange(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.Playing:
                StartCoroutine(EnemyDelaySpawn());
                break;
            case GameState.Paused:
            case GameState.GameOver:
                StopCoroutine(EnemyDelaySpawn());
                break;

        }
    }
    private void OnEnable()
    {  
        GameEvents.OnEnemyDied += OnEnemyDied;
        GameEvents.OnGameStateChange += OnGameStateChange;
    }
    private void OnDisable()
    {
        GameEvents.OnEnemyDied += OnEnemyDied;
        GameEvents.OnGameStateChange -= OnGameStateChange;
    }

}
