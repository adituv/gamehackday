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
	public List<ObjectSpawner> Spawners;
	public float SpawnRate = 0.3f;

	public AudioSource BackgroundMusic;
	public AudioSource GameOverSound;

	private float Score;
	public Text ScoreText;

	private bool allowStartGame = true;
	private float gameOverTime = 0.0f;
	public float gameOverScreenMinimum = 2.0f;

	bool overlayEnabled = true;
	float startSpeed;
	float lastSpawn;

	public bool IsRunning { get; private set; }

	public void StartGame() {
		if (!allowStartGame)
			return;
		
		Player.Reset ();
		Score = 0;

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
		allowStartGame = false;
		gameOverTime = Time.time;
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
		int rand = (int) Mathf.Floor (Random.value * Spawners.Count);
		var spawnedObj = Spawners [rand].Spawn ();

		spawnedObj.GetComponent<BlockMover> ().Game = this;
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

			Score += GameSpeed * Time.deltaTime;
			ScoreText.text = "Score: " + (int)(Mathf.Floor (Score));

			if (Time.time - lastSpawn > 1 / (SpawnRate * GameSpeed)) {
				SpawnObstacle ();
				lastSpawn = Time.time;
			}
		} else {
			if (Time.time - gameOverTime > gameOverScreenMinimum) {
				allowStartGame = true;
			}
		}
	}
}
