using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_item : MonoBehaviour
{
    public AudioSource HaudioData;
    public RandomAudioClip HLibAudio;
    // Start is called before the first frame update
    public void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody.velocity.magnitude > 1.5f)
        {

            float audioLevel = other.relativeVelocity.magnitude / 10.0f;


           // Debug.Log("hit level: " + other.rigidbody.velocity.magnitude);
            AudioClip HClip = HLibAudio.GetComponent<RandomAudioClip>().GetRandomAudioClip();

            //HaudioData.PlayOneShot(HClip, audioLevel);
            //HaudioData.clip = HClip;
            HaudioData.volume = other.relativeVelocity.magnitude / 10.0f;
            HaudioData.Play();//(HClip,audioLevel);
        }
    }
        //Collider myCollider = collision.contacts[0].thisCollider;
    
    }
