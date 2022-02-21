using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerDetected(other))
            print("Entered");
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (PlayerDetected(other))
            print("stayed");
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlayerDetected(other))
            print("exited");
    }

    bool PlayerDetected(Collider _other)
    {
        return _other.CompareTag("Player");
    }
}

