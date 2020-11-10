using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipstickFactory : MonoBehaviour
{
  private static LipstickFactory _instance;
  public static LipstickFactory Instance { get { return _instance; } } // a singleton
  private int productAmount = 0;
  private int oilPerProduct = 95;
  private int marketPricePerProduct = 100;

  public int ProductAmount
  {
    get { return productAmount; }
    set { productAmount = value; }
  }
  public int MarketPricePerProduct
  {
    get { return marketPricePerProduct; }
    set { marketPricePerProduct = value; }
  }
  private void Awake()
  {
    // singleton condition
    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
    }
    else
    {
      _instance = this;
    }
  }


  public void reduceProductAmount()
  {
    int currAmount = ProductAmount;
    currAmount -= 1;
    if (currAmount >= 0)
    {
      ProductAmount = currAmount;
    }
    // return currAmount;
  }
  public void addProductAmount()
  {
    int currAmount = ProductAmount;
    currAmount += 1;
    if (currAmount >= 0)
    {
      ProductAmount = currAmount;
    }
    // return currAmount;
  }
}