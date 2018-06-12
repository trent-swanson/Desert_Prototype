using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float speed;
	public float turnSpeed;

    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;

    [HideInInspector]
    public bool canMove = true;

	public Rigidbody rb;

    public Transform cameraPoint;
    public GameObject cam;

    void Start() {
		rb = GetComponent<Rigidbody>();
		Cursor.visible = false;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

	void FixedUpdate() {
        transform.Rotate(Vector3.zero);
        transform.Translate(Vector3.zero);
		if (canMove) {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            float y = turnSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            transform.Rotate(0, y, 0);
            transform.Translate(x, 0, z);

            //jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }

    void OnCollisionStay() {
        isGrounded = true;
    }
}
