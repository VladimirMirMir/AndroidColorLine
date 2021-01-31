using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public Vector3 offset;
    [HideInInspector]
    public Transform target;

    private void Awake()
    {
        target = GameObject.Find("Motor").transform;
    }

    void Update()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
