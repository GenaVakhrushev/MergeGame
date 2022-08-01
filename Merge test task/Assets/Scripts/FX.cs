using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    public static FX singleton { get; private set; }

    public GameObject IceShatter;
    public GameObject LightCast;

    public AudioClip Pop;
    public AudioClip Destr;
    public AudioClip Splash;

    AudioSource audioSourse;

    private void Awake()
    {
        singleton = this;
        audioSourse = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        audioSourse.clip = clip;

        audioSourse.Play();
    }
}
