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
    public bool inHand,isFull;
    public ParticleSystem suckParticle;
    public AudioSource ClipSource;
    public float FadeVol,i =0;

    private void Start()
    {
        ClipSource.Play();
    }
    public void Update()
    {
        
        isFull = nozzle.GetComponent<Scr_VacSucker>().isFull;
        //suckZone.enabled = false;
        if (suck && !isFull)
        {
                
                suckParticle.Play();
            if (suckZone.GetComponent<CapsuleCollider>().height <= 3.5)
            {
                suckZone.GetComponent<CapsuleCollider>().height += (15f * Time.deltaTime);
            }
           
        }
        else {  i = 0; suckParticle.Stop();
            if (suckZone.GetComponent<CapsuleCollider>().height >= 0)
            { suckZone.GetComponent<CapsuleCollider>().height -= (15f * Time.deltaTime); } }


        if (!inHand) {

            transform.rotation = Quaternion.identity;
            Transform Targetpos = leftHolster.transform;
            transform.position = Targetpos.position;            
        } 
    }
    public void sucking()
    {
        if (!isFull && ClipSource.volume == 0)
        {
            StartCoroutine(AudioFade.StartFade(ClipSource, 1f, .5f));
        }
        
        
        suck = true;
        
    }
    public void NotSucking()
    {
        if (ClipSource.volume != 0)
        {
            StartCoroutine(AudioFade.StartFade(ClipSource, .5f, 0f));
        }
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
    public void AudioClipFade()
    {

        
    }
    public void OnTriggerStay(Collider other) // Items being sucked up within collider
    {
        if (suck && !isFull)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("item"))
            {
                
                SuckableItems items = other.gameObject.GetComponent<SuckableItems>();
                //items.GetComponent<SuckableItems>().Sucked();*/
                if (items.ItemState != SuckableItems.itemState.Shrink)
                { 
                items.ItemState = SuckableItems.itemState.Following;
                }
            }
        }
        else
        {
            SuckableItems items = other.gameObject.GetComponent<SuckableItems>();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("item"))
        {
            SuckableItems items = other.gameObject.GetComponent<SuckableItems>();
            //items.GetComponent<SuckableItems>().Sucked();*/
            if (items.ItemState == SuckableItems.itemState.Following)
            {
                items.ItemState = SuckableItems.itemState.Idle;
            }
        }
    }
}
