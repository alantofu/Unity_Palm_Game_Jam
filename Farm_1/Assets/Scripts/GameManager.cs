using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } } // a singleton

    private Player player;
    private CameraController cameraController;
    private BuildSystem buildSystem;
    private PlantSystem plantSystem;
    private SelectSystem selectSystem;
    private ManufactureSystem manufactureSystem;
    private AudioManager audioManager;
    private Camera mainCam;

    public GameObject sidePanel;
    public GameObject confirmationPanel;
    public GameObject buySeedPanel;
    public GameObject forestHCSPanel;
    public GameObject replantTreePanel;
    public GameObject lipstickFactoryPanel;
    public GameObject chocolateFactoryPanel;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        mainCam = Camera.main;
    }

    void Start()
    {
        player = Player.Instance;
        cameraController = CameraController.Instance;
        buildSystem = BuildSystem.Instance;
        plantSystem = PlantSystem.Instance;
        selectSystem = SelectSystem.Instance;
        manufactureSystem = ManufactureSystem.Instance;
        audioManager = AudioManager.Instance;

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
        plantSystem.gameObject.SetActive(true);
    }

    public void OnPlantingPalmOilTree()
    {
        plantSystem.PlacePalmObj();
    }

    public void OnBiomassMethod()
    {
        plantSystem.gameObject.SetActive(true);
        plantSystem.ReplantPalmObj();
        plantSystem.gameObject.SetActive(false);
    }

    public void OnIncreasingProductAmount()
    {
        if (manufactureSystem.selectedFactoryObject)
        {
            manufactureSystem.selectedFactoryObject.GetComponent<Factory>().IncProductAmount();
        }
    }

    public void OnDecreasingProductAmount()
    {
        if (manufactureSystem.selectedFactoryObject)
        {
            manufactureSystem.selectedFactoryObject.GetComponent<Factory>().DecProductAmount();
        }
    }

    public void OnStartingManufacturing()
    {
        manufactureSystem.selectedFactoryObject.GetComponent<Earning>().StartManufacturingProcess();
        player.ReduceOil(manufactureSystem.selectedFactoryObject.GetComponent<Factory>().totalOil);
    }

    public void ConfirmationPanelOnAccept()
    // confirm button will not close confirmation panel automatically
    // side panel will not show automatically
    {
        if (buildSystem.gameObject.activeSelf)
        {
            if (buildSystem.canBuild) // if can place the factory obj
            {
                audioManager.PlaySound("Build Audio");

                buildSystem.PlaceFactoryObj();
                buildSystem.gameObject.SetActive(false);
                sidePanel.gameObject.SetActive(true);
                confirmationPanel.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }
        else if (plantSystem.gameObject.activeSelf)
        {
            if (plantSystem.selectedRemovableObjList.Count > 0)
            {
                if (plantSystem.selectedRemovableObj.CompareTag("Forest Tree"))
                {
                    forestHCSPanel.SetActive(true);
                }
                else
                {
                    buySeedPanel.SetActive(true);
                }
            }
        }
        mainCam.GetComponent<PostCameraProcess>().toggle = false;
        selectSystem.gameObject.SetActive(true);
    }

    public void ConfirmationPanelOnReject()
    {
        audioManager.PlaySound("Cancel Audio");
        
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

    public void OnFocusingBackToCenter()
    {
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
