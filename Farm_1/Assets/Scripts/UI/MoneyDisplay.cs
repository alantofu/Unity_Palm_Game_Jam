using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyDisplay : MonoBehaviour
{
  public Text coinText;
  void Start()
  {
    coinText.text = Player.Instance.Money.ToString();
  }
  // Update is called once per frame
  void Update()
  {
    coinText.text = Player.Instance.Money.ToString();
  }
  void OnMouseOver(){
    Debug.Log("on mouse over");
    if (Input.GetMouseButtonDown(0))
    {
      Player.Instance.reduceMoney(5);
    }
  }
}
