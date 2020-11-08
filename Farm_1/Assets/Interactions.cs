using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public GameObject GO;
    // Start is called before the first frame update
    void Start()
    {
        GO.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GO.SetActive(false);
        }
    }
}
