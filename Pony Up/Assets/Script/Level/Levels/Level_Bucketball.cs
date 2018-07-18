using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Bucketball : Level
{
    public Vector2 shootingStarCenterPoint;
    public Vector2 shootingStarSize;

    public int count = 15;
    public float interval = 0.5f;

    public override void StartLevel()
    {
        GameManager.Instance().IEnumeratorTrigger(LevelStart());
    }

    IEnumerator LevelStart()
    {
        LevelGenerator.Instance().InstantiateObject("Bucket", new Vector2(0, zyf.GetWorldScreenSize().y /2 + 1));

        yield return new WaitForSeconds(2f);

        int a = count;
        while (GameManager.Instance().gaming && a > 0)
        {
            a--;

            float generatePosX = Random.Range(shootingStarCenterPoint.x - shootingStarSize.x / 2,
                                                shootingStarCenterPoint.x + shootingStarSize.x / 2);
            float generatePosY = Random.Range(shootingStarCenterPoint.y - shootingStarSize.y / 2,
                                                shootingStarCenterPoint.y + shootingStarSize.y / 2);

            LevelGenerator.Instance().InstantiateObject("Rain", new Vector2(generatePosX, generatePosY));

            yield return new WaitForSeconds(interval);
        }

        LevelFinish();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //生成区域
        Gizmos.DrawCube(shootingStarCenterPoint, shootingStarSize);
    }

}
