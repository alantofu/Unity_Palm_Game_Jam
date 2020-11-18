using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earning : MonoBehaviour
{
    public GameObject coinIcon;
    public bool collectable = false;
    public int waitSecond = 5;
    public float fadeDuration = 0.5f;

    public float coinIconLocalY = 1.5f;
    public float animationDistance = 1.5f;

    public int getCoinAmount = 500;

    void Start()
    {
        collectable = false;
        StopAllCoroutines();
        StartCoroutine(EarningMoney());
        coinIconLocalY = coinIcon.transform.localPosition.y;
    }

    IEnumerator EarningMoney()
    {
        yield return new WaitForSeconds(waitSecond);
        coinIcon.SetActive(true);
        collectable = true;
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
            yield return new WaitForSeconds(0.0167f);
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
            yield return new WaitForSeconds(0.0167f);
        }
        coinIcon.SetActive(false);
    }

    public void OnClickResponse()
    {
        if (collectable)
        {
            StartCoroutine(FadeOutIcon());
            Player.Instance.addMoney(getCoinAmount);
            collectable = false;
            StartCoroutine(EarningMoney());
        }
    }

}
