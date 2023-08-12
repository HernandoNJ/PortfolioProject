using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.DungeonEscapeC3Mobile.Scripts.Enemy
{
	public class SpiderAnimEvent : MonoBehaviour
	{
		[SerializeField] private Spider spider;
		[SerializeField] private GameObject acid;

		public void FireAcid()
		{
			spider.SpiderAttack();
		}
	}
}
