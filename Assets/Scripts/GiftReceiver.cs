using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiftReceiver : MonoBehaviour, IHaveAChoise, IScriptPiece {
  [SerializeField] NextTriggerer _triggerer; public NextTriggerer Triggerer { get => _triggerer; }
  public Transform scriptsParent;
  public float waitingTime = 2;

  public GameObject Choosen { get => scriptUnleashed; }
  public GameObject scriptUnleashed;

  void Start () {
    Execute();
  }

  IEnumerator _EventuallyActivate () {
    yield return new WaitForSeconds(waitingTime);
    Gift gift = LevelManager.Instance.Gift;

    if (gift) {
      scriptUnleashed = scriptsParent.Find(gift.definition.name).gameObject;
    } else {
      scriptUnleashed = scriptsParent.Find("no gift").gameObject;
    }

    Util.Execute(scriptUnleashed);
    Triggerer.TriggerFinish(this);
  }

  public void Execute () {
    StartCoroutine(_EventuallyActivate());
  }
}
