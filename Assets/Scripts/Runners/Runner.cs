using System;
using UnityEngine;

namespace Runners
{
    public class Runner : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deathParticles;
        [SerializeField] private Animator animator;
        [SerializeField] private float speed = 2f;
        public event Action<Runner> Died;
        private Vector3 _destination;
        private bool _isDead;

        private void Start()
        {
            _destination = transform.localPosition;
        }

        private void Update()
        {
            if (_isDead) return;

            Vector3 direction = _destination - transform.localPosition;
            Vector3 moveVector = direction * (speed * Time.deltaTime);
            if (moveVector.sqrMagnitude >= Vector3.SqrMagnitude(transform.localPosition - _destination))
            {
                transform.localPosition = _destination;
            }
            else
            {
                transform.localPosition += moveVector;
            }
        }

        public void SetDestination(Vector3 destination)
        {
            _destination = destination;
            _destination.y = transform.localPosition.y;
        }

        public void Die()
        {
            if (_isDead)
                return;

            animator.SetTrigger("Death");
            _isDead = true;
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Died?.Invoke(this);
        }
    }
}