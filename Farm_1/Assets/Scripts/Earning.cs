using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Earning : MonoBehaviour
{
    private AudioManager audioManager;

    public GameObject coinIcon;
    public bool isCollectable = false;
    public int waitSecond = 5;
    public float fadeDuration = 0.5f;
    public float coinIconLocalY = 2.3f;
    public float animationDistance = 1.5f;
    public int getCoinAmount = 0;

    public bool isManufacturing = false; // true if it is producing money

    public Image timerBar;
    public float manufacturingTime = 6;
    public float remainingTime;
    public int totalProfit;
    public float profitRate;
    public int collectedProfit;
    public float generatedProfit;

    public event Action<float> OnTimeDecrease = delegate { };


    void Awake()
    {
        OnTimeDecrease += HandleTimeChange; // += operator calls the add method on the event
    }

    void Start()
    {
        audioManager = AudioManager.Instance;
        isCollectable = false;
        coinIconLocalY = coinIcon.transform.localPosition.y;
        Debug.Log("Coin Y Location: " + coinIcon.transform.localPosition.y);
    }

    public void StartManufacturingProcess()
    {
        manufacturingTime = this.transform.GetComponent<Factory>().totalTime;
        if (manufacturingTime > 0)
        {
            totalProfit = this.transform.GetComponent<Factory>().totalProfit;

            profitRate = totalProfit / manufacturingTime;
            generatedProfit = 0;
            collectedProfit = 0;
            remainingTime = manufacturingTime;

            timerBar.transform.parent.gameObject.SetActive(true);
            isManufacturing = true;
            StartCoroutine(ManufacturingProcess());
            StartCoroutine(EarningMoney());
        }
    }

    IEnumerator EarningMoney()
    {
        float elapsed = 0f;
        while (elapsed <= waitSecond && isManufacturing)
        {
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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
        while (remainingTime > 0)
        {
            ReduceTime(Time.deltaTime);
            generatedProfit += profitRate * Time.deltaTime;
            yield return new WaitForEndOfFrame();
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
            AudioManager.Instance.PlaySound("Money Audio");

            isCollectable = false;

            StartCoroutine(FadeOutIcon());
            int collectingAmount;
            if (isManufacturing)
            {
                collectingAmount = Mathf.FloorToInt(generatedProfit);
                generatedProfit = 0f;
                StartCoroutine(EarningMoney()); // continue earning if it is manufacturing
            }
            else
            {
                collectingAmount = totalProfit - collectedProfit;
                generatedProfit = 0f;
            }
            Player.Instance.AddMoney(Mathf.FloorToInt(collectingAmount));
            collectedProfit += collectingAmount;
        }
    }

}
