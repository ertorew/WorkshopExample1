using UnityEngine;
using UnityEngine.InputSystem;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance { get; private set; }
    public Transform[] BallSpawnLocations;
	public GameObject BallPrefab;

	private InputAction spawnBallAction;

	public static void SpawnBall()
	{
		Vector3 targetLocation = GameSystem.instance.BallSpawnLocations[Random.Range(0, GameSystem.instance.BallSpawnLocations.Length)].position;

		Instantiate<GameObject>(instance.BallPrefab, targetLocation, Quaternion.identity);
	}

	private void Awake()
	{
        instance = this;
		spawnBallAction = InputSystem.actions.FindAction("SpawnBall");
	}

	private void Update()
	{
		if (spawnBallAction.WasPressedThisFrame())
		{
			SpawnBall();
		}
	}
}
