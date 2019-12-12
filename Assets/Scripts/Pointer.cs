using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pointer : MonoBehaviour {
    public LayerMask allowed;

    public Grabbable target;
    public LineRenderer active;
    public LineRenderer inactive;
    public float maxDistance = 0.5f;

    void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,
                            maxDistance, allowed)) {
            active.gameObject.SetActive(true);
            inactive.gameObject.SetActive(false);
            active.positionCount = 2;
            active.SetPositions(new Vector3[] {
                    Vector3.zero, new Vector3(0,0, hit.distance)
                });

            if (target) {
                target.SetHighlight(false);
            }

            target = hit.collider.GetComponentInParent<Grabbable>();
            target.SetHighlight(true);
        } else {
            inactive.positionCount = 2;
            inactive.SetPositions(new Vector3[] {
                    Vector3.zero, new Vector3(0,0, maxDistance)
                });
            active.gameObject.SetActive(false);
            inactive.gameObject.SetActive(true);
        }
    }
}
