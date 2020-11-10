using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{

    public bool collectable = false;

    void Start()
    {
        collectable = false;
        StopAllCoroutines();
        StartCoroutine(FarmingFruit());
    }

    IEnumerator FarmingFruit()
    {
        Debug.Log(this.name + " starts farming fruit");
        yield return new WaitForSeconds(3);
        Debug.Log(this.name + " ready to be harvested");
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
