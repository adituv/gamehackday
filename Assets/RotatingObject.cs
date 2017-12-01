using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour {

	public float RotationSpeed = 100.0f;

	Rigidbody2D body;
	// Use this for initialization
	void Start () {
		body = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		body.MoveRotation (body.rotation + RotationSpeed * Time.deltaTime);
	}
}
