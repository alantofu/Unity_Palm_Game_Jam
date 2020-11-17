using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerSpawner : MonoBehaviour
{
    public GameObject Worker;
    public Transform WorkerParent;
    private GridSystem gridSystem;
    public int Amountofworkers;

    private void SpawnWorker()
    {
        int RanX = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(0));
        int RanZ = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(1));
        Debug.Log(RanX);
        Debug.Log(RanZ);
        GameObject tempObj = Instantiate(Worker,
                                                 gridSystem.getPositionByGridPoint(RanX, RanZ),
                                                 Quaternion.identity,
                                                 WorkerParent);
        tempObj.name = "Worker (" + RanX.ToString() + ", " + RanZ.ToString() + ")";
        gridSystem.objectOnGrid[RanX, RanZ] = tempObj;
    }

    void Start()
    {
        gridSystem = GridSystem.Instance;
        for (int i =0; i< Amountofworkers;i++)
        {
            SpawnWorker();
        }
    }

    
}
