using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClick : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource musicSource;

    // Start is called before the first frame update
    public void ClickMenu()
    {
        musicSource.PlayScheduled(AudioSettings.dspTime);
    }
}
