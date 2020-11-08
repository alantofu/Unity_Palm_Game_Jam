using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BuildSystem buildSystem;
    private PlantSystem plantSystem;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        buildSystem = BuildSystem.Instance;
        plantSystem = PlantSystem.Instance;
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        buildSystem.gameObject.SetActive(false);
        plantSystem.gameObject.SetActive(true);
    }

    public void triggerPlacementSystem(bool trigger)
    {
        mainCam.GetComponent<PostCameraProcess>().toggle = trigger;
    }

    public void triggerBuildSystem(bool trigger)
    {
        mainCam.GetComponent<PostCameraProcess>().toggle = trigger;
        buildSystem.gameObject.SetActive(trigger);
        if(trigger) {
            buildSystem.InstantiateFactoryObj();
        }
        else{
            buildSystem.PlaceFactoryObj();
        }
    }

    public void triggerPlantSystem(bool trigger)
    {
        plantSystem.gameObject.SetActive(trigger);
    }

    public void plantPalmOilTree() {
        plantSystem.PlacePalmObj();
    }

}
