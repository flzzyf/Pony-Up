using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryWhenInvisible : MonoBehaviour
{

    void OnBecameInvisible()
    {
        Destroy(transform.parent.gameObject);
    }
}
