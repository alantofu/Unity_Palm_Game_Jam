using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player
{
  private static Player instance = null;
  private static readonly object padlock = new object();
  private int money = 100;
  private int rep = 100;
  private int oil = 100;

  Player()
  {
  }

  public static Player Instance
  {
    get
    {
      lock (padlock)
      {
        if (instance == null)
        {
          instance = new Player();
        }
        return instance;
      }
    }
  }

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
  public int reduceMoney(int amount)
  {
    int currAmount = Money;
    currAmount -= amount;
    Money = currAmount;
    return currAmount;
  }
  public int addMoney(int amount)
  {
    int currAmount = Money;
    currAmount += amount;
    Money = currAmount;
    return currAmount;
  }
  public int reduceRep(int amount)
  {
    int currAmount = Rep;
    currAmount -= amount;
    Rep = currAmount;
    return currAmount;
  }
  public int addRep(int amount)
  {
    int currAmount = Rep;
    currAmount += amount;
    Rep = currAmount;
    return currAmount;
  }
  public int reduceOil(int amount)
  {
    int currAmount = Oil;
    currAmount -= amount;
    Oil = currAmount;
    return currAmount;
  }
  public int addOil(int amount)
  {
    int currAmount = Oil;
    currAmount += amount;
    Oil = currAmount;
    return currAmount;
  }
}