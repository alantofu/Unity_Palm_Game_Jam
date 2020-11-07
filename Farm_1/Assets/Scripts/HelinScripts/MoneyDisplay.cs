using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyDisplay : MonoBehaviour
{
  public Text coinText;
  public Player player;
  void Start()
  {
    player = Player.Instance;
  }
  // Update is called once per frame
  void Update()
  {
    coinText.text = player.Money.ToString();
  }
}
