using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
  public List<TabButton> tabButtons;
  public Sprite tabIdle;
  public Sprite tabHover;
  public Sprite tabActive;
  public TabButton selectedTab;
  public List<GameObject> objectsToSwap;

  public void Subscribe(TabButton button)
  {
    if (tabButtons == null)
    {
      tabButtons = new List<TabButton>();
    //   tabButtons.Add(button);
    //   onTabSelected(button);
      //   selectedTab = button;
      //   button.background.sprite = tabActive;
    }
    // else
    // {
    //   button.background.sprite = tabIdle;
    // }
      tabButtons.Add(button);
  }

  public void onTabEnter(TabButton button)
  {
    ResetTabs();
    if (selectedTab == null || button != selectedTab)
    {
      button.background.sprite = tabHover;
    }
  }
  public void onTabExit(TabButton button)
  {
    ResetTabs();
  }
  public void onTabSelected(TabButton button)
  {
    selectedTab = button;
    ResetTabs();
    button.background.sprite = tabActive;
    int index = button.transform.GetSiblingIndex();
    for (int i = 0; i < objectsToSwap.Count; i++)
    {
      if (i == index)
      {
        objectsToSwap[i].SetActive(true);
      }
      else
      {
        objectsToSwap[i].SetActive(false);
      }
    }
  }
  public void ResetTabs()
  {
    foreach (TabButton button in tabButtons)
    {
      if (selectedTab != null && button == selectedTab) { continue; }
      button.background.sprite = tabIdle;
    }
  }
}
