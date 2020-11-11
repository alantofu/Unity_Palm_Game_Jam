using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class FactoryDisplayManager : MonoBehaviour
{
  public TMP_Text productNum;
  public TMP_Text oilAmount;
  public TMP_Text profitReturn;
  public TMP_Text timeRequired;
  public Factory lf;
  void Start()
  {
    lf = Factory.Instance;
    productNum.text = lf.ProductAmount.ToString();
    // int oil = lf.ProductAmount*lf.OilPerProduct;
    // int profit = lf.ProductAmount*lf.ProfitReturnPerProduct;
    // int time = lf.ProductAmount*lf.TimeRequiredPerProduct;
    oilAmount.text = lf.TotalOil.ToString();
    profitReturn.text = lf.TotalProfit.ToString();
    timeRequired.text = lf.TotalTime.ToString();
  }
  // Update is called once per frame
  void Update()
  {
    productNum.text = lf.ProductAmount.ToString();
    // int oil = lf.ProductAmount*lf.OilPerProduct;
    // int profit = lf.ProductAmount*lf.ProfitReturnPerProduct;
    // int time = lf.ProductAmount*lf.TimeRequiredPerProduct;
    oilAmount.text = lf.TotalOil.ToString();
    profitReturn.text = lf.TotalProfit.ToString();
    timeRequired.text = lf.TotalTime.ToString();
    // oilAmount.text = oil.ToString();
    // profitReturn.text = profit.ToString();
    // timeRequired.text = time.ToString();
  }
}
