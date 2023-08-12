using UnityEngine;

namespace Assets.DemoProjects.GameDevHQ.Projects.Cert_Project.Scripts
{
    public class Explosion : MonoBehaviour
    {
        public AudioClip explosionSound;

        private void OnEnable()
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
    }
}
