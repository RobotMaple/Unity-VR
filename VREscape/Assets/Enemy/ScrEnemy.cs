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
        Vector3 direction = target.position - transform.position;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
        //Apply the rotation 
        transform.rotation = rot;
        // put 0 on the axys you do not want for the rotation object to rotate
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,0 );
    }
}
