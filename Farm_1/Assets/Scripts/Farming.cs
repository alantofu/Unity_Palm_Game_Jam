using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Farming : MonoBehaviour
{
    public GameObject grownTree;
    public GameObject deadTree;
    public GameObject oilIcon;
    private GameManager gameManager;
    private PlantSystem plantSystem;

    public bool collectable = false;
    public int waitSecond = 3;
    public float fadeDuration = 0.5f;

    public float oilIconLocalY = 1.5f;
    public float animationDistance = 1.5f;

    public int getOilAmount = 1000;
    public int maxFarmCount = 10;
    public int farmCount = 0;
    public bool isDead = false; // true if the palm oil tree is dead

    void Start()
    {
        gameManager = GameManager.Instance;
        plantSystem = PlantSystem.Instance;
        collectable = false;
        StopAllCoroutines();
        StartCoroutine(FarmingFruit());
        oilIconLocalY = oilIcon.transform.localPosition.y;
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
            oilIcon.transform.localPosition = new Vector3(0, Mathf.Lerp(oilIconLocalY - animationDistance, oilIconLocalY, elapsed / fadeDuration), 0);
            yield return new WaitForEndOfFrame();
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
            oilIcon.transform.localPosition = new Vector3(0, Mathf.Lerp(oilIconLocalY, oilIconLocalY + animationDistance, elapsed / fadeDuration), 0);
            yield return new WaitForEndOfFrame();
        }
        oilIcon.SetActive(false);
    }

    public void OnClickResponse()
    {
        if (collectable)
        {
            farmCount++;
            StartCoroutine(FadeOutIcon());
            Player.Instance.addOil(getOilAmount);
            collectable = false;
            if (farmCount < maxFarmCount)
            {
                StartCoroutine(FarmingFruit());
            }
            else
            {
                isDead = true;
                deadTree.SetActive(true);
                grownTree.SetActive(false);
            }
        }
        else if (isDead)
        {
            gameManager.replantTreePanel.SetActive(true);
            plantSystem.selectedDeadTreeObj = this.gameObject;
        }
    }

}
