using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveTrigger : MonoBehaviour {

    ShipMovement ship;

    private void Start() {
        ship = transform.parent.GetComponent<ShipMovement>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            ship.attachedPlayer = other.GetComponent<Move>();
            ship.playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            ship.playerInRange = false;
            ship.attachedPlayer = null;
        }
    }
}
