using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour {
    ShipMovement ship;

    private void Start() {
        ship = transform.parent.GetComponent<ShipMovement>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            ship.attachedPlayer = other.GetComponent<Move>();
            other.transform.SetParent(ship.transform);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            ship.attachedPlayer = null;
            other.transform.SetParent(null);
        }
    }
}
