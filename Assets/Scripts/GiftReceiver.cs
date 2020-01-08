using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiftReceiver : MonoBehaviour {
  public EmotionManager emotions;
  public Transform scriptsParent;
  public float waitingTime = 2;

  void Start () {
    StartCoroutine(_EventuallyActivate());
  }

  IEnumerator _EventuallyActivate () {
    yield return new WaitForSeconds(waitingTime);
    Gift gift = LevelManager.Instance.gift.GetComponent<Gift>();
    Util.Execute(scriptsParent.Find(gift.definition.name).gameObject);
  }
}
