using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour 
{
    public GameObject cube;
    public Vector2 generatePos = new Vector2(0, 6);
    [HideInInspector]
    public Transform objectParent;

	private void Start()
	{
        objectParent = new GameObject("objectParent").transform;
	}

	public void StartLevel()
    {
        StartCoroutine(Level1());

    }

    IEnumerator Level1()
    {
        while(GameManager.instance.gaming)
        {
            Instantiate(cube, generatePos, Quaternion.identity, objectParent);
            yield return new WaitForSeconds(1f);
        }
    }
}
