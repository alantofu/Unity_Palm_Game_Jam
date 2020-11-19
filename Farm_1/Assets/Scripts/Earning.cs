using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        isCollectable = false;
        StopAllCoroutines();
        StartCoroutine(EarningMoney());
        coinIconLocalY = coinIcon.transform.localPosition.y;
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
