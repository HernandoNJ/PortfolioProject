using UnityEngine;

public class Lives : MonoBehaviour
{
	[SerializeField] public GameObject[] lives;
	private int livesCount = 3;

	private void Start() => TappyBirdGameManager.OnGameoverConfirmedEvent += LivesChecker;

	private void LivesChecker()
	{
		livesCount--;
		lives[livesCount].SetActive(false);
	}

	private void OnDestroy() => TappyBirdGameManager.OnGameoverConfirmedEvent -= LivesChecker;

}