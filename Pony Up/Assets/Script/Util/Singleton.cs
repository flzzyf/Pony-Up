using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static T instance;

    public static T Instance()
    {
        if (instance == null)
        {
            //寻找现有脚本
            instance = FindObjectOfType<T>();

            if (FindObjectsOfType<T>().Length > 1)
            {
                throw new Exception("超过一个!");
                return instance;
            }
            //没有现有脚本
            if (instance == null)
            {
                string instanceName = typeof(T).Name;
                throw new Exception("Instance Name: " + instanceName);

                GameObject instanceGO = GameObject.Find(instanceName);

                if (instanceGO == null)
                    instanceGO = new GameObject(instanceName);
                instance = instanceGO.AddComponent<T>();
                DontDestroyOnLoad(instanceGO);  //保证实例不会被释放
                throw new Exception("Add New Singleton " + instance.name + " in Game!");
            }
        }

        return instance;
    }

    protected virtual void OnDestroy()
    {
        instance = null;
    }
}
