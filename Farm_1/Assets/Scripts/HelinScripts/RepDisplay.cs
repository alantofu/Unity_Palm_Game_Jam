using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class RepDisplay : MonoBehaviour
{
  public TMP_Text text;
  public Player player;
  void Start()
  {
    player = Player.Instance;
  }
  // Update is called once per frame
  void Update()
  {
    text.text = player.Rep.ToString();
  }
}
