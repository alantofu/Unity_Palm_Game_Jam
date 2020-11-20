using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private static Factory _instance;
    public static Factory Instance { get { return _instance; } } // a singleton
    private int productAmount = 0;
    public int oilPerProduct;
    public int totalOil = 0;
    public int profitReturnPerProduct;
    public int totalProfit = 0;
    public int timeRequiredPerProduct;
    public int totalTime = 0;

    public GameObject selectedFactory;

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
    public int TotalOil
    {
        get { return totalOil; }
        set { totalOil = value; }
    }
    public int TotalProfit
    {
        get { return totalProfit; }
        set { totalProfit = value; }
    }
    public int TotalTime
    {
        get { return totalTime; }
        set { totalTime = value; }
    }

    public int TimeRequiredPerProduct
    {
        get { return timeRequiredPerProduct; }
        set { timeRequiredPerProduct = value; }
    }
    private void Awake()
    {
        // singleton condition
        // if (_instance != null && _instance != this)
        // {
        //   Destroy(this.gameObject);
        //   Debug.Log("THIS GAME OBJECT DESTROYED");
        // }
        // else
        // {
        _instance = this;
        // }
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
            TotalOil = ProductAmount * OilPerProduct;
            TotalProfit = ProductAmount * ProfitReturnPerProduct;
            TotalTime = ProductAmount * TimeRequiredPerProduct;
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
            TotalOil = ProductAmount * OilPerProduct;
            TotalProfit = ProductAmount * ProfitReturnPerProduct;
            TotalTime = ProductAmount * TimeRequiredPerProduct;
        }
        // return currAmount;
    }

    public void StartManufacturing()
    {
        if (selectedFactory)
        {
          selectedFactory.GetComponent<Earning>().enabled = true;
          selectedFactory.GetComponent<Earning>().StartManufacturingProcess(totalProfit, totalTime);
        }
    }

    void OnEnable()
    {
        productAmount = 0;
        totalOil = 0;
        totalProfit = 0;
        totalTime = 0;
    }
}