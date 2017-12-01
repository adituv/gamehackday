using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour {
	public GameController Game;
	public PlayerController Player;

	void OnMouseDown () {
		if (Game.IsRunning) {
			Player.StartAccelerating ();
		}
	}

	void OnMouseUp () {
		if (!Game.IsRunning) {
			Game.StartGame ();
		} else {
			Player.StopAccelerating ();
		}
	}
}
