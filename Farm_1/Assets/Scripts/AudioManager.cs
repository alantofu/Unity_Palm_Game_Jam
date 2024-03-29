﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    public Dictionary<string, AudioSource> audioList;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        audioList = new Dictionary<string, AudioSource>();
        foreach (Transform child in transform)
        {
            audioList.Add(child.name, child.GetComponent<AudioSource>());
        }
    }

    public void PlaySound(string audioName)
    {
        if (audioList.ContainsKey(audioName))
        {
            audioList[audioName].Play();
        }
    }

    public void PlayPlantAudio() {
        audioList["Plant Audio"].Play();
    }

}
