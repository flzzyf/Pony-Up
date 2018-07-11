using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public void StartLevel()
    {
        LevelGenerator.Instance().StartLevel();
    }

    public void ClearLevel()
    {
        LevelGenerator.Instance().ClearLevel();

    }
}
