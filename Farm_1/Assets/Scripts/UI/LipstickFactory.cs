using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipstickFactory : MonoBehaviour
{
  private static LipstickFactory _instance;
  public static LipstickFactory Instance { get { return _instance; } } // a singleton
  private int productAmount = 0;
  public int oilPerProduct;
  public int profitReturnPerProduct;
  public int timeRequiredPerProduct;

  public int ProductAmount
  {
    get { return productAmount; }
    set { productAmount = value; }
  }
  public int ProfitReturnPerProduct
  {
    get { return profitReturnPerProduct; }
    set { profitReturnPerProduct = value; }
  }
  public int OilPerProduct
  {
    get { return oilPerProduct; }
    set { oilPerProduct = value; }
  }
  public int TimeRequiredPerProduct
  {
    get { return timeRequiredPerProduct; }
    set { timeRequiredPerProduct = value; }
  }
  private void Awake()
  {
    // singleton condition
    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
      Debug.Log("THIS GAME OBJECT DESTROYED");
    }
    else
    {
      _instance = this;
      Debug.Log("_instance = this;");
    }
  }

  public void reduceProductAmount()
  {
    Debug.Log("method reduceProductAmount called.");
    int currAmount = ProductAmount;
    currAmount -= 1;
    if (currAmount >= 0)
    {
      Debug.Log("amount reduced.");
      ProductAmount = currAmount;
    }
    // return currAmount;
  }
  public void addProductAmount()
  {
    Debug.Log("method addProductAmount called.");
    int currAmount = ProductAmount;
    currAmount += 1;
    if (currAmount >= 0)
    {
      Debug.Log("amount added.");
      ProductAmount = currAmount;
    }
    // return currAmount;
  }
}