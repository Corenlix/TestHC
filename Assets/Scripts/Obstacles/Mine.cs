using Runners;
using UnityEngine;

namespace Obstacles
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionParticles;
        [SerializeField] private float explosionRadius;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Runner runner))
            {
                Instantiate(explosionParticles, transform.position, Quaternion.identity);
                var targets = Physics.OverlapSphere(transform.position, explosionRadius);
                foreach (var target in targets)
                {
                    if (target.TryGetComponent(out Runner targetRunner))
                    {
                        targetRunner.Die();
                    }
                }
                
                Destroy(gameObject);
            }
        }
    }
}