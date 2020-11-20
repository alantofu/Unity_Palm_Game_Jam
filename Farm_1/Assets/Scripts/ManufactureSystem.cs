using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManufactureSystem : MonoBehaviour
{
    private static ManufactureSystem _instance;
    public static ManufactureSystem Instance { get { return _instance; } } // a singleton

    public GameObject selectedFactoryObject;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
