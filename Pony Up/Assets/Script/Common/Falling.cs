using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public float speed = .8f;
    public bool destoryWhenInvisible = false;

    bool invisible = false;
    void FixedUpdate()
    {
        if (!invisible)
            transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
    }

    void OnBecameInvisible()
    {
        invisible = true;

        if (destoryWhenInvisible)
            Destroy(transform.parent.gameObject);
        else
            gameObject.SetActive(false);
    }
}
