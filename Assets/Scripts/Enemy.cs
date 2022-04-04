using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehaviour
{
    public EnemyTypes myType;
    public int health;
    int baseHealth = 100;
    public float mySpeed;
    float baseSpeed = 2;

    [Header("AI")]
    public PatrolType myPatrol;
    int patrolPoint = 0;
    bool reverse = false;
    Transform startPos;
    Transform endPos;
    public Transform moveToPos;

    Animator anim;
    bool isDead = false;


    void Start()
    {
        SetUpAI();
        SetupEnemy();
        StartCoroutine(Move());
        anim = GetComponent<Animator>();
        

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit(50);
        }
    }

    void SetUpAI()
    {
        startPos = transform;
        endPos = _EM.GetRandomSpawnPoint();
        moveToPos = endPos;
    }
    void SetupEnemy()
    {
        switch (myType)
        {
            case EnemyTypes.OneHand:
                health = baseHealth;
                mySpeed = baseSpeed;
                myPatrol = PatrolType.Linear;
                break;
            case EnemyTypes.TwoHand:
                health = baseHealth * 2;
                mySpeed = baseSpeed / 2;
                myPatrol = PatrolType.Loop;
                break;
            case EnemyTypes.Archer:
                health = baseHealth / 2;
                mySpeed = baseSpeed * 2;
                myPatrol = PatrolType.Random;
                break;
            default:
                health = baseHealth;
                mySpeed = baseSpeed;
                myPatrol = PatrolType.Random;
                break;
        }
    }

    IEnumerator Move()
    {
        switch (myPatrol)
        {
            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpawnPoint();
                break;
            case PatrolType.Linear:
                moveToPos = _EM.spawnPoints[patrolPoint];
                patrolPoint = patrolPoint != _EM.spawnPoints.Length ? patrolPoint + 1 : 0;
                break;
            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse;
                break;
        }

        while (Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * mySpeed);
            transform.rotation = Quaternion.LookRotation(moveToPos.position);
            yield return null;
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(Move());
    }


    public void Hit(int _damage)
    {
    
        health -= _damage;
        if (health <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                int randomNumber = Random.Range(1, 3);
                StopAllCoroutines();
                anim.SetTrigger("Die " + randomNumber);
                GameEvents.ReportEnemyDied(this);
            }
        }
        else
        {
            int randomNumber = Random.Range(1, 4);
            anim.SetTrigger("Hit " + randomNumber);
            GameEvents.ReportEnemyHit(this);
        }
            
    }

    void Attack()
    {
        if (isDead)
            return;
        int randomNumber = Random.Range(1, 4);
        anim.SetTrigger("Attack " + randomNumber);
    }


}
     
    

