using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneController : MonoBehaviour
{
    public Transform cane;
    public float maxRotation = 80;
    public float maxDistance = 3;
    public float speed = 2;

    Vector2 originPos;
    Vector2 offset;
    bool mouseDown = false;
    Vector2 targetPos;

    private void FixedUpdate()
    {
        if (!GameManager.Instance().gaming)
            return;

        MovementControl();

        RotationControl();

        //自动复位
        if (!mouseDown)
        {
            Vector2 dir = GameManager.Instance().twilight.transform.position - cane.position;
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
            //移动权杖的核心代码
            //获取权杖移动的目标点
            Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = mouseWorldPoint - originPos;
            targetPos = originPos + offset;

            //向目标点移动权杖
            Vector2 desiredPos = Vector2.Lerp(cane.position, targetPos, speed * Time.deltaTime);
            cane.position = desiredPos;

        }

        if (Input.GetMouseButtonUp(0))
        {
            offset = Vector2.zero;

            mouseDown = false;
        }
    }

    void RotationControl()
    {
        float offsetX = (targetPos - (Vector2)cane.position).x;
        offsetX /= 0.6f;
        Vector3 rotation = -1 * Vector3.forward * offsetX * maxRotation;
        cane.rotation = Quaternion.Lerp(cane.rotation, Quaternion.Euler(rotation), 0.1f);
    }

}
