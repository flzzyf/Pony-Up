using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour 
{
    Rigidbody2D rb;

    public float speed = .8f;

	void Start () 
	{
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
	{
        rb.AddForce(Vector2.down * speed, ForceMode2D.Force);
	}
}
