using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Earning : MonoBehaviour
{
    public GameObject coinIcon;
    public bool isCollectable = false;
    public int waitSecond = 5;
    public float fadeDuration = 0.5f;
    public float coinIconLocalY = 1.5f;
    public float animationDistance = 1.5f;
    public int getCoinAmount = 500;

    public bool isManufacturing = false; // true if it is producing money

    public Image timerBar;
    public float manufacturingTime = 6;
    public float remainingTime;
    public int totalProfit;

    public event Action<float> OnTimeDecrease = delegate { };


    void Awake()
    {
        OnTimeDecrease += HandleTimeChange; // += operator calls the add method on the event
    }

    void Start()
    {
        isCollectable = false;
        StartCoroutine(EarningMoney());
        coinIconLocalY = coinIcon.transform.localPosition.y;
    }

    public void StartManufacturingProcess(int maxProfit, int maxTime)
    {
        Debug.Log("Enter");
        if (maxTime > 0)
        {
            Debug.Log("Start");
            totalProfit = maxProfit;
            manufacturingTime = maxTime;
            timerBar.transform.parent.gameObject.SetActive(true);
            remainingTime = manufacturingTime;
            StartCoroutine(ManufacturingProcess());
        }
    }

    IEnumerator EarningMoney()
    {
        yield return new WaitForSeconds(waitSecond);
        coinIcon.SetActive(true);
        isCollectable = true;
        StartCoroutine(FadeInIcon());
    }

    IEnumerator FadeInIcon()
    {
        SpriteRenderer renderer = coinIcon.GetComponent<SpriteRenderer>();
        Color color = renderer.material.color;

        float elapsed = 0f;
        while (elapsed <= fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            renderer.material.color = color;
            coinIcon.transform.localPosition = new Vector3(coinIcon.transform.localPosition.x, Mathf.Lerp(coinIconLocalY - animationDistance, coinIconLocalY, elapsed / fadeDuration), coinIcon.transform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOutIcon()
    {
        SpriteRenderer renderer = coinIcon.GetComponent<SpriteRenderer>();
        Color color = renderer.material.color;

        float elapsed = 0f;
        while (elapsed <= fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            renderer.material.color = color;
            coinIcon.transform.localPosition = new Vector3(coinIcon.transform.localPosition.x, Mathf.Lerp(coinIconLocalY, coinIconLocalY + animationDistance, elapsed / fadeDuration), coinIcon.transform.localPosition.z);
            yield return new WaitForEndOfFrame();
        }
        coinIcon.SetActive(false);
    }

    IEnumerator ManufacturingProcess()
    {
        if (!isManufacturing)
        {
            isManufacturing = true;
            while (remainingTime > 0)
            {
                ReduceTime(Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        timerBar.transform.parent.gameObject.SetActive(false); // disable timer bar
        isManufacturing = false;
    }

    private void ReduceTime(float amount)
    {
        remainingTime -= amount;
        OnTimeDecrease(remainingTime / manufacturingTime); // Action<float>
    }

    private void HandleTimeChange(float newPercent)
    {
        timerBar.fillAmount = 1 - newPercent;
    }

    public void OnClickResponse()
    {
        if (isCollectable)
        {
            StartCoroutine(FadeOutIcon());
            Player.Instance.addMoney(getCoinAmount);
            isCollectable = false;
            StartCoroutine(EarningMoney());
        }
    }

}
