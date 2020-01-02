using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiftReceiver : MonoBehaviour {
  public AudioSource voice;
  public EmotionManager emotions;
  public Transform scriptsParent;
  public float waitingTime = 2;

  void Start () {
    StartCoroutine(_EventuallyActivate());
  }

  IEnumerator _EventuallyActivate () {
    yield return new WaitForSeconds(waitingTime);
    Gift gift = LevelManager.Instance.gift.GetComponent<Gift>();
    scriptsParent.Find(gift.definition.name).GetComponent<DialogueScript>().Execute();
    voice.PlayOneShot(gift.definition.dialogue);
  }
}
