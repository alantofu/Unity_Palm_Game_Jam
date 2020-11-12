using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private static Player _instance;
  public static Player Instance { get { return _instance; } } // a singleton
  // private static Player instance = null;
  public int money = 10000;
  public int rep = 15;
  public int oil = 12000;
  // https://gist.github.com/mathiassoeholm/6744344
  // Or you can search for a gameobject and assign it to a variable
  public GameObject factory;
  public GameObject chocolateFac;
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
    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
    }
    else
    {
      _instance = this;
    }
  }
  void Start()
  {
    // You can find a gameobject by name
  }
  void Update()
  {
    // Now to acces a component on the gameobject,
    // simply use the generic GetComponent method
  }
  public void factoryOperation()
  {
    Factory fac = null;
    Factory fac2 = null;
    factory = GameObject.Find("LipstickFac");
    chocolateFac = GameObject.Find("ChocolateFac");
    if (factory != null)
    {
      fac = factory.GetComponent<Factory>();
      reduceOil(fac.TotalOil);
      fac.TotalOil = 0;
      fac.ProductAmount = 0;
      fac.TotalTime = 0;
      fac.TotalProfit = 0;
    }
    if (chocolateFac != null)
    {
      fac2 = chocolateFac.GetComponent<Factory>();
      reduceOil(fac2.TotalOil);
      fac2.TotalOil = 0;
      fac2.ProductAmount = 0;
      fac2.TotalTime = 0;
      fac2.TotalProfit = 0;
    }
    // pass the profit amount and time required to the cube
    // need to pass a Cube game object to this script first

    // Debug.Log("compare total profit");
    // Debug.Log(fac.TotalProfit);
    // Debug.Log(fac.TotalProfit);
  }
  public void reduceMoney(int amount)
  {
    int currAmount = Money;
    currAmount -= amount;
    if (currAmount >= 0)
    {
      Money = currAmount;
      Debug.Log("The money amount deducted.");
    }
    else
    {
      Debug.Log("The amount will turn -ve after deduction.");
    }
    // return currAmount;
  }
  public void addMoney(int amount)
  {
    int currAmount = Money;
    currAmount += amount;
    Money = currAmount;
    Debug.Log("The money amount added.");
    // return currAmount;
  }
  public void reduceRep(int amount)
  {
    int currAmount = Rep;
    currAmount -= amount;
    if (currAmount >= 1)
    {
      Rep = currAmount;
    }
    else
    {
      Debug.Log("The rep will turn zero or -ve after deduction.");
    }
    // return currAmount;
  }
  public void addRep(int amount)
  {
    int currAmount = Rep;
    currAmount += amount;
    Rep = currAmount;
    Debug.Log("The rep amount added.");
    // return currAmount;
  }
  public void reduceOil(int amount)
  {
    int currAmount = Oil;
    currAmount -= amount;
    if (currAmount >= 0)
    {
      Oil = currAmount;
    }
    else
    {
      Debug.Log("The Oil will turn -ve after deduction.");
    }
    // return currAmount;
  }
  public void addOil(int amount)
  {
    int currAmount = Oil;
    currAmount += amount;
    Oil = currAmount;
    Debug.Log("The oil amount added.");
    // return currAmount;
  }
}