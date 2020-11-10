using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanel : MonoBehaviour
{
    public GameObject[] Panel;

    public void switchPanel(){
        if(Panel[0] != null && Panel[1]!=null){
            bool isActive = Panel[0].activeSelf;
            Panel[0].SetActive(!isActive);
            isActive = Panel[1].activeSelf;
            Panel[1].SetActive(!isActive);
        }
    }
}
