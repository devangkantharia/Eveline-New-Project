using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform camCam;
    public Vector3 posCam;
    public Quaternion lastPos;
    public float turnSpeed = 20f;

    void Awake()
    {
        camCam = Camera.main.transform;
    }

    void Update()
    {
        posCam = camCam.position - transform.position;
        posCam.y = 0;
        lastPos = Quaternion.LookRotation(-posCam);
        transform.rotation = Quaternion.Slerp(transform.rotation, lastPos, Time.deltaTime * turnSpeed);
    }
}
