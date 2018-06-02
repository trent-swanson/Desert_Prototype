using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float speed;
	public float turnSpeed;

	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		Cursor.visible = false;
	}

	void FixedUpdate() {
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		float y = turnSpeed * Input.GetAxis ("Mouse X") * Time.deltaTime;
        transform.Rotate(0, y, 0);
        transform.Translate(x, 0, z);
        //Camera.main.transform.Rotate (y, 0, 0);
	}
}
