using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameController Game;
	public Rigidbody2D Helicopter;
	public Vector2 InitialPosition;
	public float HelicopterAcceleration = 10.0f;

	private bool isAccelerating = false;

	public void Reset() {
		Helicopter.MovePosition (InitialPosition);
		isAccelerating = false;
	}

	public void StartAccelerating() {
		isAccelerating = true;
	}

	public void StopAccelerating() {
		isAccelerating = false;
	}

	public void StopImmediately() {
		Helicopter.velocity = new Vector2 (0, 0);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.IsRunning) {
			if (isAccelerating) {
				Helicopter.AddForce (new Vector2 (0, HelicopterAcceleration));
			} else {
				Helicopter.AddForce (new Vector2 (0, -HelicopterAcceleration));
			}
		}
	}
}
