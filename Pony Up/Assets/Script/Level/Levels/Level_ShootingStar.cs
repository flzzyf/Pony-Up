using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_ShootingStar : Level
{
    public Vector2 shootingStarCenterPoint;
    public Vector2 shootingStarSize;
    public Vector2 shootingStarDirection;

    public int count = 15;
    public float interval = 0.5f;
    public float torque = 3;

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
            float generatePosX = Random.Range(shootingStarCenterPoint.x - shootingStarSize.x / 2,
                                                shootingStarCenterPoint.x + shootingStarSize.x / 2);
            float generatePosY = Random.Range(shootingStarCenterPoint.y - shootingStarSize.y / 2,
                                                shootingStarCenterPoint.y + shootingStarSize.y / 2);

            LevelGenerator.Instance().InstantiateObject("Star", new Vector2(generatePosX, generatePosY), shootingStarDirection, torque);

            yield return new WaitForSeconds(interval);
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
