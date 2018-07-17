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

    public Vector2 generateRate;
    bool generatingCloud = false;

    Transform cloudParent;

    void Start()
    {
        cloudParent = new GameObject("Parent_Cloud").transform;
    }

    public void StartGenerateCloud()
    {
        generatingCloud = true;

        StartCoroutine(LoopGenerateCloud());
    }

    public void StopGenerateCloud()
    {
        generatingCloud = false;
    }

    public void ClearCloud()
    {
        ClearChildObject(cloudParent);
    }

    IEnumerator LoopGenerateCloud()
    {
        float delay = 0;
        while (generatingCloud)
        {
            if (delay <= 0)
            {
                delay = Random.Range(generateRate.x, generateRate.y);

                StartCoroutine(GenerateCloud());
            }
            else
            {
                delay -= Time.deltaTime;
            }
            yield return null;
        }
    }

    float lastCloudX = 0;
    int lastCloudType = 0;

    IEnumerator GenerateCloud()
    {
        //随机云的种类，如果和上一朵相同就加一
        int type = Random.Range(0, cloudPrefabs.Length);
        if (type == lastCloudType)
        {
            type++;
            type %= cloudPrefabs.Length;
        }
        lastCloudType = type;
        //随机云的X坐标，如果和上一朵云太接近就重新生成
        float randomX;
        do
        {
            randomX = Random.Range(-rangeX, rangeX);
        }
        while (Mathf.Abs(randomX - lastCloudX) < 2);
        lastCloudX = randomX;

        //生成云
        GameObject cloud = Instantiate(cloudPrefabs[type],
            new Vector2(randomX, generateY), Quaternion.identity, cloudParent);
        //云有33%概率会遮挡小马
        int random = Random.Range(0, 3);
        if (random > 1)
            cloud.GetComponent<SpriteRenderer>().sortingLayerName = "Cloud";
        //持续下降
        while (generatingCloud && cloud.transform.position.y > destoryY)
        {
            cloud.transform.Translate(Vector2.down * speed * Time.deltaTime);
            yield return null;

        }
        if(GameManager.Instance().gaming)
            Destroy(cloud);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float height = 1f;
        Gizmos.DrawCube(new Vector2(0, generateY - height / 2), new Vector2(rangeX * 2, height));
        Gizmos.DrawCube(new Vector2(0, destoryY), new Vector2(6, 0.5f));
    }

    void ClearChildObject(Transform _parent)
    {
        while (_parent.childCount > 0)
        {
            Destroy(_parent.GetChild(0).gameObject);
            _parent.GetChild(0).SetParent(null);
        }
    }
}
