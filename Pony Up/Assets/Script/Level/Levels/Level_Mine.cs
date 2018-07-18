using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Mine : Level
{
    public Vector2 zoneCenterPoint;
    public Vector2 zoneSize;

    public int count = 15;
    public float interval = 0.5f;

    public override void StartLevel()
    {
        GameManager.Instance().IEnumeratorTrigger(LevelStart());
    }

    IEnumerator LevelStart()
    {
        int a = count;
        while (GameManager.Instance().gaming && a > 0)
        {
            a--;

            float generatePosX = Random.Range(zoneCenterPoint.x - zoneSize.x / 2,
                                                zoneCenterPoint.x + zoneSize.x / 2);
            float generatePosY = Random.Range(zoneCenterPoint.y - zoneSize.y / 2,
                                                zoneCenterPoint.y + zoneSize.y / 2);

            LevelGenerator.Instance().InstantiateObject("Mine", new Vector2(generatePosX, generatePosY));

            yield return new WaitForSeconds(interval);
        }

        LevelFinish();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //生成区域
        Gizmos.DrawCube(zoneCenterPoint, zoneSize);
    }
}
