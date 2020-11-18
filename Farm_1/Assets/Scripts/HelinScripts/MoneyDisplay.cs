using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class MoneyDisplay : MonoBehaviour
{
  // public Text coinText;
  public TMP_Text coinText;
  public Player player;
  void Start()
  {
    player = Player.Instance;
    coinText.text = player.Money.ToString();
  }
  // Update is called once per frame
  void Update()
  {
    coinText.text = player.Money.ToString();
  }
}
