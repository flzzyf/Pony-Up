using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Bucketball : Level
{
    public Vector2 shootingStarCenterPoint;
    public Vector2 shootingStarSize;
    public Vector2 shootingStarDirection;

    public override void StartLevel()
    {
        zyf.Out("篮球关卡");
        GameManager.Instance().IEnumeratorTrigger(LevelStart());
    }

    IEnumerator LevelStart()
    {
        LevelGenerator.Instance().InstantiateObject("Bucket", new Vector2(0, zyf.GetWorldScreenSize().y /2 + 1));

        yield return new WaitForSeconds(2f);

        int a = 30;
        while (GameManager.Instance().gaming && a > 0)
        {
            a--;

            int direction;
            direction = Random.Range(0, 2) > 0 ? 1 : -1;

            float generatePosX = Random.Range(shootingStarCenterPoint.x - shootingStarSize.x / 2,
                                                shootingStarCenterPoint.x + shootingStarSize.x / 2) * direction;
            float generatePosY = Random.Range(shootingStarCenterPoint.y - shootingStarSize.y / 2,
                                                shootingStarCenterPoint.y + shootingStarSize.y / 2);

            Vector2 launchDirection = shootingStarDirection;
            launchDirection.x *= direction;

            LevelGenerator.Instance().InstantiateObject("Cube_Small", new Vector2(generatePosX, generatePosY), 
                                                        launchDirection);

            yield return new WaitForSeconds(0.3f);
        }

        LevelFinish();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //流星生成区域
        Gizmos.DrawCube(shootingStarCenterPoint, shootingStarSize);
        Gizmos.DrawCube(ReversedVectorX(shootingStarCenterPoint), shootingStarSize);
        Gizmos.DrawLine(shootingStarCenterPoint, shootingStarCenterPoint + shootingStarDirection);
        Gizmos.DrawLine(ReversedVectorX(shootingStarCenterPoint),
                        ReversedVectorX(shootingStarCenterPoint) +
                        ReversedVectorX(shootingStarDirection));
    }

    //翻转向量的X值
    Vector2 ReversedVectorX(Vector2 _vector)
    {
        return new Vector2(-_vector.x, _vector.y);
    }
}
