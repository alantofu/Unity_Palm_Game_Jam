using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class RepDisplay : MonoBehaviour
{
    public TMP_Text text;
    private Player player;

    void Start()
    {
        player = Player.Instance;
    }
    
    void Update()
    {
        text.text = player.reputation.ToString();
    }
}
