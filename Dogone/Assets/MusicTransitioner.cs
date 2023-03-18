using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransitioner : MonoBehaviour
{
    // Start is called before the first frame update
    public string LevelAudioName;
    public string PreviousLevelAudioName;
    public GameObject LevelAudio;
    public GameObject PreviousLevelAudio;
    public GameObject Player;
    public bool active;
    public bool active2;
    public float Xfalloff;
    public float MinX;

    void Start()
    {
        LevelAudio = GameObject.Find(LevelAudioName);
        PreviousLevelAudio = GameObject.Find(PreviousLevelAudioName);
    }

    // Update is called once per frame
    void Update()
    {
        LevelAudio = GameObject.Find(LevelAudioName);
        PreviousLevelAudio = GameObject.Find(PreviousLevelAudioName);
        Player = GameObject.Find("Player");
        if(active == true)
        {
            PreviousLevelAudio.GetComponent<Music>().StartCoroutine(FadeAudioSource.StartFade(PreviousLevelAudio.GetComponent<AudioSource>(), 1.5f, 0f));
            active = false;
        }

        if(active2 == true & LevelAudio.GetComponent<AudioSource>().enabled == true && Player.transform.position.x < Xfalloff && Player.transform.position.x > MinX)
        {   
            Invoke("DelayedCheck", 0.2f);
        }

        if(PreviousLevelAudio.GetComponent<AudioSource>().enabled == true && Player.transform.position.x > Xfalloff)
        {
            PreviousLevelAudio.GetComponent<Music>().reversible = false;
            PreviousLevelAudio.GetComponent<Music>().StartCoroutine(FadeAudioSource.StartFade(PreviousLevelAudio.GetComponent<AudioSource>(), 1.5f, 0f));
            Invoke("NextStart", 1.5f);
        }

        if(Player.transform.position.x < MinX)
        {
            LevelAudio.GetComponent<Music>().StartCoroutine(FadeAudioSource.StartFade(LevelAudio.GetComponent<AudioSource>(), 1.5f, 0f));
            Invoke("HardStop", 1.5f);
        }
    }

    void NextStart()
    {
        PreviousLevelAudio.GetComponent<AudioSource>().enabled = false;
        LevelAudio.GetComponent<Music>().reversible = true;
        LevelAudio.GetComponent<AudioSource>().enabled = true;
        LevelAudio.GetComponent<AudioSource>().volume = 0f;
        LevelAudio.GetComponent<Music>().StartCoroutine(FadeAudioSource.StartFade(LevelAudio.GetComponent<AudioSource>(), 1.5f, 0.49f));
    }


    void PreviousStart()
    {   
        PreviousLevelAudio.GetComponent<Music>().rundown = false;
        LevelAudio.GetComponent<AudioSource>().enabled = false;
        PreviousLevelAudio.GetComponent<Music>().reversible = true;
        PreviousLevelAudio.GetComponent<AudioSource>().enabled = true;
        PreviousLevelAudio.GetComponent<AudioSource>().volume = 0f;
        PreviousLevelAudio.GetComponent<Music>().StartCoroutine(FadeAudioSource.StartFade(PreviousLevelAudio.GetComponent<AudioSource>(), 1.5f, 0.5f));
    }

    void HardStop()
    {
        LevelAudio.GetComponent<AudioSource>().enabled = false;
    }

    void DelayedCheck()
    {
    
        if(active2 == true & LevelAudio.GetComponent<AudioSource>().enabled == true && Player.transform.position.x < Xfalloff && Player.transform.position.x > MinX)
        {    
            LevelAudio.GetComponent<Music>().StartCoroutine(FadeAudioSource.StartFade(LevelAudio.GetComponent<AudioSource>(), 1.5f, 0f));
            LevelAudio.GetComponent<Music>().reversible = false;
            Invoke("PreviousStart", 1.5f);
            active2 = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            active = true;
            active2 = true;
            LevelAudio = GameObject.Find(LevelAudioName);
            PreviousLevelAudio = GameObject.Find(PreviousLevelAudioName);
        }
    }
}
