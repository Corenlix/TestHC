using UnityEngine;

namespace Obstacles
{
    public class Saw : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform currentTarget;
        [SerializeField] private Transform nextTarget;

        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        
            Vector3 direction = Vector3.Normalize(currentTarget.position - transform.position);
            Vector3 moveVector = direction * (moveSpeed * Time.deltaTime);
            if (moveVector.sqrMagnitude >= Vector3.SqrMagnitude(transform.position - currentTarget.position))
            {
                transform.position = currentTarget.position;
                (nextTarget, currentTarget) = (currentTarget, nextTarget);
            }
            else
            {
                transform.position += moveVector;
            }
        }
    }
}