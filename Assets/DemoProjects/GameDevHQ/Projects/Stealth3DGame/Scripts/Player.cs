using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.DemoProject.GameDevHQ.Projects.Stealth3DGame.Scripts
{
	public class Player : MonoBehaviour
	{
		private Animator animator;
		private Camera mainCam;
		private NavMeshAgent agent;
		private Vector3 destination;

		public GameObject coinPrefab;
		public bool coinLaunched;

		private const string walk = "isWalking";
		private const string throwCoin = "ThrowCoin";

		public static event Action<Vector3> OnCoinLaunched;

		private void Start()
		{
			mainCam = Camera.main;
			if (mainCam == null)
				Debug.Log("Main cam is null");
			Debug.Log("Main cam pos: " + mainCam.transform.position);
			agent = GetComponent<NavMeshAgent>();
			animator = GetComponentInChildren<Animator>();
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
				MovePlayer();
			if (Input.GetMouseButtonDown(1))
				LaunchCoin();
			CheckDistance();
		}

		private void MovePlayer()
		{
			Ray rayOrigin = mainCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			if (Physics.Raycast(rayOrigin, out hitInfo))
			{
				agent.SetDestination(hitInfo.point);
				animator.SetBool(walk, true);
				destination = hitInfo.point;
			}
		}

		private void CheckDistance()
		{
			var newDistance = Vector3.Distance(transform.position, destination);
			if (newDistance < 1f)
				animator.SetBool(walk, false);
		}

		private void LaunchCoin()
		{
			if (coinLaunched)
				return;

			var rayOrigin = mainCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;

			if (!Physics.Raycast(rayOrigin, out hitInfo))
				return;

			Instantiate(coinPrefab, hitInfo.point, Quaternion.identity);
			animator.SetTrigger(throwCoin);
			coinLaunched = true;
			OnCoinLaunched?.Invoke(hitInfo.point);
		}
	}
}
