using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlacementSystem placementSystem;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        placementSystem = PlacementSystem.Instance;
    }

    private void Start()
    {
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        placementSystem.gameObject.SetActive(false);
    }

    public void triggerPlacementSystem(bool trigger)
    {
        mainCam.GetComponent<PostCameraProcess>().toggle = trigger;
        placementSystem.gameObject.SetActive(trigger);
    }


}
