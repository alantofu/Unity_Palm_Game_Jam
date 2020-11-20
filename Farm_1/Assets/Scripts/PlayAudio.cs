using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private static PlayAudio _instance;
    public static PlayAudio Instance { get { return _instance; } }

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
            Debug.Log(child.name);
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

}
