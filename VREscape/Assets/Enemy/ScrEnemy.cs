using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 10.0f;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        //transform.position = target.position;
        Vector3 direction = target.position - transform.position;
        if (target.GetComponent<Scr_GuideSystem>().target != null)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            // Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
            // transform.rotation = rot;
        }
        //Apply the rotation 

        // put 0 on the axys you do not want for the rotation object to rotate
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
