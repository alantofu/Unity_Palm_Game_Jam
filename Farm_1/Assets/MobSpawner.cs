using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] Prefab;
    public Transform Parent;
    private GridSystem gridSystem;
    public int WorkerAmount;

    void Start()
    {
        gridSystem = GridSystem.Instance;
        SpawnWorkers();
    }

    private void SpawnWorkers()
    {
        if (Prefab.Length>0)
        {
            for (int j = 0; j< Prefab.Length; j++)
            {
                for (int i = 0; i < WorkerAmount; i++)
                {
                    int RanX = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(0));
                    int RanZ = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(1));
                    GameObject tempObj = Instantiate(Prefab[j],
                                                        gridSystem.getPositionByGridPoint(RanX, RanZ),
                                                        Quaternion.identity,
                                                        Parent);
                    tempObj.name = "Mob (" + RanX.ToString() + ", " + RanZ.ToString() + ")";
                    tempObj.SetActive(true);
                    gridSystem.objectOnGrid[RanX, RanZ] = tempObj;
                }
            }
        }     
    }
}
