using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    [SerializeField]
    TrailRenderer tireMarkL;

    [SerializeField]
    TrailRenderer tireMarkR;

    [SerializeField]
    AudioClip tires;

    AudioSource myAudio;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void SetTiremarks(bool grounded)
    {
        if (!grounded)
        {
            //Stop audio and tiremarks
        }
        else
        {
            //Play tiremark emitters
            if (!myAudio.isPlaying)
            {
                myAudio.PlayOneShot(tires);
                myAudio.loop = true;
            }
        }
    }
}
