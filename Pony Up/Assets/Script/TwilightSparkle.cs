using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightSparkle : MonoBehaviour
{
    public GameObject gfx;
    Animator animator;
    Collider2D[] colliders;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        colliders = GetComponentsInChildren<Collider2D>();
    }

    void Update()
    {
        if (GameManager.Instance().gaming)
        {
            if(Input.GetMouseButton(0))
            {
                float scaleX = CaneController.Instance().cane.position.x < 0 ? -1 : 1;
                transform.localScale = new Vector3(scaleX, 1, 1);
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Dead();
            GameManager.Instance().GameOver();
        }
    }

    void Dead()
    {
        animator.SetBool("dead", true);
        foreach (var item in colliders)
        {
            item.enabled = false;
        }
    }

    public void Rebirth()
    {
        animator.SetBool("dead", false);
        foreach (var item in colliders)
        {
            item.enabled = true;
        }


    }
}
