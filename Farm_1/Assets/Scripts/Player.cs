using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance { get { return _instance; } } // a singleton
                                                                // private static Player instance = null;
    public int money = 10000;
    public int reputation = 15;
    public int oil = 12000;

    private ManufactureSystem manufactureSystem;

    void Awake()
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
        manufactureSystem = ManufactureSystem.Instance;
    }

    public void ReduceMoney(int amount)
    {
        int tempMoney = money - amount;
        if (tempMoney >= 0)
        {
            money = tempMoney;
            Debug.Log("The money amount deducted.");
        }
        else
        {
            Debug.Log("The amount will turn -ve after deduction.");
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("The money amount added.");
    }

    public void ReduceRep(int amount)
    {
        int tempRep = reputation - amount;
        if (tempRep >= 1)
        {
            reputation = tempRep;
        }
        else
        {
            Debug.Log("The rep will turn zero or -ve after deduction.");
        }
    }

    public void AddRep(int amount)
    {
        reputation += amount;
        Debug.Log("The rep amount added.");
    }

    public void ReduceOil(int amount)
    {
        int currAmount = oil - amount;
        if (currAmount >= 0)
        {
            oil = currAmount;
        }
        else
        {
            Debug.Log("The Oil will turn -ve after deduction.");
        }
    }

    public void AddOil(int amount)
    {
        oil += amount;
        Debug.Log("The oil amount added.");
    }
}