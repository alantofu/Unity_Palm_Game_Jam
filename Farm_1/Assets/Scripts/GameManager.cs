﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CameraController cameraController;
    private BuildSystem buildSystem;
    private PlantSystem plantSystem;
    private SelectSystem selectSystem;
    private Camera mainCam;

    public GameObject sidePanel;
    public GameObject confirmationPanel;
    public GameObject buySeedPanel;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Start()
    {
        cameraController = CameraController.Instance;
        buildSystem = BuildSystem.Instance;
        plantSystem = PlantSystem.Instance;
        selectSystem = SelectSystem.Instance;
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        buildSystem.gameObject.SetActive(false);
        plantSystem.gameObject.SetActive(false);
        selectSystem.gameObject.SetActive(true);
        if (buySeedPanel == null)
        {
            buySeedPanel = GameObject.Find("Canvas/Panel (BUY SEED)");
        }
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

    public void OnRemovingForestTree()
    {
        cameraController.PanningToDefault();
        plantSystem.gameObject.SetActive(true);
    }

    public void OnPlantingPalmOilTree()
    {
        plantSystem.PlacePalmObj();
    }

    public void ConfirmationPanelOnAccept()
    // confirm button will not close confirmation panel automatically
    // side panel will not show automatically
    {
        if (buildSystem.gameObject.activeSelf)
        {
            if (buildSystem.canBuild) // if can place the factory obj
            {
                buildSystem.PlaceFactoryObj();
                buildSystem.gameObject.SetActive(false);
                sidePanel.gameObject.SetActive(true);
                confirmationPanel.gameObject.SetActive(false);
            }
            else {
                return;
            }
        }
        else if (plantSystem.gameObject.activeSelf)
        {
            if (plantSystem.selectedForestObjList.Count > 0)
            {
                buySeedPanel.SetActive(true);
            }
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

    public void OnFocusingBackToCenter() {
        cameraController.PanningToDefault();
        cameraController.ZoomingToDefault();
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
