using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public float movingDistance = 5f;

    Vector2 originPos;

    void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        if (GameManager.Instance().gaming &&
         Mathf.Abs(transform.position.y - originPos.y) < movingDistance)
        {
            transform.Translate(Vector2.down * GameManager.Instance().backgroundFallingSpeed * Time.deltaTime, Space.World);
        }
    }

    public void Reset()
    {
        transform.position = originPos;
    }
}
