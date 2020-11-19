using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantSystem : MonoBehaviour
{
    private static PlantSystem _instance;
    public static PlantSystem Instance { get { return _instance; } } // a singleton

    private GridSystem gridSystem;

    public GameObject palmPrefab;
    public Transform palmParent;

    public List<GameObject> selectedForestObjList;
    public GameObject selectedChildObj;
    public GameObject selectedDeadTreeObj;

    public Vector3 dragStartPosition;
    public Vector3 dragFinalPosition;

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
    }

    void OnEnable()
    {
        selectedForestObjList = new List<GameObject>();
    }

    void Start()
    {
        gridSystem = GridSystem.Instance;
        selectedForestObjList = new List<GameObject>();
    }

    public void SelectDeselectForestObj(GameObject selectedObj)
    {
        if (selectedObj)
        {
            if (selectedObj.transform.GetChild(0).gameObject.activeSelf)  // if forest tree obj is not highlighted then highlight it
            {
                if (selectedForestObjList.Count > 0)
                { // allow one tree only
                    StopAllHighlight();
                }
                selectedChildObj = selectedObj.transform.GetChild(1).gameObject;
                selectedChildObj.SetActive(true);
                selectedObj.transform.GetChild(0).gameObject.SetActive(false);
                selectedForestObjList.Add(selectedObj);
            }
            else // unhighlight it
            {
                selectedChildObj = selectedObj.transform.GetChild(0).gameObject;
                selectedChildObj.SetActive(true);
                selectedObj.transform.GetChild(1).gameObject.SetActive(false);
                selectedForestObjList.Remove(selectedObj);
            }

        }

    }

    public void UnhighlightForestObj(GameObject selectedObj)
    {
        if (selectedObj)
        {
            selectedChildObj = selectedObj.transform.GetChild(0).gameObject;
            selectedChildObj.SetActive(true);
            selectedObj.transform.GetChild(1).gameObject.SetActive(false);
        }
        selectedForestObjList.Remove(selectedObj);
    }

    public void StopAllHighlight()
    { // fix bug where need to unhighlight but without other condition
        foreach (GameObject selectedObj in selectedForestObjList)
        {
            if (selectedObj)
            {
                selectedChildObj = selectedObj.transform.GetChild(0).gameObject;
                selectedChildObj.SetActive(true);
                selectedObj.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        selectedForestObjList.Clear();
    }

    public void PlacePalmObj()
    {
        foreach (GameObject selectedObj in selectedForestObjList)
        {
            if (selectedObj)
            {
                Vector2Int selectedPoint = gridSystem.GetGridPointByPosition(selectedObj.transform.position);

                if (gridSystem.objectOnGrid[selectedPoint.x, selectedPoint.y].CompareTag("Forest Tree"))
                {
                    GameObject newObj = Instantiate(palmPrefab,
                                                gridSystem.GetPositionByGridPoint(selectedPoint.x, selectedPoint.y),
                                                Quaternion.identity,
                                                palmParent);
                    newObj.name = "Palm Tree (" + selectedPoint.x.ToString() + ", " + selectedPoint.y.ToString() + ")";
                    gridSystem.objectOnGrid[selectedPoint.x, selectedPoint.y] = newObj;

                    Destroy(selectedObj);
                }
            }
        }
        StopAllHighlight();
        selectedChildObj = null;
    }

    public void ReplantPalmObj()
    {
        if (selectedDeadTreeObj.CompareTag("Palm Oil Tree"))
        {
            Vector2Int selectedPoint = gridSystem.GetGridPointByPosition(selectedDeadTreeObj.transform.position);
            GameObject newObj = Instantiate(palmPrefab,
                                        selectedDeadTreeObj.transform.position,
                                        Quaternion.identity,
                                        palmParent);
            newObj.name = "Palm Tree (" + selectedPoint.x.ToString() + ", " + selectedPoint.y.ToString() + ")";
            gridSystem.objectOnGrid[selectedPoint.x, selectedPoint.y] = newObj;

            Destroy(selectedDeadTreeObj);
        }
    }

    private void OnDisable()
    {
        StopAllHighlight();
        selectedForestObjList = null;
        selectedChildObj = null;
    }

}
