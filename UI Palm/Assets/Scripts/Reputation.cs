using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
     private int reputation = 15;
     public Text reputationText;

    // Update is called once per frame
    void Update()
    {
        reputationText.text = reputation+"";

        if(Input.GetKeyDown(KeyCode.Space))
        {
            reputation--;
        }
    }
}
