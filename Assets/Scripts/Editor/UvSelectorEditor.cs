using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(UvSelector))]
public class UvSelectorEditor : Editor {
  UvSelector Target { get => (UvSelector) target; }

  public override void OnInspectorGUI () {
    DrawDefaultInspector();
    if (GUILayout.Button("Set UV")) {
      Target.SetUv(Target.index);
    }
  }
}
