using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Cert_Project.Scripts.Spawner
{
	public class EnemiesSpawner : MonoBehaviour
	{
		[Header("Class references")]
		[SerializeField] private EnemyWaveData[] enemyDataArray;
		[SerializeField] private EnemyWaveData currentEnemyWaveData;

		[Header("Waves info")]
		[SerializeField] private int minWaveIndex;
		[SerializeField] private int maxWaveIndex;
		[SerializeField] private int nextWave;

		[Header(" ")]
		[SerializeField] private GameObject enemyPrefab;
		[SerializeField] private GameObject spawnPosition;

		[SerializeField] private int maxEnemies;
		[SerializeField] private int currentEnemies;
		[SerializeField] private bool spawnEnabled;

		private Vector2 startPosition;

		public static event Action OnEnemyL1WaveStarted;
		public static event Action OnMidBossWaveStarted;
		public static event Action OnFinalBossWaveStarted;

		private void OnEnable()
		{
			Enemy.Enemy.OnEnemyDestroyed += DecreaseEnemiesAmount;
			Manager.GameManager.OnGameStarted += StartNewWave;
			Manager.GameManager.OnGameOver += DisableSpawning;
		}

		private void OnDisable()
		{
			Enemy.Enemy.OnEnemyDestroyed -= DecreaseEnemiesAmount;
			Manager.GameManager.OnGameStarted -= StartNewWave;
			Manager.GameManager.OnGameOver -= DisableSpawning;
			StopAllCoroutines();
		}

		private void Start()
		{
			startPosition = spawnPosition.transform.position;
			nextWave = minWaveIndex;
			EnableSpawning();
		}

		public int GetCurrentWave() => nextWave;

		private void DecreaseEnemiesAmount(int n)
		{
			currentEnemies--;

			if (currentEnemies < 0) Debug.LogError("current enemies is less than 0");

			if (currentEnemies <= 0)
			{
				Debug.Log($"enemies count: {currentEnemies}");
				DisableSpawning();
				StartNewWave();
			}
		}

		private void IncreaseEnemiesAmount() => currentEnemies++;

		public void StartNewWave()
		{
			if (nextWave < minWaveIndex || nextWave > maxWaveIndex)
			{
				Debug.LogWarning("check current wave value");
				return;
			}

			if (nextWave is 7) OnMidBossWaveStarted?.Invoke(); // todo check 
			else if (nextWave is 13) OnFinalBossWaveStarted?.Invoke(); // todo check
			else OnEnemyL1WaveStarted?.Invoke(); // todo check

			if (nextWave < maxWaveIndex) StartCoroutine(nameof(EnemyWaveRoutine));
			nextWave++;
		}

		private IEnumerator EnemyWaveRoutine()
		{
			currentEnemyWaveData = enemyDataArray[nextWave];

			yield return new WaitForSeconds(3);

			EnableSpawning();
			currentEnemies = 0;
			maxEnemies = currentEnemyWaveData.maxEnemies;
			enemyPrefab = currentEnemyWaveData.enemyPrefab;

			while (currentEnemies < maxEnemies && spawnEnabled)
			{
				Instantiate(enemyPrefab, startPosition, quaternion.identity);
				IncreaseEnemiesAmount();
				yield return new WaitForSeconds(1);
			}
		}

		private void EnableSpawning() => spawnEnabled = true;
		private void DisableSpawning() => spawnEnabled = false;
	}
}