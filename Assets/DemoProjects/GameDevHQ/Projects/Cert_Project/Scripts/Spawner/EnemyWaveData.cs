using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Cert_Project.Scripts.Spawner
{
    [CreateAssetMenu(fileName = "EnemyWaveData", menuName = "Scriptable Obj/EnemyWaveData")]
    public class EnemyWaveData : ScriptableObject
    {
        public GameObject enemyPrefab;
        public int maxEnemies;

    }
}