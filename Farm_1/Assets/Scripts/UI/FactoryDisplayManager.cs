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
    public ManufactureSystem manufactureSystem;

    void Start()
    {
        manufactureSystem = ManufactureSystem.Instance;
        productNum.text = manufactureSystem.selectedFactoryObject.GetComponent<Factory>().productAmount.ToString();
        oilAmount.text = manufactureSystem.selectedFactoryObject.GetComponent<Factory>().totalOil.ToString();
        profitReturn.text = manufactureSystem.selectedFactoryObject.GetComponent<Factory>().totalProfit.ToString();
        timeRequired.text = manufactureSystem.selectedFactoryObject.GetComponent<Factory>().totalTime.ToString();
    }

    void Update()
    {
        productNum.text = manufactureSystem.selectedFactoryObject.GetComponent<Factory>().productAmount.ToString();
        oilAmount.text = manufactureSystem.selectedFactoryObject.GetComponent<Factory>().totalOil.ToString();
        profitReturn.text = manufactureSystem.selectedFactoryObject.GetComponent<Factory>().totalProfit.ToString();
        timeRequired.text = manufactureSystem.selectedFactoryObject.GetComponent<Factory>().totalTime.ToString();
    }
}
