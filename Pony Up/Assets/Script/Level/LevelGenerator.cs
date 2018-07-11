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

    void Start()
    {
        objectParent = new GameObject("Parent_Object").transform;

        //添加关卡后从这里加入关卡数列
        levelList.Add(new Cubes());
        levelList.Add(new ShootingStar());
    }

    public void StartLevel()
    {
        int levelIndex = Random.Range(0, levelList.Count - 1);
        levelIndex = 1;
        levelList[levelIndex].StartLevel();
    }

    public void ClearLevel()
    {
        ClearChildObject(objectParent);
    }

    void ClearChildObject(Transform _parent)
    {
        while (_parent.childCount > 0)
        {
            Destroy(_parent.GetChild(0).gameObject);
            _parent.GetChild(0).SetParent(null);
        }
    }

    public void InstantiateObject(objectType _type, Vector2 _pos)
    {
        GameObject go = cube;
        if (_type == objectType.Cube)
        {
            go = cube;
        }
        Instantiate(go, generatePos, Quaternion.identity, objectParent);
    }
}

public abstract class Level
{
    public abstract void StartLevel();
}

public class Cubes : Level
{
    public override void StartLevel()
    {
        zyf.Out("方块关卡");
        GameManager.Instance().IEnumeratorTrigger(LevelStart());
    }

    IEnumerator LevelStart()
    {
        int a = 10;
        while (GameManager.Instance().gaming && a > 0)
        {
            a--;
            LevelGenerator.Instance().InstantiateObject(
                LevelGenerator.objectType.Cube, LevelGenerator.Instance().generatePos);

            yield return new WaitForSeconds(1f);
        }
    }
}

public class ShootingStar : Level
{
    public override void StartLevel()
    {
        zyf.Out("流星关卡");
        GameManager.Instance().IEnumeratorTrigger(LevelStart());
    }

    IEnumerator LevelStart()
    {
        int a = 10;
        while (GameManager.Instance().gaming && a > 0)
        {
            a--;
            LevelGenerator.Instance().InstantiateObject(
                LevelGenerator.objectType.Cube, LevelGenerator.Instance().generatePos);

            yield return new WaitForSeconds(1f);
        }
    }
}
