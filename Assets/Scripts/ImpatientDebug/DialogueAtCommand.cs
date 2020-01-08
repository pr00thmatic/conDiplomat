using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ImpacientDebug {
public class DialogueAtCommand : MonoBehaviour {
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
