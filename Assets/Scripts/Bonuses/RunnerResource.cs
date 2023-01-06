using Runners;
using UnityEngine;

namespace Bonuses
{
    public class RunnerResource : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Runner runner))
            {
                FindObjectOfType<RunnersGroup>().AddRunner(runner.transform.localPosition + Vector3.forward);
                Instantiate(particles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}