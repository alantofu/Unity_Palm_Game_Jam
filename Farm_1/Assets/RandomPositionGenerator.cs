using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionGenerator : MonoBehaviour
{
    private GridSystem gridSystem;
    void Start()
    {
        gridSystem = GridSystem.Instance;
        int RanX = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(0));
        int RanZ = Random.Range(0, gridSystem.objectOnGrid.GetUpperBound(1));
        GameObject.Find("AlCharacterControl").GetComponent<Transform>() = gridSystem.getPositionByGridPoint(RanX, RanZ);
    }

    
}
