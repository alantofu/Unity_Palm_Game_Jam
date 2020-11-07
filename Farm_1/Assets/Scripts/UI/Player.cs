using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player
{
  private static Player instance = null;
  private static readonly object padlock = new object();
  private int money = 100;

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
  public int reduceMoney(int amount)
  {
    int currAmount = Money;
    currAmount -= amount;
    Money = currAmount;
    return currAmount;
  }
}