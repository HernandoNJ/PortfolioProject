using UnityEngine;
using UnityEngine.AI;

public class AIChickenController : MonoBehaviour
{

	public GameObject goal;
	NavMeshAgent agent;
	Animator anim;


	// Use this for initialization
	void Start()
	{
		goal = GameObject.FindWithTag("Player");
		agent = this.GetComponent<NavMeshAgent>();
		anim = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		agent.SetDestination(goal.transform.position);
		if (agent.remainingDistance < 2)
		{
			anim.SetBool("isMoving", false);

		}
		else
		{
			anim.SetBool("isMoving", true);
		}

	}
}
