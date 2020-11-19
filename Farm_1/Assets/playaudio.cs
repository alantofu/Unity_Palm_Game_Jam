using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playaudio : MonoBehaviour
{
    public AudioSource building;
    public AudioSource exit;
    public AudioSource market;
    public AudioSource leaderboard;
    public AudioSource news;
    void Start()
    {
       
    }
    public void playbuilding()
    {
        building.Play();
    }

    public void playexit()
    {
        exit.Play();
    }
    public void playmarket()
    {
        market.Play();
    }
    public void playleaderboard()
    {
        leaderboard.Play();
    }
    public void playnews()
    {
        news.Play();
    }
}
