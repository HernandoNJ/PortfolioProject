using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Platformer_2D.Training.Scripts
{
	public class ElevatorPanel : MonoBehaviour
	{
		[SerializeField] private Elevator elevator;
		[SerializeField] private Renderer callButtonRend;
		[SerializeField] private int requiredCoins = 8;
		[SerializeField] private bool eKeyPressed;
		[SerializeField] private Color buttonColor;

		private void Start()
		{
			buttonColor = Color.red;
			callButtonRend.material.color = buttonColor;
		}

		private void Update()
		{
			eKeyPressed = Input.GetKeyDown(KeyCode.E);
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				var coinsCount = other.GetComponent<PuzzleProject.Scripts.Player>().CoinsCount();

				if (eKeyPressed && coinsCount >= requiredCoins)
				{
					elevator.CallElevator();

					if (buttonColor == Color.red)
						buttonColor = Color.green;
					else if (buttonColor == Color.green)
						buttonColor = Color.red;
					callButtonRend.material.color = buttonColor;
				}
			}
		}
	}
}
