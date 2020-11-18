using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionGenerator : MonoBehaviour
{
    private GridSystem gridSystem;
    private Vector3 randomposition;
    private static Transform Target;

    void Start()
    {
        gridSystem = GridSystem.Instance;
        StartCoroutine(GenerateRandomPosition());
    }

    IEnumerator GenerateRandomPosition()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, randomposition) < 0.5f)
            {
                int RanX = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(0));
                int RanZ = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(1));
                randomposition = gridSystem.getPositionByGridPoint(RanX, RanZ);
                Target.position = randomposition;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    
}
