using UnityEngine;
using System.Collections;
using System;

public class AudioFade
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        if (audioSource.isPlaying)
        {
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                Debug.Log("Volume:  " + audioSource.volume);
                yield return null;
            }
            yield break;
        }
    }
}