using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private static Player _instance;
  public static Player Instance { get { return _instance; } } // a singleton
  // private static Player instance = null;
  private int money = 100;
  private int rep = 110;
  private int oil = 120;

  public int Money
  {
    get { return money; }
    set { money = value; }
  }
  public int Rep
  {
    get { return rep; }
    set { rep = value; }
  }
  public int Oil
  {
    get { return oil; }
    set { oil = value; }
  }
  private void Awake()
  {
    // singleton condition
    if (_instance != null && _instance != this){
      Destroy(this.gameObject);
    }
    else
    {
      _instance = this;
    }
  }


  public void reduceMoney(int amount)
  {
    int currAmount = Money;
    currAmount -= amount;
    Money = currAmount;
    // return currAmount;
  }
  public void addMoney(int amount)
  {
    int currAmount = Money;
    currAmount += amount;
    Money = currAmount;
    // return currAmount;
  }
  public void reduceRep(int amount)
  {
    int currAmount = Rep;
    currAmount -= amount;
    Rep = currAmount;
    // return currAmount;
  }
  public void addRep(int amount)
  {
    int currAmount = Rep;
    currAmount += amount;
    Rep = currAmount;
    // return currAmount;
  }
  public void reduceOil(int amount)
  {
    int currAmount = Oil;
    currAmount -= amount;
    Oil = currAmount;
    // return currAmount;
  }
  public void addOil(int amount)
  {
    int currAmount = Oil;
    currAmount += amount;
    Oil = currAmount;
    // return currAmount;
  }
}