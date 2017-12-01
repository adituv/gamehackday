using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	public Transform Container;
	public GameObject ThingToSpawn;
	public Vector2 MinimumPosition;
	public Vector2 MaximumPosition;

	public GameObject Spawn() {
		float rand = Random.value;
		Vector2 pos = MinimumPosition + rand * (MaximumPosition - MinimumPosition);
		var spawnedThing = Instantiate (ThingToSpawn, Container);
		spawnedThing.transform.position = pos;

		return spawnedThing;
	}
}

