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
        gridSystem = GridSystem.Instance;
        SpawnForest();
        SpawnPalmOil();
    }

    private void SpawnForest()
    {
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
                    Vector3 randomPosition = new Vector3(Random.Range(0.0f, 0.5f), 0.545f, Random.Range(0.0f, 0.5f));
                    tempObj.transform.GetChild(0).transform.position = tempObj.transform.position + randomPosition;
                    tempObj.transform.GetChild(1).transform.position = tempObj.transform.position + randomPosition;
                    // Debug.Log(tempObj + " - " + tempObj.transform.GetChild(0).transform.position);
                    gridSystem.objectOnGrid[x, z] = tempObj;
                }
            }
        }
    }

    private void SpawnPalmOil()
    {
        for (int x = 49; x <= 50; x++)
        {
            for (int z = 49; z <= 50; z++)
            {
                GameObject tempObj = Instantiate(palmPrefab,
                                                    gridSystem.getPositionByGridPoint(x, z),
                                                    Quaternion.identity,
                                                    palmParent);
                tempObj.name = "Palm Tree (" + x.ToString() + ", " + z.ToString() + ")";
                gridSystem.objectOnGrid[x, z] = tempObj;
                tempObj.GetComponent<Growing>().grew = true;
            }
        }
    }

    // true if forest tree can generate at that point
    private bool CheckForestSpawnPermit(int x, int z)
    {
        if ((x >= 49 && x <= 50) && (z >= 49 && z <= 50))
        {
            return false;
        }
        return true;
    }

    private bool CheckPalmSpawnPermit()
    {
        return true;
    }

}
