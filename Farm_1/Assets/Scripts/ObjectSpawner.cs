using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject forestPrefab;
    public GameObject palmPrefab;
    public Transform forestParent;
    public Transform palmParent;

    public List<Vector2Int> palmOilTreePointList;
    public List<Vector2Int> emptyPointList;

    private GridSystem gridSystem;

    void Awake()
    {
        palmOilTreePointList = new List<Vector2Int>()
        {
            new Vector2Int(53, 49),
            new Vector2Int(53, 50),
            new Vector2Int(54, 49),
            new Vector2Int(54, 50),
        };
        emptyPointList = new List<Vector2Int>()
        {
        };
        for (int x = 43; x <= 48; x++)
        {
            for (int y = 47; y <= 52; y++)
            {
                emptyPointList.Add(new Vector2Int(x, y));
            }
        }
    }

    void Start()
    {
        gridSystem = GridSystem.Instance;
        if (forestPrefab == null)
        {
            forestPrefab = Resources.Load("Prefabs/Forest Tree", typeof(GameObject)) as GameObject;
        }
        if (palmPrefab == null)
        {
            palmPrefab = Resources.Load("Prefabs/Palm Tree", typeof(GameObject)) as GameObject;
        }
        SpawnForest();
        SpawnPalmOil();
    }

    private void SpawnForest()
    {
        for (int x = 0; x <= gridSystem.objectOnGrid.GetUpperBound(0); x++)
        {
            for (int z = 0; z <= gridSystem.objectOnGrid.GetUpperBound(1); z++)
            {
                if (!CheckPalmSpawnPoint(x, z) && !CheckEmptyPoint(x, z))
                {
                    GameObject tempObj = Instantiate(forestPrefab,
                                                        gridSystem.GetPositionByGridPoint(x, z),
                                                        Quaternion.identity,
                                                        forestParent);
                    tempObj.name = "Forest Tree (" + x.ToString() + ", " + z.ToString() + ")";
                    Vector3 randomPosition = new Vector3(Random.Range(0.0f, 0.4f), 0f, Random.Range(0.0f, 0.4f));
                    tempObj.transform.GetChild(0).transform.position = tempObj.transform.position + randomPosition;
                    tempObj.transform.GetChild(1).transform.position = tempObj.transform.position + randomPosition;
                    gridSystem.objectOnGrid[x, z] = tempObj;
                }
            }
        }
    }

    private void SpawnPalmOil()
    {
        foreach (Vector2Int spawnPoint in palmOilTreePointList)
        {
            GameObject tempObj = Instantiate(palmPrefab,
                                                gridSystem.GetPositionByGridPoint(spawnPoint.x, spawnPoint.y),
                                                Quaternion.identity,
                                                palmParent);
            tempObj.name = "Palm Tree (" + spawnPoint.x.ToString() + ", " + spawnPoint.y.ToString() + ")";
            gridSystem.objectOnGrid[spawnPoint.x, spawnPoint.y] = tempObj;
            tempObj.GetComponent<Growing>().grew = true;
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

    // true if palm tree should spawn at that point
    private bool CheckPalmSpawnPoint(int x, int z)
    {
        foreach (Vector2Int checkPoint in palmOilTreePointList)
        {
            if (checkPoint.x == x && checkPoint.y == z)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckEmptyPoint(int x, int z)
    {
        foreach (Vector2Int checkPoint in emptyPointList)
        {
            if (checkPoint.x == x && checkPoint.y == z)
            {
                return true;
            }
        }
        return false;
    }

}
