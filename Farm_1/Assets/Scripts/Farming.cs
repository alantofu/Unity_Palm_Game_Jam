using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public GameObject oilIcon;
    public bool collectable = false;
    public int waitSecond = 3;
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;

    void Start()
    {
        collectable = false;
        StopAllCoroutines();
        StartCoroutine(FarmingFruit());
    }

    IEnumerator FarmingFruit()
    {
        float elapsed = 0f;
        while(elapsed <= fadeOutDuration) {
            
            yield return null;
        }
        oilIcon.SetActive(false);
        Debug.Log(this.name + " starts farming fruit");
        yield return new WaitForSeconds(waitSecond);
        Debug.Log(this.name + " ready to be harvested");
        oilIcon.SetActive(true);
        collectable = true;
    }

    private void OnMouseDown()
    {
        if (collectable)
        {
            collectable = false;
            Debug.Log(this.name + " fruit is collected");
            StartCoroutine(FarmingFruit());
        }
    }

}
