using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public GameObject builtFactory;
    public GameObject constructingFactory;
    public GameObject promptCube;
    public Image timerBar;

    public float constructingTime = 20;
    public float remainingTime;

    public event Action<float> OnTimeDecrease = delegate { };

    public bool isConstructed = false; // true if successfully grew

    void Awake()
    {
        GetComponent<Earning>().enabled = false;
        OnTimeDecrease += HandleTimeChange; // += operator calls the add method on the event
    }

    void Start()
    {
        builtFactory.gameObject.SetActive(false);
        constructingFactory.gameObject.SetActive(true);
        timerBar.transform.parent.gameObject.SetActive(true);
        remainingTime = constructingTime;
        StartCoroutine(ConstructingProcess());
    }

    IEnumerator ConstructingProcess()
    {
        if (!isConstructed)
        {
            while (remainingTime > 0)
            {
                ReduceTime(Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            isConstructed = true;
        }
        builtFactory.gameObject.SetActive(true);
        constructingFactory.gameObject.SetActive(false);
        GetComponent<Earning>().enabled = true;
        timerBar.transform.parent.gameObject.SetActive(false); // disable timer bar
        this.enabled = false;
    }

    private void ReduceTime(float amount)
    {
        remainingTime -= amount;
        OnTimeDecrease(remainingTime / constructingTime); // Action<float>
    }

    private void HandleTimeChange(float newPercent)
    {
        timerBar.fillAmount = 1 - newPercent;
    }
}
