using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audio;
    public bool rundown;
    public float looplength;
    public bool reversible;
    public float Volume;
    public float TrackTime;
    float timer = 0f;
    void Start()
    {
        reversible = true;
        rundown = false;
        audio.volume = 0f;
        StartCoroutine(FadeAudioSource.StartFade(audio, 5f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        Volume = audio.volume;
        TrackTime = audio.time;
        if(TrackTime >= looplength - 1.25f & Time.time >= timer)
        {
            StartCoroutine(FadeAudioSource.StartFade(audio, 1.25f, 0f));
            timer = Time.time + looplength;
        }
        if(reversible == true)
        {
            if(Volume == 0f)
            {
                Invoke("DelayedReturn", 0.125f);
            }
        }
        if(rundown == true)
        {
            reversible = false;
            StartCoroutine(FadeAudioSource.StartFade(audio, 1.5f, 0f));
        }
    }
    void DelayedReturn()
    {
        StartCoroutine(FadeAudioSource.StartFade(audio, 1.25f, 0.5f));
    }
}
