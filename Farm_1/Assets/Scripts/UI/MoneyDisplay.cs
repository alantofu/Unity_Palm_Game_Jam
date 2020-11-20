using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class MoneyDisplay : MonoBehaviour
{
    public TMP_Text coinText;
    private Player player;

    void Start()
    {
        player = Player.Instance;
    }
    
    void Update()
    {
        coinText.text = player.money.ToString();
    }
}
