using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.PuzzleProject.Scripts
{
	public class PressurePad : MonoBehaviour
	{
		[SerializeField] private float distance;
		[SerializeField] private GameObject winText;


		private void OnTriggerStay(Collider other)
		{
			distance = Vector3.Distance(transform.position, other.transform.position);

			if (other.CompareTag("MovingBox") && distance < 0.1f)
			{
				other.GetComponent<Rigidbody>().isKinematic = true;
				GetComponentInChildren<Renderer>().material.color = Color.blue;
				winText.SetActive(true);
			}
		}
	}
}
