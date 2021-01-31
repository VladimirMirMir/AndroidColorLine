using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    AudioSource souce;
    bool isOn = true;

    private void Awake()
    {
        souce = GetComponent<AudioSource>();
        souce.playOnAwake = false;
    }

    public void PlaySound(AudioClip clip)
    {
        souce.clip = clip;
        souce.Play();
    }

    public void ToggleSound()
    {
        if(isOn)
        {
            souce.volume = 0;
            isOn = false;
        }
        else
        {
            souce.volume = 1;
            isOn = true;
        }
    }
}
