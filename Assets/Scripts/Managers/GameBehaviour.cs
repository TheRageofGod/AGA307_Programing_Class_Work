using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    protected static GameManager _GM { get { return GameManager.INSTANCE; } }
    protected static Enemy_Manager _EM { get { return Enemy_Manager.INSTANCE; } }
    protected static Ui_Manager _UI { get { return Ui_Manager.INSTANCE; } }
}



public class GameBehaviour<T> : GameBehaviour where T : GameBehaviour
{
    private static T instance_;

    public static T INSTANCE
    {
        get
        {
            if(instance_ == null)
            {
                instance_ = FindObjectOfType<T>();
                if(instance_ == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    singleton.AddComponent<T>();
                }
            }
            return instance_;
        }
    }

    protected virtual void Awake()
    {
        if (instance_ == null)
            instance_ = this as T;
        else
            Destroy(gameObject);
    }
}
