using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour {

	public GameController Game;

	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Game.IsRunning) {
			var pos = body.position;
			pos.x -= Game.GameSpeed * Time.deltaTime;

			body.MovePosition (pos);
		}
	}
}
