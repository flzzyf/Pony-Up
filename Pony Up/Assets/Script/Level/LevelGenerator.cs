using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGenerator : Singleton<LevelGenerator>
{
    public GameObject cube;
    public Vector2 generatePos = new Vector2(0, 6);

    [Header("关卡物体")]
    public LevelObject[] levelObjects;
    Dictionary<string, LevelObject> levelObjectDictionary = new Dictionary<string, LevelObject>();


    [System.Serializable]
    public class LevelObject
    {
        public string name;
        public GameObject prefab;
    }

    void Start()
    {
        foreach (LevelObject item in levelObjects)
        {
            levelObjectDictionary[item.name] = item;
        }
    }

    //生成物体并指向移动方向
    public GameObject InstantiateObject(string _name, Vector2 _pos)
    {
        LevelObject obj = levelObjectDictionary[_name];

        GameObject go = Instantiate(obj.prefab, _pos, Quaternion.identity, ParentManager.Instance().GetParent("LevelObject"));

        return go;
    }

    public void InstantiateObject(string _name, Vector2 _pos, Vector2 _force = default(Vector2), float _torque = 0)
    {
        GameObject go = InstantiateObject(_name, _pos);
        if (_force != default(Vector2))
        {
            go.GetComponent<Rigidbody2D>().velocity = _force;
        }

        if(_torque != 0)
        {
            go.GetComponent<Rigidbody2D>().AddTorque(_torque);
        }

    }
}


