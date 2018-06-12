using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    public float shipSpeed;
    public float shipTurnSpeed;

    bool hasDriver = false;

    Rigidbody rb;

    [HideInInspector]
    public Move attachedPlayer;

    public bool playerInRange = false;

    public Transform shipCameraPoint;
    public Transform camPivot;
    public float camRotateSpeed;

    GameObject tempCamera;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (hasDriver) {
            //cam rotate
            float y = camRotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            camPivot.Rotate(0, y, 0);

            if (Input.GetAxis("Vertical") != 0) {
                //ship move
                float x = Input.GetAxis("Vertical");
                transform.Translate(0, 0, -x);
                //Vector3 movement = new Vector3(0, 0.0f, -x);
                //Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);
                //rb.velocity = movement * shipSpeed * Time.deltaTime;
                //rb.velocity += yVelFix;

                //ship turn
                if (Input.GetKey(KeyCode.A))
                    transform.Rotate(Vector3.up, -shipTurnSpeed * Time.deltaTime);

                if (Input.GetKey(KeyCode.D))
                    transform.Rotate(Vector3.up, shipTurnSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
            PlayerAttach();
    }

    public void PlayerAttach() {
        if (playerInRange) {
            attachedPlayer.canMove = !attachedPlayer.canMove;
            hasDriver = !hasDriver;
            if (hasDriver) {
                attachedPlayer.rb.isKinematic = true;
                tempCamera = attachedPlayer.cam;
                tempCamera.transform.SetParent(shipCameraPoint);
                tempCamera.transform.position = shipCameraPoint.position;
                tempCamera.transform.rotation = shipCameraPoint.rotation;
            } else {
                attachedPlayer.rb.isKinematic = false;
                tempCamera.transform.SetParent(attachedPlayer.cameraPoint);
                tempCamera.transform.position = attachedPlayer.cameraPoint.position;
                tempCamera.transform.rotation = attachedPlayer.cameraPoint.rotation;
            }
        }
    }
}
