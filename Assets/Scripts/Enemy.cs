using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyTypes myType;
    public int health;
    void Start()
    {
        SetUp();

    }

    void SetUp()
    {
        switch (myType)
        {
            case EnemyTypes.Archer:
            health = 50;
                break;
            case EnemyTypes.OneHand:
            health = 100;
                break;
            case EnemyTypes.TwoHand:
            health = 200;
                break;
        }
    }

}
     
    

