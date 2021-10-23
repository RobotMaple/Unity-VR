using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedSpeed;
    // Update is called once per frame
    void Update()
    {

        this.transform.Rotate(speedSpeed * Time.deltaTime, speedSpeed * Time.deltaTime, speedSpeed * Time.deltaTime);
    }
}
