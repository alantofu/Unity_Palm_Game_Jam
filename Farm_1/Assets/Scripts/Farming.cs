using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Farming : MonoBehaviour
{
    public GameObject oilIcon;
    public bool collectable = false;
    public int waitSecond = 3;
    public float fadeDuration = 0.5f;

    private float oilIconLocalY = 1.5f;

    public int getOilAmount = 1000;

    void Start()
    {
        collectable = false;
        StopAllCoroutines();
        StartCoroutine(FarmingFruit());
        oilIconLocalY = oilIcon.transform.position.y;
    }

    IEnumerator FarmingFruit()
    {
        yield return new WaitForSeconds(waitSecond);
        oilIcon.SetActive(true);
        collectable = true;
        StartCoroutine(FadeInIcon());
    }

    IEnumerator FadeInIcon()
    {
        SpriteRenderer renderer = oilIcon.GetComponent<SpriteRenderer>();
        Color color = renderer.material.color;

        float elapsed = 0f;
        while (elapsed <= fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            renderer.material.color = color;
            oilIcon.transform.localPosition = new Vector3(0, Mathf.Lerp(0, oilIconLocalY, elapsed / fadeDuration), 0);
            yield return new WaitForSeconds(0.0167f);
        }
    }

    IEnumerator FadeOutIcon()
    {
        SpriteRenderer renderer = oilIcon.GetComponent<SpriteRenderer>();
        Color color = renderer.material.color;

        float elapsed = 0f;
        while (elapsed <= fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            renderer.material.color = color;
            oilIcon.transform.localPosition = new Vector3(0, Mathf.Lerp(oilIconLocalY, 3, elapsed / fadeDuration), 0);
            yield return new WaitForSeconds(0.0167f);
        }
        oilIcon.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (collectable)
        {
            StartCoroutine(FadeOutIcon());
            Player.Instance.addOil(getOilAmount);
            collectable = false;
            StartCoroutine(FarmingFruit());
        }
    }

}
