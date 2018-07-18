using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public bool triggerToFall = false;

    Rigidbody2D rb;
    bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!triggerToFall)
        {
            rb.gravityScale = GameManager.Instance().globalGravity;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (triggered)
            return;

        triggered = true;
        rb.gravityScale = GameManager.Instance().globalGravity;

    }
}
