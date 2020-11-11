using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// this class was replaced by FactoryDisplayManager.cs
public class LipstickNumDisplay : MonoBehaviour
{
  public TMP_Text text;
  public Factory lf;
  void Start()
  {
    // lf = Factory.Instance;
    // Debug.Log(lf);
    // Debug.Log(lf.ProductAmount);
    // text.text = lf.ProductAmount.ToString();
    // int oil = lf.ProductAmount*lf.OilPerProduct;
    // int profit = lf.ProductAmount*lf.ProfitReturnPerProduct;
    // Debug.Log(oil.ToString());
    // Debug.Log(profit.ToString());
  }
  // Update is called once per frame
  void Update()
  {
    text.text = lf.ProductAmount.ToString();
  }
}
