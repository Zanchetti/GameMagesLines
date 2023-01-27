using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenuControl : MonoBehaviour {
    public AudioSource musicSource;

    void Start()
    {
        musicSource.PlayScheduled(AudioSettings.dspTime);
    }

    // Update is called once per frame
    void Update() { }
}