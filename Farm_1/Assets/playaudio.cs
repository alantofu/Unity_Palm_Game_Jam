using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playaudio : MonoBehaviour
{
    public AudioSource building;
    void Start()
    {
       
    }
    public void playBuilding()
    {
        building.Play();
    }
}
