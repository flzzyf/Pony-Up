using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public int firstLevel = 0;
    int lastLevelIndex = 0;

    public float levelInterval = 5;
    public float levelStartWaiting = 2;

    public GameObject text_level;

    [Header("关卡")]
    public List<LevelSelector> levelList = new List<LevelSelector>();

    public void StartLevel()
    {
        int levelIndex = GetNextLevel();

        text_level.SetActive(false);
        text_level.SetActive(true);
        text_level.GetComponent<Text>().text = levelList[levelIndex].displayName;

        StartCoroutine(StartLevelWaiting(levelIndex));
    }

    IEnumerator StartLevelWaiting(int _index)
    {
        yield return new WaitForSeconds(levelStartWaiting);

        levelList[_index].level.StartLevel();

    }

    public void ClearLevel()
    {
        ParentManager.Instance().ClearChilds("LevelObject");
    }

    public void LevelFinish()
    {
        GameManager.Instance().LevelFinish();

        StartCoroutine(LevelFinishWaiting());
    }

    int GetNextLevel()
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

        return levelIndex;
    }

    IEnumerator LevelFinishWaiting()
    {
        yield return new WaitForSeconds(levelInterval);

        StartLevel();
    }

}



