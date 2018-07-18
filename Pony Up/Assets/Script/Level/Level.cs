using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public virtual void StartLevel() { }

    public void LevelFinish()
    {
        if (GameManager.Instance().gaming)
            LevelGenerator.Instance().LevelFinish();
    }
}