using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LipstickNumDisplay : MonoBehaviour
{
  public TMP_Text text;
  public LipstickFactory lipstickFactory;
  void Start()
  {
    lipstickFactory = LipstickFactory.Instance;
    Debug.Log(lipstickFactory);
    Debug.Log(lipstickFactory.ProductAmount);
    text.text = lipstickFactory.ProductAmount.ToString();
  }
  // Update is called once per frame
  void Update()
  {
    text.text = lipstickFactory.ProductAmount.ToString();
  }
}
