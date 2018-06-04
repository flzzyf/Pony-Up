using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightSparkle : MonoBehaviour 
{


	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("DIE");
        }
	}
}
