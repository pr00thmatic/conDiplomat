using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiftDependant : MonoBehaviour {
  public string triggerGift;

  void Start () {
    if (triggerGift == LevelManager.Instance.GiftName) {
      int n = transform.childCount;
      for (int i=n-1; i>=0; i--) {
        transform.GetChild(i).parent = transform.parent;
      }
    }

    Destroy(this.gameObject);
  }
}
