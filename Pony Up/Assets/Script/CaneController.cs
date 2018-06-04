using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneController : MonoBehaviour 
{
    public Transform cane;
    public float maxRotation = 80;
    public float maxDistance = 3;

    Vector2 originPos;
    Vector2 offset;
    bool mouseDown = false;

	private void FixedUpdate()
	{
        MovementControl();

        RotationControl();

        if(!mouseDown)
        {
            Vector2 dir = GameManager.instance.twilight.position - cane.position;
            float distance = dir.magnitude;
            Debug.Log(distance);
            if (distance > maxDistance)
            {
                cane.Translate(-dir * Time.deltaTime);
            }
        }

	}

	void MovementControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            originPos = Input.mousePosition;
            originPos = Camera.main.ScreenToWorldPoint(originPos);

            mouseDown = true;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = mouseWorldPoint - originPos;
            //Debug.Log(offset);
            cane.Translate(offset, Space.World);

            originPos = mouseWorldPoint;
        }

        if(Input.GetMouseButtonUp(0))
        {
            offset = Vector2.zero;

            mouseDown = false;
        }
    }

    void RotationControl()
    {
        float offsetX = offset.x;
        offsetX /= 0.6f;
        Vector3 rotation = -1 * Vector3.forward * offsetX * maxRotation;
        cane.rotation = Quaternion.Lerp(cane.rotation, Quaternion.Euler(rotation), 0.1f);
    }

}
