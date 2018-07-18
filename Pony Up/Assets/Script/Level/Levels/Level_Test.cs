using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Test : Level
{
    public override void StartLevel()
    {
        zyf.Out("测试关卡");
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
