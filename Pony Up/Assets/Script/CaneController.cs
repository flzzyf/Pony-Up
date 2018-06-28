using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneController : MonoBehaviour
{
    public Transform cane;
    public float maxRotation = 80;
    public float maxDistance = 3;
    public float maxSpeed = 2;

    Vector2 originPos;
    Vector2 offset;
    bool mouseDown = false;

    private void FixedUpdate()
    {
        if (!GameManager.instance.gaming)
            return;

        MovementControl();

        RotationControl();

        //自动复位
        if (!mouseDown)
        {
            Vector2 dir = GameManager.instance.twilight.transform.position - cane.position;
            float distance = dir.magnitude;
            if (distance > maxDistance)
            {
                cane.Translate(dir * Time.deltaTime);
            }
        }

    }

    void MovementControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            originPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mouseDown = true;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = mouseWorldPoint - originPos;
            Vector2 targetPos = originPos + offset;
            // Vector2 movement = offset.normalized * maxSpeed;
            Debug.Log(targetPos);
            // cane.Translate(movement, Space.World);
            cane.position = targetPos;

            //originPos = mouseWorldPoint;
        }

        if (Input.GetMouseButtonUp(0))
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
