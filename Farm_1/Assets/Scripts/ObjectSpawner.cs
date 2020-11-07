using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject forestPrefab;
    public GameObject palmPrefab;
    public GameObject factoryPrefab;
    public Transform forestParent;
    public Transform palmParent;
    public Transform factoryParent;

    private GridSystem gridSystem;

    void Start()
    {
        SpawnForest();
    }

    private void SpawnForest()
    {
        gridSystem = GridSystem.Instance;
        for (int x = 0; x <= gridSystem.objectOnGrid.GetUpperBound(0); x++)
        {
            for (int z = 0; z <= gridSystem.objectOnGrid.GetUpperBound(1); z++)
            {
                if (CheckForestSpawnPermit(x, z))
                {
                    GameObject tempObj = Instantiate(forestPrefab,
                                                        gridSystem.getPositionByGridPoint(x, z),
                                                        Quaternion.identity,
                                                        forestParent);
                    tempObj.name = "Forest Tree (" + x.ToString() + ", " + z.ToString() + ")";
                    // Color tempColor = tempObj.transform.GetChild(0).GetComponent<Renderer>().material.color;
                    // Color tempColor = tempObj.GetComponentInChildren<Renderer>().material.color;
                    // tempColor.a = 0.2f;
                    // tempObj.transform.GetChild(0).GetComponent<Renderer>().material.color = tempColor;
                    // tempObj.GetComponentInChildren<Renderer>().material.color = tempColor;
                    gridSystem.objectOnGrid[x, z] = tempObj;
                }
            }
        }
    }

    // true if forest tree can generate at that point
    private bool CheckForestSpawnPermit(int x, int z)
    {
        if ((x >= 40 && x <= 60) && (z >= 40 && z <= 60))
        {
            return false;
        }
        return true;
    }

    private bool CheckPalmSpawnPermit() {
        return true;
    }

}
