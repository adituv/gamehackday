using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {
	
	public float ScrollSpeed = 1.0f;

	private float singleObjectWidth;
	private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		singleObjectWidth = this.GetComponentInChildren<SpriteRenderer> ().bounds.size.x;
		originalPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		var pos = this.transform.position;
		pos.x -= ScrollSpeed * Time.deltaTime;
		if (pos.x + singleObjectWidth < originalPosition.x) {
			this.transform.position = originalPosition;
		} else {
			this.transform.position = pos;
		}
	}
}
