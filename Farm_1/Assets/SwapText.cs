using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapText : MonoBehaviour
{
    public TextMesh text;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        text = gameObject.GetComponent<TextMesh>();
        text.text = "Counter: " + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
