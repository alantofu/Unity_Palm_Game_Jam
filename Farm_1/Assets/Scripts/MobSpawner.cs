using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] prefabArray;
    public Transform parentTransform;
    private GridSystem gridSystem;
    public int workerAmount;

    void Start()
    {
        gridSystem = GridSystem.Instance;
        SpawnWorkers();
    }

    private void SpawnWorkers()
    {
        if (prefabArray.Length > 0)
        {
            for (int j = 0; j < prefabArray.Length; j++)
            {
                for (int i = 0; i < workerAmount; i++)
                {
                    int RanX = Random.Range(30, 70); // hardcoded range
                    int RanZ = Random.Range(30, 70);
                    GameObject tempObj = Instantiate(prefabArray[j],
                                                        gridSystem.GetPositionByGridPoint(RanX, RanZ),
                                                        Quaternion.identity,
                                                        parentTransform);
                    tempObj.name = prefabArray[j].name + " (" + RanX.ToString() + ", " + RanZ.ToString() + ")";
                    tempObj.SetActive(true);
                }
            }
        }
    }
}
