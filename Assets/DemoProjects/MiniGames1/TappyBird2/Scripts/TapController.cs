using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
	public delegate void PlayerDelegate();
	public static event PlayerDelegate OnPlayerDied;
	public static event PlayerDelegate OnPlayerScored;

	public float tapForce = 10;
	public float tiltSmooth = 5;
	public Vector2 startPos;

	private TappyBirdGameManager gm;
	private Rigidbody2D rb;
	private Quaternion downRotation;
	private Quaternion forwardRotation;

	public AudioSource tapSound;
	public AudioSource scoreSound;
	public AudioSource dieSound;


	private void OnEnable()
	{
		TappyBirdGameManager.OnGameStartedEvent += OnGameStartedMethod;
		TappyBirdGameManager.OnGameoverConfirmedEvent += OnGameOverConfirmedMethod;
	}

	private void OnDisable()
	{
		TappyBirdGameManager.OnGameStartedEvent -= OnGameStartedMethod;
		TappyBirdGameManager.OnGameoverConfirmedEvent -= OnGameOverConfirmedMethod;
	}
	private void Start()
	{
		gm = TappyBirdGameManager.instance;

		rb = GetComponent<Rigidbody2D>();
		rb.simulated = false;

		downRotation = Quaternion.Euler(0, 0, -90);
		forwardRotation = Quaternion.Euler(0, 0, 35);

	}

	private void Update()
	{
		if (gm.IsGameover)
			return;

		if (Input.GetMouseButtonDown(0))
		{
			transform.rotation = forwardRotation;
			rb.velocity = Vector2.zero;
			rb.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
			tapSound.Play();
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.Escape))
		{ Application.Quit(); }

	}

	private void OnGameStartedMethod() { rb.velocity = Vector2.zero; rb.simulated = true; }

	private void OnGameOverConfirmedMethod()
	{
		// reset everything's position before start playing
		transform.localPosition = startPos; // local position because it is inside an environment
		transform.rotation = Quaternion.identity;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("ScoreZone"))
		{
			// register a score event
			// event sent to GameManager
			// play a sound
			OnPlayerScored();
			scoreSound.Play();
		}

		if (col.gameObject.CompareTag("DeadZone"))
		{
			// freeze the rb component in GO (bird) when it hits
			// register a dead event 
			rb.simulated = false;
			// event sent to GameManager                        
			// play a sound
			OnPlayerDied();
			dieSound.Play();

		}
	}
}


