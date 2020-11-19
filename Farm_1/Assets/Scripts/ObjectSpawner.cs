using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject forestPrefab;
    public GameObject denseForestPrefab;
    public GameObject palmPrefab;
    public Transform forestParent;
    public Transform palmParent;

    public List<Vector2Int> denseTreePointList;
    public List<Vector2Int> palmOilTreePointList;
    public List<Vector2Int> emptyPointList;

    private GridSystem gridSystem;

    void Awake()
    {
        denseTreePointList = new List<Vector2Int>()
        {
        };
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
        InitializeSquarePoint(53, 59, 6, denseTreePointList);
        InitializeSquarePoint(41, 61, 6, denseTreePointList);
        InitializeSquarePoint(44, 27, 6, denseTreePointList);
        InitializeSquarePoint(54, 26, 6, denseTreePointList);
        for (int x = 25; x <= 35; x++)
        {
            for (int y = 34; y <= 64; y++)
            {
                denseTreePointList.Add(new Vector2Int(x, y));
            }
        }
        for (int x = 62; x <= 72; x++)
        {
            for (int y = 34; y <= 64; y++)
            {
                denseTreePointList.Add(new Vector2Int(x, y));
            }
        }
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
        SpawnDenseForest();
        SpawnForest();
        SpawnPalmOil();
    }

    private void SpawnForest()
    {
        for (int x = 0; x <= gridSystem.objectOnGrid.GetUpperBound(0); x++)
        {
            for (int z = 0; z <= gridSystem.objectOnGrid.GetUpperBound(1); z++)
            {
                if (!CheckPalmSpawnPoint(x, z) && !CheckEmptyPoint(x, z) && !CheckDenseForestSpawnPoint(x, z))
                {
                    GameObject tempObj;
                    Vector3 randomPosition;
                    tempObj = Instantiate(forestPrefab,
                                            gridSystem.GetPositionByGridPoint(x, z),
                                            Quaternion.identity,
                                            forestParent);
                    tempObj.name = "Forest Tree (" + x.ToString() + ", " + z.ToString() + ")";
                    randomPosition = new Vector3(Random.Range(0.0f, 0.4f), 0f, Random.Range(0.0f, 0.4f));
                    tempObj.transform.GetChild(0).transform.position = tempObj.transform.position + randomPosition;
                    tempObj.transform.GetChild(1).transform.position = tempObj.transform.position + randomPosition;
                    gridSystem.objectOnGrid[x, z] = tempObj;
                }
            }
        }
    }

    void SpawnDenseForest()
    {
        foreach (Vector2Int spawnPoint in denseTreePointList)
        {
            GameObject tempObj = Instantiate(denseForestPrefab,
                                                gridSystem.GetPositionByGridPoint(spawnPoint.x, spawnPoint.y),
                                                Quaternion.identity,
                                                forestParent);
            tempObj.name = "Dense Forest Tree (" + spawnPoint.x.ToString() + ", " + spawnPoint.y.ToString() + ")";

            Vector3 randomPosition = new Vector3(Random.Range(0.0f, 0.4f), 0f, Random.Range(0.0f, 0.4f));
            tempObj.transform.GetChild(0).transform.position = tempObj.transform.position + randomPosition;
            tempObj.transform.GetChild(1).transform.position = tempObj.transform.position + randomPosition;
            gridSystem.objectOnGrid[spawnPoint.x, spawnPoint.y] = tempObj;
        }
    }

    void SpawnPalmOil()
    {
        foreach (Vector2Int spawnPoint in palmOilTreePointList)
        {
            GameObject tempObj = Instantiate(palmPrefab,
                                                gridSystem.GetPositionByGridPoint(spawnPoint.x, spawnPoint.y),
                                                Quaternion.identity,
                                                palmParent);
            tempObj.name = "Palm Tree (" + spawnPoint.x.ToString() + ", " + spawnPoint.y.ToString() + ")";
            gridSystem.objectOnGrid[spawnPoint.x, spawnPoint.y] = tempObj;
            tempObj.GetComponent<Growing>().hasGrown = true;
        }
    }

    private bool CheckDenseForestSpawnPoint(int x, int z)
    {
        foreach (Vector2Int checkPoint in denseTreePointList)
        {
            if (checkPoint.x == x && checkPoint.y == z)
            {
                return true;
            }
        }
        return false;
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

    private void InitializeSquarePoint(int startX, int startY, int size, List<Vector2Int> vectList)
    {
        for (int x = startX; x < startX + size; x++)
        {
            for (int y = startY; y < startY + size; y++)
            {
                vectList.Add(new Vector2Int(x, y));
            }
        }

    }

}
