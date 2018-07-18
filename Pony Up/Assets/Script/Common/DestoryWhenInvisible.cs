using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryWhenInvisible : MonoBehaviour
{
    public float disappearRange = 1f;

    Vector2 worldScreenSize;

    bool destoryed = false;

    private void Awake()
    {
        worldScreenSize = zyf.GetWorldScreenSize();
    }

    private void Update()
    {
        if (destoryed)
            return;

        if (transform.position.y < -1 * worldScreenSize.y / 2 - disappearRange)
        {
            destoryed = true;
            Destroy(gameObject);

        }
    }
}
