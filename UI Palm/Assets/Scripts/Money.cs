using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private int money = 10000;
    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$ "+money;

        if(Input.GetKeyDown(KeyCode.Space)){
            money+=100;
        }
    }
}
