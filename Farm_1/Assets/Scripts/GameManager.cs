using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlacementSystem placementSystem;
    private BuildSystem buildSystem;
    private Camera mainCam;

    private void Awake()
    {

    }

    private void Start()
    {
        mainCam = Camera.main;
        placementSystem = PlacementSystem.Instance;
        buildSystem = BuildSystem.Instance;
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        placementSystem.gameObject.SetActive(false);
        buildSystem.gameObject.SetActive(false);
    }

    public void triggerPlacementSystem(bool trigger)
    {
        mainCam.GetComponent<PostCameraProcess>().toggle = trigger;
        placementSystem.gameObject.SetActive(trigger);
    }

    public void triggerBuildSystem(bool trigger)
    {
        buildSystem.gameObject.SetActive(trigger);
        buildSystem.CreateFactory();
    }

}
