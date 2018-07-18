using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Firework : Level
{
    public Vector2 zoneCenterPoint;
    public Vector2 zoneSize;
    public Vector2 launchDirection;

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

            int direction;
            direction = Random.Range(0, 2) > 0 ? 1 : -1;

            float generatePosX = Random.Range(zoneCenterPoint.x - zoneSize.x / 2,
                                                zoneCenterPoint.x + zoneSize.x / 2) * direction;
            float generatePosY = Random.Range(zoneCenterPoint.y - zoneSize.y / 2,
                                                zoneCenterPoint.y + zoneSize.y / 2);

            Vector2 launchDir = launchDirection;
            launchDir.x *= direction;

            LevelGenerator.Instance().InstantiateObject("Cube_Small", new Vector2(generatePosX, generatePosY), launchDir);

            yield return new WaitForSeconds(interval);
        }

        LevelFinish();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //生成区域
        Gizmos.DrawCube(zoneCenterPoint, zoneSize);
        Gizmos.DrawCube(ReversedVectorX(zoneCenterPoint), zoneSize);
        Gizmos.DrawLine(zoneCenterPoint, zoneCenterPoint + launchDirection);
        Gizmos.DrawLine(ReversedVectorX(zoneCenterPoint),
                        ReversedVectorX(zoneCenterPoint) +
                        ReversedVectorX(launchDirection));
    }

    //翻转向量的X值
    Vector2 ReversedVectorX(Vector2 _vector)
    {
        return new Vector2(-_vector.x, _vector.y);
    }
}
