using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GiftReceiver : MonoBehaviour, IHaveAChoise, IScriptPiece {
  [SerializeField] NextTriggerer _triggerer; public NextTriggerer Triggerer { get => _triggerer; }
  [SerializeField] DecisionMemory _memory;
  public DecisionMemory Memory { get => _memory? _memory: GetComponentInParent<DecisionMemory>(); }
  public Transform scriptsParent;
  public float waitingTime = 2;

  public GameObject Choosen { get => Memory.decision; set => Memory.decision = value; }

  void Start () {
    Execute();
  }

  IEnumerator _EventuallyActivate () {
    yield return new WaitForSeconds(waitingTime);
    Gift gift = LevelManager.Instance.Gift;

    if (gift) {
      Choosen = scriptsParent.Find(gift.definition.name).gameObject;
    } else {
      Choosen = scriptsParent.Find("no gift").gameObject;
    }

    Util.Execute(Choosen);
    Triggerer.TriggerFinish(this);
  }

  public void Execute () {
    StartCoroutine(_EventuallyActivate());
  }
}
