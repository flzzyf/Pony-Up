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

    private void Update()
    {
        if(triggerToFall && !triggered)
        {
            transform.Translate(Vector2.down * GameManager.Instance().backgroundFallingSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (triggered)
            return;

        if (other.gameObject.tag == "Enemy")
            return;

        triggered = true;
        rb.gravityScale = GameManager.Instance().globalGravity;

    }
}
