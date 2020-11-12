﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BuildSystem buildSystem;
    private PlantSystem plantSystem;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        buildSystem = BuildSystem.Instance;
        plantSystem = PlantSystem.Instance;
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        buildSystem.gameObject.SetActive(false);
        plantSystem.gameObject.SetActive(true);
    }

    public void showGridline()
    {
        mainCam.GetComponent<PostCameraProcess>().toggle = true;
    }

    public void hideGridline()
    {
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
    }

    public void planLipstickFactory()
    {
        triggerBuildSystem(true, 1);
    }

    public void buildLipstickFactory()
    {
        triggerBuildSystem(false, 1);
    }

    public void planChocolateFactory()
    {
        triggerBuildSystem(true, 0);
    }

    public void buildChocolateFactory()
    {
        triggerBuildSystem(false, 0);
    }

    private void triggerBuildSystem(bool trigger, int index)
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

    public void cancelBuildSystem() {
        buildSystem.CancelFactoryPlanning();
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        buildSystem.gameObject.SetActive(false);
    }

    public void onPlantSystem()
    {
        triggerPlantSystem(true);
    }

    public void offPlantSystem()
    {
        triggerPlantSystem(false);
    }

    private void triggerPlantSystem(bool trigger)
    {
        plantSystem.gameObject.SetActive(trigger);
    }

    public void plantPalmOilTree()
    {
        plantSystem.PlacePalmObj();
    }



}
