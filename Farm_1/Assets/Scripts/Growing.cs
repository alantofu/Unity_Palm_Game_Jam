﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growing : MonoBehaviour
{
    public GameObject smallTree;
    public GameObject mediumTree;
    public GameObject grownTree;
    public GameObject timerObject;

    private TextMesh timerText;
    public float growingTime = 20;
    private float timeRemaining;
    bool mediumAlready = false;

    private void Start()
    {
        smallTree.gameObject.SetActive(true);
        mediumTree.gameObject.SetActive(false);
        grownTree.gameObject.SetActive(false);
        timerText = timerObject.GetComponent<TextMesh>();
        timeRemaining = growingTime;
    }


    private void Update()
    {
        timerObject.transform.LookAt(Camera.main.transform);
        timerObject.transform.LookAt(2 * transform.position - Camera.main.transform.position);
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.FloorToInt(timeRemaining).ToString();
            if ((timeRemaining < growingTime/2) && !mediumAlready)
            {
                smallTree.gameObject.SetActive(false);
                mediumTree.gameObject.SetActive(true);
                grownTree.gameObject.SetActive(false);
                mediumAlready = true;
            }
        }
        else
        {
            smallTree.gameObject.SetActive(false);
            mediumTree.gameObject.SetActive(false);
            grownTree.gameObject.SetActive(true);
            timerText.text = "";
        }
    }

}