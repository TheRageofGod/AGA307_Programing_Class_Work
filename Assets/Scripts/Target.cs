using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    float moveDistance = 500f;

    void Start()
    {
        //StartCoroutine(Move());
        StartCoroutine(moveRandom());
    }

    IEnumerator Move()
    {
       
        for (int i = 0; i< moveDistance; i++)
      {
            transform.Translate(Vector3.right * Time.deltaTime);
            yield return null;
      }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < moveDistance; i++)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
           yield return null;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(Move());
    }
    IEnumerator moveRandom()
    {
        Vector3 direction = new Vector3();
        int rnd = Random.Range(0, 5);
   
        if (rnd == 0)
            direction = Vector3.back;
        if (rnd == 1)
            direction = Vector3.forward;
        if (rnd == 2)
            direction = Vector3.down;
        if (rnd == 3)
            direction = Vector3.right;
        if (rnd == 4)
            direction = Vector3.up;
        if (rnd == 5)
            direction = Vector3.left;

        for (int i = 0; i < moveDistance; i++)
        {
            transform.Translate(direction * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2);

        StartCoroutine(moveRandom());
    }
}


