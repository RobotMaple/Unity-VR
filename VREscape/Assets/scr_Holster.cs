using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Holster : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform other;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   

    void Update()
    {
        Vector3 newForward = cameraTransform.forward;
        newForward.y = 0;
        transform.position = cameraTransform.position;
       
        transform.forward = newForward;



    }
}
