using UnityEngine;

public class Parallaxer : MonoBehaviour
{
	// keep track of all its objects
	// it determines whether or not an object will be used
	class PoolObject
	{
		public Transform transform;
		public bool inUse;
		public PoolObject(Transform t) { transform = t; }
		public void Use() { inUse = true; }
		public void Dispose() { inUse = false; }
	}

	[System.Serializable]
	public struct YSpawnRange
	{
		public float min, max; // min and max height of the spawn
	}

	public GameObject parallaxPrefab; // keep track of the prefab's type spawned, in this case, GameObject type
	public int poolSize; // pool's size: how many objects will be spawn
	public Vector2 targetAspectRatio;
	public YSpawnRange ySpawnRange;
	public float shiftSpeed;
	public float spawnRate; // how often objects will be spawned
	public Vector2 defaultSpawnPos;
	public Vector2 immediateSpawnPos;
	public bool spawnImmediate; // set up parallax at start (pre-warm)

	// ensure parallax objects are spawned within the screen space
	// for all target platforms aspect ratio
	// Aspect ratio: divide width by height

	// portrait or landscape: 10/16 or 16/10
	// camera class just gives x/width or x/height
	// store target aspect ratio within a float
	private float targetAspect;
	private float spawnTimer;
	private int offScreenPos = 20;

	private TappyBirdGameManager gm;
	private PoolObject[] poolObjectsArray; // array of pool objects needed

	private void Awake() => Configure(); // on gameoverconfirmed, objects will be respawned



	private void OnEnable() => TappyBirdGameManager.OnGameoverConfirmedEvent += OnGameOverConfirmedMethod;

	private void OnDisable() => TappyBirdGameManager.OnGameoverConfirmedEvent -= OnGameOverConfirmedMethod;

	private void Start() => gm = TappyBirdGameManager.instance;

	private void Update()
	{
		if (gm.IsGameover)
			return;

		Shift();
		spawnTimer += Time.deltaTime;

		if (spawnTimer > spawnRate)
		{ Spawn(); spawnTimer = 0; }
	}

	private void Configure()
	{
		// we need to instantiate a number of prefabs based on pool size

		// set target aspect
		// targetAspectRatio will be modified from inspector
		// if target aspect changes during gameplay, it needs to be reset everytime we reconfigure
		targetAspect = targetAspectRatio.x / targetAspectRatio.y;

		// instantiate go, different from spawning
		poolObjectsArray = new PoolObject[poolSize];
		for (int i = 0; i < poolObjectsArray.Length; i++)
		{
			GameObject go = Instantiate(parallaxPrefab) as GameObject; // cast prefab as GameObject
			Transform tr = go.transform;
			tr.SetParent(transform); // TODO Study: set the parent to this current object
			tr.position = Vector3.one * offScreenPos; // set the position initializing it off-screen

			// initialize the value within our pool objects array
			// new PoolObject with a parameter tr of type Transform
			poolObjectsArray[i] = new PoolObject(tr);
		}

		// for our parallaxing, spawning is placing objects 
		// in correct position to be shown on screen
		if (spawnImmediate)
		{ SpawnImmediate(); }
	}

	private void OnGameOverConfirmedMethod()
	{
		// bird in start position
		// dispose and restart all GOs 
		for (int i = 0; i < poolObjectsArray.Length; i++)
		{
			poolObjectsArray[i].Dispose();
			poolObjectsArray[i].transform.position = Vector3.one * offScreenPos;
		}

		if (spawnImmediate)
		{ SpawnImmediate(); }
	}

	private void Spawn()
	{
		Transform t = GetPoolObjects(); // this transform comes from one of the pool objects
		if (t == null)
			return; // if true, it indicates poolsize is too small

		// set the position of t's GO
		Vector3 pos = Vector3.zero; // define parallax position
		pos.x = (defaultSpawnPos.x * Camera.main.aspect) / targetAspect; // it will dependent upon target aspect
		pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
		t.position = pos;
	}


	private void SpawnImmediate()
	{
		Transform t = GetPoolObjects(); // this transform comes from one of the pool objects
		if (t == null)
			return; // if true, it indicates poolsize is too small

		// set the position of t's GO
		Vector3 pos = Vector3.zero; // define parallax position
		pos.x = (immediateSpawnPos.x * Camera.main.aspect) / targetAspect; // it will dependent upon target aspect
		pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
		t.position = pos;

		Spawn();
	}

	private void Shift()
	{
		for (int i = 0; i < poolObjectsArray.Length; i++)
		{
			poolObjectsArray[i].transform.position += -Vector3.right * shiftSpeed * Time.deltaTime; // TODO study

			// as it shifts, is the position less than the dead zone?
			// is our pool object position less than default spawn position?
			CheckDisposeObject(poolObjectsArray[i]);
		}
	}

	// check if an object must be disposed
	private void CheckDisposeObject(PoolObject poolObject)
	{
		// if parallax object is off screen, set InUse to false
		// to make it available to be spawned again
		// start using target aspect
		// defaultSpawnPos.x should be off screen
		// current aspect ratio: Camera.main.aspect
		if (poolObject.transform.position.x < (-defaultSpawnPos.x * Camera.main.aspect) / targetAspect)
		{
			poolObject.Dispose(); // inUse = false

			// set the position someplace off-screen
			// any number far far away from screen
			poolObject.transform.position = Vector3.one * offScreenPos;
		}
	}

	// get available pool objects
	Transform GetPoolObjects()
	{
		// how to get an available object?
		// 1. get the 1st. available pool object in the array
		for (int i = 0; i < poolObjectsArray.Length; i++)
		{
			if (!poolObjectsArray[i].inUse)
			{
				poolObjectsArray[i].Use(); // it will set inUse to true

				return poolObjectsArray[i].transform;
			}
		}

		return null;
	}

}
