using System.Collections;
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
        plantSystem.gameObject.SetActive(false);
    }

    public void OnBuyingLipstickFac()
    {
        plantSystem.gameObject.SetActive(false);
        triggerBuildSystem(true, 1);
    }

    public void OnBuyingChocolatekFac()
    {
        plantSystem.gameObject.SetActive(false);
        triggerBuildSystem(true, 0);
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
