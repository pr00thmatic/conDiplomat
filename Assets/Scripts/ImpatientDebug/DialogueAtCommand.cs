using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ImpacientDebug {
public class DialogueAtCommand : MonoBehaviour {
  void Reset () {
    DialogueAtCommand[] commanded = GameObject.FindObjectsOfType<DialogueAtCommand>();

    foreach (DialogueAtCommand clone in commanded) {
      if (clone != this) {
        if (Application.isPlaying) {
          Destroy(clone);
        } else {
          DestroyImmediate(clone);
        }
      }
    }
  }

  void Update () {
    if (Input.GetKeyDown(KeyCode.Space)) {
      IScriptPiece[] pieces = GetComponents<IScriptPiece>();
      foreach (IScriptPiece piece in pieces) {
        piece.Execute();
      }
    }
  }
}
}
