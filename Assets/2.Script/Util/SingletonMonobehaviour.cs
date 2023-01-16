using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                    instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        var t_objs = FindObjectsOfType<T>();

        if (t_objs.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
