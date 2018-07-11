using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneController : Singleton<CaneController>
{
    public Transform cane;
    public float maxRotation = 80;
    public float maxDistance = 3;
    public float speed = 2;
    public float offsetScale = 1.5f;

    Vector2 offset;
    bool mouseDown = false;
    Vector2 lastMousePoint;
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
                cane.Translate(dir * Time.deltaTime, Space.World);
            }
        }

    }
    //移动控制
    void MovementControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;

            targetPos = cane.position;
        }

        if (Input.GetMouseButton(0))
        {
            //移动权杖的核心代码
            //获取权杖移动的目标点
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (lastMousePoint == Vector2.zero)
                lastMousePoint = mousePoint;
            offset = mousePoint - lastMousePoint;
            targetPos += offset * offsetScale;

            //向目标点移动权杖
            if (Vector2.Distance(cane.position, targetPos) < speed * Time.deltaTime)
            {
                cane.position = targetPos;
            }
            else
            {
                Vector2 dir = targetPos - (Vector2)cane.position;
                dir.Normalize();
                cane.Translate(dir * speed * Time.deltaTime, Space.World);
            }

            lastMousePoint = mousePoint;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;

            lastMousePoint = Vector2.zero;

            offset = Vector2.zero;
        }
    }
    //移动旋转
    void RotationControl()
    {
        float offsetX = offset.x;
        offsetX /= 0.6f;
        offsetX = Mathf.Clamp(offsetX, -maxRotation, maxRotation);
        Vector3 rotation = -1 * Vector3.forward * offsetX * maxRotation;
        cane.rotation = Quaternion.Lerp(cane.rotation, Quaternion.Euler(rotation), 0.1f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(targetPos, 0.5f);

    }
}
