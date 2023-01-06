using System.Collections;
using Runners;
using UnityEngine;

namespace Bonuses
{
    public class Romb : MonoBehaviour
    {
        private const float AddPointsDelay = 0.6f;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem particles;
        private bool _picked;


        private void OnTriggerEnter(Collider other)
        {
            if (_picked) return;

            if (other.TryGetComponent(out Runner runner))
            {
                _picked = true;
                animator.SetTrigger("Pick");
                Instantiate(particles, transform.position, Quaternion.identity);
                StartCoroutine(PickCoroutine());
            }
        }

        private IEnumerator PickCoroutine()
        {
            yield return new WaitForSeconds(AddPointsDelay);
            PlayerStats.Instance.AddPoints(1);
        }
    }
}