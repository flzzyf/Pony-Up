using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : Singleton<CloudManager>
{
    public GameObject[] cloudPrefabs;
    public float speed = 0.8f;
    public float rangeX;
    public float generateY;
    public float destoryY;

    void Start()
    {

    }

    void Update()
    {

    }

    public void StartGenerate()
    {
        StartCoroutine(GenerateCloud());
    }

    IEnumerator GenerateCloud()
    {
        int type = Random.Range(0, cloudPrefabs.Length - 1);
        float randomX = Random.Range(-rangeX, rangeX);
        GameObject cloud = Instantiate(cloudPrefabs[type],
            new Vector2(randomX, generateY), Quaternion.identity);
        while (cloud.transform.position.y > destoryY)
        {
            cloud.transform.Translate(Vector2.down * speed * Time.deltaTime);
            yield return null;

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float height = 1f;
        Gizmos.DrawCube(new Vector2(0, generateY - height / 2), new Vector2(rangeX, height));
        Gizmos.DrawCube(new Vector2(0, destoryY), new Vector2(rangeX, height));

    }
}
