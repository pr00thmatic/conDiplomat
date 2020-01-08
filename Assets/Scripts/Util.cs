using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {
  public static void RecursiveSetLayer (int layer, Transform parent) {
    if (!parent) return;
    parent.gameObject.layer = layer;

    foreach (Transform child in parent) {
      RecursiveSetLayer(layer, child);
    }
  }

  public static void SetColliders (Transform owner, bool value) {
    Collider[] colliders = owner.GetComponentsInChildren<Collider>();

    foreach (Collider c in colliders) {
      c.enabled = value;
    }
  }

  public static void ResetBody (Rigidbody body) {
    body.velocity = Vector3.zero;
    body.angularVelocity = Vector3.zero;
  }

  public static void ResetTransform (Transform t) {
    t.localPosition = Vector3.zero;
    t.localRotation = Quaternion.identity;
    t.localScale = Vector3.one;
  }

  public static Vector3 GetScale (Transform t) {
    Transform oldParent = t.parent;
    t.parent = null;
    Vector3 scale = t.localScale;
    t.parent = oldParent;

    return scale;
  }

  public static void SetScale (Transform t, Vector3 scale) {
    Transform oldParent = t.parent;
    t.parent = null;
    t.localScale = scale;
    t.parent = oldParent;
  }

  public static IScriptPiece[] Execute (Transform scriptOwner) {
    if (!scriptOwner) return Execute((GameObject) null);
    return Execute(scriptOwner.gameObject);
  }

  public static IScriptPiece[] Execute (GameObject scriptOwner) {
    if (!scriptOwner) return new IScriptPiece[0];

    IScriptPiece[] pieces = scriptOwner.GetComponents<IScriptPiece>();

    foreach (IScriptPiece piece in pieces) {
      piece.Execute();
    }

    return pieces;
  }
}
