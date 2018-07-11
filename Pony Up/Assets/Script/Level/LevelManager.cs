using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Vector2 shootingStarCenterPoint;
    public Vector2 shootingStarSize;
    public Vector2 shootingStarDirection;

    public void StartLevel()
    {
        LevelGenerator.Instance().StartLevel();
    }

    public void ClearLevel()
    {
        LevelGenerator.Instance().ClearLevel();
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

public abstract class Level
{
    public abstract void StartLevel();

    public void LevelFinish()
    {
        if (GameManager.Instance().gaming)
            LevelGenerator.Instance().LevelFinish();
    }
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

        LevelFinish();
    }
}

public class ShootingStar : Level
{
    public Vector2 shootingStarCenterPoint;
    public Vector2 shootingStarSize;
    public Vector2 shootingStarDirection;

    public ShootingStar()
    {
        shootingStarCenterPoint = LevelManager.Instance().shootingStarCenterPoint;
        shootingStarSize = LevelManager.Instance().shootingStarSize;
        shootingStarDirection = LevelManager.Instance().shootingStarDirection;
    }

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
            float generatePosX = Random.Range(shootingStarCenterPoint.x - shootingStarSize.x / 2,
                                                shootingStarCenterPoint.x + shootingStarSize.x / 2);
            float generatePosY = Random.Range(shootingStarCenterPoint.y - shootingStarSize.y / 2,
                                                shootingStarCenterPoint.y + shootingStarSize.y / 2);
            LevelGenerator.Instance().InstantiateObject(
                LevelGenerator.objectType.Cube, new Vector2(generatePosX, generatePosY),
                                shootingStarDirection);

            yield return new WaitForSeconds(1f);
        }

        LevelFinish();
    }
}
