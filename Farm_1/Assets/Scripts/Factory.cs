using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public int oilPerProduct;
    public int profitReturnPerProduct;
    public int timeRequiredPerProduct;

    public int productAmount = 0;
    public int totalOil = 0;
    public int totalProfit = 0;
    public int totalTime = 0;

    public void DecProductAmount()
    {
        Debug.Log("method reduceProductAmount called.");
        int tempAmount = productAmount;
        tempAmount -= 1;
        if (tempAmount >= 0)
        {
            Debug.Log("amount reduced.");
            productAmount = tempAmount;
            totalOil = productAmount * oilPerProduct;
            totalProfit = productAmount * profitReturnPerProduct;
            totalTime = productAmount * timeRequiredPerProduct;
        }
    }
    
    public void IncProductAmount()
    {
        Debug.Log("method addProductAmount called.");
        int tempAmount = productAmount;
        tempAmount += 1;
        if (tempAmount >= 0)
        {
            Debug.Log("amount added.");
            productAmount = tempAmount;
            totalOil = productAmount * oilPerProduct;
            totalProfit = productAmount * profitReturnPerProduct;
            totalTime = productAmount * timeRequiredPerProduct;
        }
    }

    public void OnResetValues()
    {
        productAmount = 0;
        totalOil = 0;
        totalProfit = 0;
        totalTime = 0;
    }
}