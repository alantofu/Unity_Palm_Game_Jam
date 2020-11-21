using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchDebug : MonoBehaviour
{
    public GameObject touchDebugCircle;
    private GameObject mouseDebugCircle;
    public Dictionary<int, GameObject> touchDict = new Dictionary<int, GameObject>();

    void Start()
    {
        // if (touchDebugCircle == null)
        // {
        //     touchDebugCircle = Resources.Load("Prefabs/Debug Circle", typeof(GameObject)) as GameObject;
        // }
    }


    void Update()
    {
        // #if UNITY_ANDROID || UNITY_IOS
        //         for (int i = 0; i < Input.touchCount; ++i)
        //         {
        //             Touch t = Input.GetTouch(i);
        //             if (t.phase == TouchPhase.Began)
        //             {
        //                 touchDict.Add(t.fingerId, createDebugCircle(t));
        //             }
        //             else if (t.phase == TouchPhase.Ended)
        //             {
        //                 Destroy(touchDict[t.fingerId].gameObject);
        //                 touchDict.Remove(t.fingerId);
        //             }
        //             else if (t.phase == TouchPhase.Moved)
        //             {
        //                 touchDict[t.fingerId].GetComponent<Image>().transform.position = t.position;
        //             }
        //         }
        // #elif UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            if (mouseDebugCircle == null)
            {
                GameObject newObj = Instantiate(touchDebugCircle, this.gameObject.transform) as GameObject;
                mouseDebugCircle = newObj;
                mouseDebugCircle.name = "Mouse";
            }
            else
            {
                // GameObject newObj = Instantiate(touchDebugCircle, this.gameObject.transform) as GameObject;
                // // Destroy(mouseDebugCircle);
                // mouseDebugCircle = newObj;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (mouseDebugCircle != null)
            {
                mouseDebugCircle.GetComponent<Image>().transform.position = Input.mousePosition;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(mouseDebugCircle);
        }
        // #endif
    }

    private void OnDisable()
    {
        foreach (KeyValuePair<int, GameObject> kvp in touchDict)
        {
            Destroy(kvp.Value);
        }
        touchDict.Clear();
    }

    GameObject createDebugCircle(Touch t)
    {
        GameObject newObj = Instantiate(touchDebugCircle, this.gameObject.transform) as GameObject;
        newObj.name = "Touch" + t.fingerId;
        newObj.GetComponent<Image>().transform.position = t.position;
        return newObj;
    }
}
