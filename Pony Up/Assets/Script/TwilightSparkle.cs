using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightSparkle : MonoBehaviour
{
    public GameObject gfx;
    Animator animator;
    Collider2D collider2D;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        collider2D = GetComponentInChildren<Collider2D>();
    }

    void Update()
    {
        if (GameManager.Instance().gaming)
        {
            float scaleX = CaneController.Instance().cane.position.x < 0 ? -1 : 1;
            gfx.transform.localScale = new Vector3(scaleX, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("DIE");
            Dead();
            GameManager.Instance().GameOver();
        }
    }

    void Dead()
    {
        animator.SetBool("dead", true);
        collider2D.enabled = false;
    }

    public void Rebirth()
    {
        animator.SetBool("dead", false);
        collider2D.enabled = true;


    }
}
