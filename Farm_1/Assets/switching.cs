using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class switching : MonoBehaviour
{
    public GameObject TreeSmall, TreeMedium, TreeGrown,Timer,Camera;
    private TextMesh TimerText;
    public float GrowingTime = 20;
    private float timeRemaining;
    bool mediumalready = false;
    // Start is called before the first frame update
    void Start()
    {
        TreeSmall.gameObject.SetActive(true);
        TreeMedium.gameObject.SetActive(false);
        TreeGrown.gameObject.SetActive(false);
        TimerText = Timer.GetComponent<TextMesh>();
        timeRemaining = GrowingTime;
    }

    // Update is called once per frame
    void Update()
    {
        Timer.transform.LookAt(Camera.transform);
        Timer.transform.LookAt(2 * transform.position - Camera.transform.position);
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            TimerText.text = Math.Floor(timeRemaining).ToString();
            if ((timeRemaining < GrowingTime/2) && !mediumalready)
            {
                TreeSmall.gameObject.SetActive(false);
                TreeMedium.gameObject.SetActive(true);
                TreeGrown.gameObject.SetActive(false);
                mediumalready = true;
            }
        }
        else
        {
            TreeSmall.gameObject.SetActive(false);
            TreeMedium.gameObject.SetActive(false);
            TreeGrown.gameObject.SetActive(true);
            TimerText.text = "Completed!!!";
        }
    }
}
