using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playaudio : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
       
    }
    public void playsound()
    {
        audio.Play();
    }
}
