using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{

    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    
    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    
    }
}
public class vrARig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConst;
    private Vector3 headBodyOffset;
    public float turnSmoothness;
    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConst.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = headConst.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConst.up, Vector3.up).normalized,Time.deltaTime * turnSmoothness);
        head.Map();
        leftHand.Map();
        rightHand.Map();

    }
}
