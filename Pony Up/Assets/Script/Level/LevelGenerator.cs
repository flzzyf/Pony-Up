using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : Singleton<LevelGenerator>
{
    public GameObject cube;
    public Vector2 generatePos = new Vector2(0, 6);

    Transform objectParent;

    List<Level> levelList = new List<Level>();

    public enum objectType { Cube, Star };

    int lastLevelIndex = 0;

    void Start()
    {
        objectParent = new GameObject("Parent_Object").transform;

        //添加关卡后从这里加入关卡数列
        levelList.Add(new Cubes());
        levelList.Add(new ShootingStar());
    }

    public void StartLevel()
    {
        int levelIndex = Random.Range(0, levelList.Count);
        if (levelIndex == lastLevelIndex)
        {
            levelIndex++;
            levelIndex %= levelList.Count;
        }
        lastLevelIndex = levelIndex;
        // levelIndex = 1;
        levelList[levelIndex].StartLevel();
    }

    public void ClearLevel()
    {
        ClearChildObject(objectParent);
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
    public void InstantiateObject(objectType _type, Vector2 _pos, Vector2 _force = default(Vector2))
    {
        GameObject go = cube;
        if (_type == objectType.Cube)
        {
            go = cube;
        }
        GameObject obj = Instantiate(go, _pos, Quaternion.identity, objectParent);
        if (_force != default(Vector2))
        {
            // obj.GetComponent<Rigidbody2D>().gravityScale = 0;
            obj.GetComponent<Rigidbody2D>().velocity = _force;
        }
    }
}


