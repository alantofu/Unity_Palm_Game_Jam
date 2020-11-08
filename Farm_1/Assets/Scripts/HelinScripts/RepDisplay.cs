using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RepDisplay : MonoBehaviour
{
  public Text text;
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
