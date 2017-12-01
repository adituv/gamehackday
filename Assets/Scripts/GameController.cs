using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public PlayerController Player;
	public Image FadeOverlay;
	public Text GameOverText;
	public float GameSpeed = 1.0f;
	public float GameAcceleration = 0.05f;

	public Vector2 MaxObstaclePosition;
	public Vector2 MinObstaclePosition;

	public Transform BlockContainer;
	public GameObject BlockPrefab;
	public float SpawnRate = 0.3f;

	public AudioSource BackgroundMusic;
	public AudioSource GameOverSound;

	bool overlayEnabled = true;
	float startSpeed;
	float lastSpawn;

	public bool IsRunning { get; private set; }

	public void StartGame() {
		Player.Reset ();

		List<GameObject> existingBlocks = new List<GameObject> ();
		foreach (Transform block in BlockContainer) {
			existingBlocks.Add (block.gameObject);
		}
		existingBlocks.ForEach (b => Destroy (b));

		BackgroundMusic.Play ();
		IsRunning = true;
		SetOverlayEnabled (false);
		GameSpeed = startSpeed;
	}

	public void GameOver() {
		IsRunning = false;
		Player.StopImmediately ();
		BackgroundMusic.Stop ();
		GameOverSound.Play ();
		SetOverlayEnabled (true);
	}

	public void SetOverlayEnabled(bool state) {
		Color color;

		overlayEnabled = state;


		foreach (var c in FadeOverlay.GetComponentsInChildren<Graphic>()) {
			color = c.color;
			color.a = overlayEnabled ? 0.6f : 0.0f;
			c.color = color;
		}

		color = FadeOverlay.color;
		color.a = overlayEnabled ? 0.6f : 0.0f;
		FadeOverlay.color = color;
	}

	void SpawnObstacle() {
		float rand = Random.value;
		Vector3 pos = MinObstaclePosition + rand * (MaxObstaclePosition - MinObstaclePosition);
		var block = Instantiate (BlockPrefab, BlockContainer);
		block.GetComponent<BlockMover> ().Game = this;
		block.transform.position = pos;
	}

	// Use this for initialization
	void Start () {
		Physics2D.queriesHitTriggers = true;
		GameOverText.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		startSpeed = GameSpeed;
		GameSpeed = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (IsRunning) {
			GameSpeed += GameAcceleration * Time.deltaTime;


			if (Time.time - lastSpawn > 1 / (SpawnRate * GameSpeed)) {
				SpawnObstacle ();
				lastSpawn = Time.time;
			}
		}
	}
}
