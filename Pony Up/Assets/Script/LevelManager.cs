using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    LevelGenerator levelGenerator;

	void Start () 
	{
        levelGenerator = GetComponent<LevelGenerator>();
	}
	
	void Update () 
	{
		
	}

    public void StartLevel()
    {
        levelGenerator.StartLevel();

    }

    public void ClearLevel()
    {
        while(levelGenerator.objectParent.childCount > 0)
        {
            Destroy(levelGenerator.objectParent.GetChild(0).gameObject);
            levelGenerator.objectParent.GetChild(0).SetParent(null);
        }
    }
}
