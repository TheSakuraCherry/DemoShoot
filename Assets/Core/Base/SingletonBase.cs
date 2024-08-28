using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : Component
{
    protected static T instance;
    
    public static  bool HasInstance => instance != null;
    
    public float InitializationTime { get; private set; }
    
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    var go = new GameObject(typeof(T).Name+" Auto-Generated");
                  
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if(!Application.isPlaying) return;
        InitializationTime = Time.time;
        DontDestroyOnLoad(gameObject);
        
        T[] oldInstances = FindObjectsOfType<T>();
        foreach (T old in oldInstances)
        {
            if(old.GetComponent<SingletonBase<T>>().InitializationTime < InitializationTime)
            {
                Destroy(old.gameObject);
            }
        }
        if(instance == null)
        {
            instance = this as T;
        }
    }
}
