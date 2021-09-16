using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor.XR.Interaction.Toolkit;
public class Scr_VacSuckerZone : MonoBehaviour
{
    public bool suck;
    public bool isColliding;
    public GameObject leftHolster, nozzle;
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
    public void OnTriggerEnter(Collider other) // Items being sucked up within collider
    {
        if (suck)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("item"))
            {
                SuckableItems items = other.gameObject.GetComponent<SuckableItems>();
                items.GetComponent<SuckableItems>().Sucked();
                //it = other.gameObject; // not sure what this does  or why I wrote it
                //items.beingSucked = true;

            }
        }
        else
        {
            SuckableItems items = other.gameObject.GetComponent<SuckableItems>();
            items.beingSucked = false;

        }
    }
}
