using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightSparkle : MonoBehaviour 
{
    SpriteRenderer gfx;

	void Start () 
	{
        gfx = GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update () 
	{
        if(GameManager.instance.gaming)
        {
            bool flip = GameManager.instance.caneController.cane.position.x < 0;
            gfx.flipX = flip;
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("DIE");
            GameManager.instance.GameOver();
        }
	}
}
