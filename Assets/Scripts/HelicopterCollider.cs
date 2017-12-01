using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterCollider : MonoBehaviour {
	public GameController Game;

	void OnCollisionEnter2D(Collision2D other) {
		Game.GameOver ();
	}
}
