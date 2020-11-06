using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour
{
    public int money;
    public int reputation;
    public int lipstick;
    public int margerine;
    public int lipstickPrice=150;
    public int margerinePrice=8;
    // Start is called before the first frame update
    void Start()
    {
        money = 100000;
        reputation = 10000;
        lipstick = 0;
        margerine = 0;
    }

    public void PurchaseLipstick(){
        money-=150;
        lipstick+=100;
    }
}
