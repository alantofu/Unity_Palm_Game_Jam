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
    public AudioSource select;
    public AudioSource setting;
    public AudioSource tick;
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
    public void playselect()
    {
        select.Play();
    }
    public void playsetting()
    {
        setting.Play();
    }

    public void playtick()
    {
        tick.Play();
    }
}
