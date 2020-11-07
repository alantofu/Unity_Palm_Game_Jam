using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinsDisplay : MonoBehaviour
{
  public Text coinText;
  private int money = 10;
  // public Player player;
  void Start()
  {
    // coinText.text = "Coins: " + money;
    coinText.text = "Coins: " + Player.Instance.Money;
    // player = GetComponent<Player>();
  }
  // Update is called once per frame
  void Update()
  {
    // coinText.text = "Coins: " + money;
    coinText.text = "Coins: " + Player.Instance.Money;
    // coinText.text = "Coins: " + Player.Money;
    if (Input.GetMouseButtonDown(0))
    {
      Debug.Log("mouse down");
      // money--;
      Player.Instance.reduceMoney(5);
    }
  }
}
