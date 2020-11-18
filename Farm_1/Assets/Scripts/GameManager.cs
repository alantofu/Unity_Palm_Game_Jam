﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BuildSystem buildSystem;
    private PlantSystem plantSystem;
    private SelectSystem selectSystem;
    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Start()
    {
        buildSystem = BuildSystem.Instance;
        plantSystem = PlantSystem.Instance;
        selectSystem = SelectSystem.Instance;
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        buildSystem.gameObject.SetActive(false);
        plantSystem.gameObject.SetActive(false);
        selectSystem.gameObject.SetActive(true);
    }

    public void OnBuyingLipstickFac()
    {
        plantSystem.gameObject.SetActive(false);
        selectSystem.gameObject.SetActive(false);
        triggerBuildSystem(true, 0);
    }

    public void OnBuyingChocolateFac()
    {
        plantSystem.gameObject.SetActive(false);
        selectSystem.gameObject.SetActive(false);
        triggerBuildSystem(true, 1);
    }

    public void OnPlantingPalmOilTree()
    {
        plantSystem.gameObject.SetActive(true);
    }

    public void ConfirmationPanelOnAccept()
    {
        if (buildSystem.gameObject.activeSelf)
        {
            buildSystem.PlaceFactoryObj();
            buildSystem.gameObject.SetActive(false);
        }
        else if (plantSystem.gameObject.activeSelf)
        {
            plantSystem.PlacePalmObj();
            plantSystem.gameObject.SetActive(false);
        }
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        selectSystem.gameObject.SetActive(true);
    }

    public void ConfirmationPanelOnReject()
    {
        if (buildSystem.gameObject.activeSelf)
        {
            buildSystem.CancelFactoryPlanning();
            buildSystem.gameObject.SetActive(false);
        }
        else if (plantSystem.gameObject.activeSelf)
        {
            plantSystem.StopAllHighlight();
            plantSystem.gameObject.SetActive(false);
        }
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        selectSystem.gameObject.SetActive(true);
    }

    void triggerBuildSystem(bool trigger, int index)
    {
        mainCam.GetComponent<PostCameraProcess>().toggle = trigger;
        buildSystem.gameObject.SetActive(trigger);
        if (trigger)
        {
            buildSystem.InstantiateFactoryObj(index);
        }
        else
        {
            buildSystem.PlaceFactoryObj();
        }
    }
}
