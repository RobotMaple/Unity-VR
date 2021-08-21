using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor.XR.Interaction.Toolkit;
public class Scr_VacSuckerZone : MonoBehaviour
{
    public bool suck;
    public bool isColliding;
    public GameObject leftHolster;
    public Collider suckZone;
    public GameObject it;
    public bool inHand;
    public ParticleSystem suckParticle;
    private void Update()
    {
        suckZone.enabled = false;
        if (suck)
        {
            suckParticle.Play();
            suckZone.enabled = true;
        }
        else { suckParticle.Stop(); }

        if (!inHand) {

            transform.rotation = Quaternion.identity;
            Transform Targetpos = leftHolster.transform;
            transform.position = Targetpos.position;

            
        }

        
    }
    public void sucking()
    {
        
        suck = true;
    }
    public void NotSucking()
    {
       
        suck = false;

    }
    public void PickedUp()
    {
        inHand = true;
    }
    public void LetGo()
    {
        inHand = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (suck)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("item"))
            {
                /*  Debug.Log("testing");
                  Vector3 relativePos = NozzlePos.transform.position - other.gameObject.transform.position;
                  Rigidbody Rbody = other.gameObject.GetComponent<Rigidbody>();
                  Rbody.AddForce(speed * relativePos);*/
                SuckableItems items = other.gameObject.GetComponent<SuckableItems>();

                items.beingSucked = true;
                it = other.gameObject;
                //items.FollowTargetWithRotation(other.gameObject, NozzlePos.transform);
                //  items = true;
            }
        }
    }
}
