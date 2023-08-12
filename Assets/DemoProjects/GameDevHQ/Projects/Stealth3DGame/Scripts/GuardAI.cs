using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	[RequireComponent(typeof(Animator))]
	public class GuardAI : MonoBehaviour
	{
		public List<Transform> waypoints;
		private NavMeshAgent guardAgent;
		private Animator animator;

		[SerializeField] private Transform newPoint;
		[SerializeField] private Vector3 coinPosition;
		[SerializeField] private float distance;
		[SerializeField] private int currentTarget;
		[SerializeField] private bool reverse;
		[SerializeField] private bool stopMove;
		[SerializeField] private bool targetReached;
		[SerializeField] private bool lookingForCoin;

		private static readonly int isWalking = Animator.StringToHash("isWalking");

		private void OnEnable() => Player.OnCoinLaunched += SetCoinPoint;
		private void OnDisable() => Player.OnCoinLaunched -= SetCoinPoint;

		private void Start()
		{
			guardAgent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
			animator.SetBool(isWalking, true);
			newPoint = waypoints[currentTarget];
		}

		private void Update()
		{
			MoveToPoint();
			if (targetReached)
				SetNewTarget();
		}

		private void MoveToPoint()
		{
			var newPosition = new Vector3(0, 0, 0);

			if (lookingForCoin)
				newPosition = coinPosition;
			else
			{
				newPoint = waypoints[index: currentTarget];
				if (newPoint != null)
					newPosition = newPoint.position;
			}

			var moveEnabled = waypoints.Count > 0 && !stopMove;
			if (moveEnabled)
				guardAgent.SetDestination(newPosition);

			distance = Vector3.Distance(transform.position, newPosition);

			if (distance > 1f)
				targetReached = false;
			else if (!targetReached)
			{
				if (lookingForCoin)
					lookingForCoin = false;
				targetReached = true;
			}
		}

		private void SetNewTarget()
		{
			// if start/end point
			if (currentTarget == waypoints.Count - 1)
			{
				reverse = true;
				StartCoroutine(routine: WaitToMove());
			}
			else if (currentTarget == 0)
			{
				reverse = false;
				StartCoroutine(routine: WaitToMove());
			}

			// if any point (including intermediate points)
			if (reverse)
				currentTarget--;
			else
				currentTarget++;
		}

		private void SetCoinPoint(Vector3 coinPositionArg)
		{
			lookingForCoin = true;
			coinPosition = coinPositionArg;
		}

		private IEnumerator WaitToMove()
		{
			stopMove = true;
			animator.SetBool(isWalking, false);

			var randNum = Random.Range(0.7f, 2f);
			yield return new WaitForSeconds(randNum);

			animator.SetBool(isWalking, true);
			stopMove = false;
		}
	}
}
