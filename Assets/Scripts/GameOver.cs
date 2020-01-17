using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameOver : MonoBehaviour, IScriptPiece {
  public NextTriggerer Triggerer { get => _triggerer; } [SerializeField] NextTriggerer _triggerer;
  public MonoBehaviour waitToFinish;

  bool _stop = false;

  void Reset () {
    VoiceScript voice = GetComponent<VoiceScript>();
    if (voice) {
      Triggerer.delay = voice.Length;
    }
  }

  public void Execute () {
    StartCoroutine(_GameOver());
  }

  IEnumerator _GameOver () {
    IScriptPiece piece = waitToFinish as IScriptPiece;
    if (piece != null) {
      piece.Triggerer.onFinish += HandleFinish;
      while (!_stop) {
        yield return null;
      }
    } else {
      yield return new WaitForSeconds(Triggerer.delay);
    }

    SceneManager.LoadSceneAsync("menu");
  }

  public void HandleFinish () {
    _stop = true;
  }
}
