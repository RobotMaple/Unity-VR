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





        Debug.Log("mag");
        AudioClip HClip = HLibAudio.GetComponent<RandomAudioClip>().GetRandomAudioClip();
        HaudioData.clip = HClip;
        HaudioData.Play();

        //Collider myCollider = collision.contacts[0].thisCollider;
    }
    public void GotHit()
    {

    }
    }
