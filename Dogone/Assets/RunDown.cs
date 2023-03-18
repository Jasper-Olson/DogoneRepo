using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeAudioSource.StartFade(GetComponent<AudioSource>(), 1.5f, 0f));
    }
}
