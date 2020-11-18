using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject WorkerPrefab;
    public Transform WorkerParent;
    private GridSystem gridSystem;
    public int WorkerAmount;

    void Start()
    {
        gridSystem = GridSystem.Instance;
        SpawnWorkers();
    }

    private void SpawnWorkers()
    {
        for (int i = 0; i < WorkerAmount; i++)
        {
            int RanX = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(0));
            int RanZ = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(1));
            GameObject tempObj = Instantiate(WorkerPrefab,
                                                gridSystem.getPositionByGridPoint(RanX, RanZ),
                                                Quaternion.identity,
                                                WorkerParent);
            tempObj.name = "Worker (" + RanX.ToString() + ", " + RanZ.ToString() + ")";
            gridSystem.objectOnGrid[RanX, RanZ] = tempObj;
        }
    }
}
