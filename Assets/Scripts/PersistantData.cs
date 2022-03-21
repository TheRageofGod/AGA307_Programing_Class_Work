using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : GameBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
