using UnityEngine;
using UnityEngine.UI;

public class AIHenController : MonoBehaviour
{
	//public GameObject checkpoint;
	//NavMeshAgent agent;
	//Animator anim;


	//// Use this for initialization
	//void Start()
	//{
	//	checkpoint = GameObject.FindWithTag("Player");
	//	agent = this.GetComponent<NavMeshAgent>();
	//	anim = this.GetComponent<Animator>();
	//}

	//// Update is called once per frame
	//void Update()
	//{
	//	agent.SetDestination(checkpoint.transform.position);
	//	if (agent.remainingDistance < 2)
	//	{
	//		anim.SetBool("isMoving", false);

	//	}
	//	else
	//	{
	//		anim.SetBool("isMoving", true);
	//	}

	//}

	// TODO modify script with command pattern when animations are available
	[SerializeField] private float moveSp = 5f;
	[SerializeField] public Text scoreText;
	[SerializeField] public int score = 0;

	void Start() { scoreText.text = score.ToString(); }

	void Update() { InputMov(); }

	public void IncreaseScore()
	{
		score++;
		scoreText.text = score.ToString();

		if (score == 4)
		{
			scoreText.text = "YOU WIN!!!";
			Time.timeScale = 0f;
		}
	}

	public void InputMov()
	{
		float moveH = Input.GetAxis("Horizontal");
		float moveV = Input.GetAxis("Vertical");

		Vector3 mov = new Vector3(moveH, 0, moveV) * moveSp * Time.deltaTime;
		transform.Translate(mov);
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("ValidCheckpoint"))
		{
			IncreaseScore();
			Destroy(col.gameObject);
		}
	}
}


