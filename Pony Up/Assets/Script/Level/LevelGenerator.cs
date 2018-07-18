using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGenerator : Singleton<LevelGenerator>
{
    public GameObject cube;
    public Vector2 generatePos = new Vector2(0, 6);

    [Header("关卡")]
    public List<LevelSelector> levelList = new List<LevelSelector>();

    int lastLevelIndex = 0;
    public int firstLevel = 0;

    [Header("关卡物体")]
    public LevelObject[] levelObjects;
    Dictionary<string, LevelObject> levelObjectDictionary = new Dictionary<string, LevelObject>();

    [System.Serializable]
    public class LevelSelector
    {
        public string name;
        [SerializeField]
        public Level level;
    }

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

    public void StartLevel()
    {
        
        int levelIndex = UnityEngine.Random.Range(0, levelList.Count);
        if (levelIndex == lastLevelIndex)
        {
            levelIndex++;
            levelIndex %= levelList.Count;
        }

        if (firstLevel != -1)
        {
            levelIndex = firstLevel;
            firstLevel = -1;
        }

        lastLevelIndex = levelIndex;

        levelList[levelIndex].level.StartLevel();
    }

    public void ClearLevel()
    {
        ParentManager.Instance().ClearChilds("LevelObject");

    }

    public void LevelFinish()
    {
        StartLevel();
    }

    void ClearChildObject(Transform _parent)
    {
        while (_parent.childCount > 0)
        {
            Destroy(_parent.GetChild(0).gameObject);
            _parent.GetChild(0).SetParent(null);
        }
    }
    //生成物体并指向移动方向
    public void InstantiateObject(string _name, Vector2 _pos, Vector2 _force = default(Vector2))
    {
        LevelObject obj = levelObjectDictionary[_name];

        GameObject go = Instantiate(obj.prefab, _pos, Quaternion.identity, ParentManager.Instance().GetParent("LevelObject"));
        if (_force != default(Vector2))
        {
            go.GetComponent<Rigidbody2D>().velocity = _force;
        }
    }
}


