using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UvSelector : MonoBehaviour {
  public int index;
  public SkinnedMeshRenderer[] rend;

  void Reset () {
    rend = GetComponentsInChildren<SkinnedMeshRenderer>();
  }

  public void SetUv (int uv) {
    Vector2[] uvs = null;

    foreach (SkinnedMeshRenderer r in rend) {
      Mesh mesh = r.sharedMesh;

      switch (uv) {
        case 1: uvs = mesh.uv; break;
        case 2: uvs = mesh.uv2; break;
        case 3: uvs = mesh.uv3; break;
        case 4: uvs = mesh.uv4; break;
        case 5: uvs = mesh.uv5; break;
        case 6: uvs = mesh.uv6; break;
        case 7: uvs = mesh.uv7; break;
        case 8: uvs = mesh.uv8; break;
      }

      mesh.SetUVs(0, new List<Vector2>(uvs));
    }
  }
}
