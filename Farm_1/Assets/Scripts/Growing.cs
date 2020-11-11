using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Growing : MonoBehaviour
{
    // https://www.youtube.com/watch?v=CA2snUe7ARM

    public GameObject smallTree;
    public GameObject mediumTree;
    public GameObject grownTree;
    public Image timerBar;

    public float growingTime = 20;
    public float remainingTime;

    public event Action<float> OnTimeDecrease = delegate { };

    bool mediumAlready = false;
    public bool grew = false; // true if successfully grew

    private void Awake()
    {
        GetComponent<Farming>().enabled = false;
        OnTimeDecrease += HandleTimeChange; // += operator calls the add method on the event
    }

    private void Start()
    {
        smallTree.gameObject.SetActive(true);
        mediumTree.gameObject.SetActive(false);
        grownTree.gameObject.SetActive(false);
        remainingTime = growingTime;
        StartCoroutine(GrowingProcess());
    }

    IEnumerator GrowingProcess()
    {
        if (!grew)
        {
            while (remainingTime > 0)
            {
                ReduceTime(Time.deltaTime);
                if ((remainingTime < growingTime / 2) && !mediumAlready)
                {
                    smallTree.gameObject.SetActive(false);
                    mediumTree.gameObject.SetActive(true);
                    grownTree.gameObject.SetActive(false);
                    mediumAlready = true;
                }
                yield return null;
            }
            grew = true;
        }
        smallTree.gameObject.SetActive(false);
        mediumTree.gameObject.SetActive(false);
        grownTree.gameObject.SetActive(true);
        GetComponent<Farming>().enabled = true;
        timerBar.transform.parent.gameObject.SetActive(false); // disable timer bar
        this.enabled = false;
    }

    private void ReduceTime(float amount)
    {
        remainingTime -= amount;
        OnTimeDecrease(remainingTime / growingTime); // Action<float>
    }

    private void HandleTimeChange(float newPercent)
    {
        timerBar.fillAmount = 1 - newPercent;
    }

}