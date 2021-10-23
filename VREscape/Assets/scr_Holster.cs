using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Holster : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject leftHolster, rightHolster, leftItem, rightItem;
    
    public float speed;
    // Start is called before the first frame update
    public void Update()
    {
        Vector3 newForward = cameraTransform.forward;
        newForward.y = 0;
        transform.position = new Vector3(cameraTransform.position.x,cameraTransform.position.y, cameraTransform.position.z);
        
        //transform.forward = newForward;
        comeback();
    }
    public void comeback()
    {
        bool isHeldR, isHeldL;
        isHeldR = rightItem.GetComponent<Scr_VacShooter>().inHand;
        isHeldL = leftItem.GetComponent<Scr_VacSuckerZone>().inHand;
        if (!isHeldR) { rightItem.transform.position = rightHolster.transform.position; }
        if (!isHeldL) { leftItem.transform.position = leftHolster.transform.position; }
        Debug.Log("Goback");
    }



}
