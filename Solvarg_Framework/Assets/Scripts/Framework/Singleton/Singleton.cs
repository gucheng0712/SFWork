﻿using System;
using UnityEngine;

public abstract class Singleton<T>: IMonoBehaviour where T : class,new()
{
    protected static T _instance = null;
    protected SingletonManager singletonManager;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    public T InitSingleton(SingletonManager singletonManager)
    {
        this.singletonManager = singletonManager;
        singletonManager.SetToSingletonList(this);
        return Instance;
    }

    protected Singleton()
    {
        if (_instance != null)
        {
            throw new System.Exception("This " + (typeof(T)).ToString() + " Singleton Instance is not null !!!");
        }
    }

    public virtual void Awake() { }
    public virtual void OnRelease() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }
    public virtual void OnGUI() { }
    public virtual void OnDisable() { }
    public virtual void OnDestroy() { }
}

