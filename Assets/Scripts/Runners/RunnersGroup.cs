using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runners
{
    public class RunnersGroup : MonoBehaviour
    {
        [SerializeField] private float groupWidth;
        [SerializeField] private float groupHeight;
        [SerializeField] private int startRowsRunners = 2;
        [SerializeField] private int startColumnsRunners = 6;
        [FormerlySerializedAs("runnerUnitPrefab")] [SerializeField] private Runner runnerPrefab;
        [SerializeField] private float speed;
        private readonly List<Runner> _runners = new();

        private void Start()
        {
            SpawnStartRunners();
        }

        private void Update()
        {
            transform.position += Vector3.forward * (speed * Time.deltaTime);
        }

        public void AddRunner(Vector3 localPosition)
        {
            SpawnRunner(localPosition);
        }

        public void GroupRunners(List<Vector3> relativePositions)
        {
            float offset = (float)relativePositions.Count / _runners.Count;
            for (int i = 0; i < _runners.Count; i++)
            {
                Runner runner = _runners[i];
                Vector3 position = relativePositions[(int)(offset * i)];
                position.x -= 0.5f;
                position.x *= groupWidth;
                position.y *= groupHeight;
                runner.SetDestination(new Vector3(position.x, 0, position.y));
            }
        }

        private void SpawnStartRunners()
        {
            float xOffset = groupWidth / (startColumnsRunners - 1);
            float startX = -groupWidth / 2f;
            float zOffset = 1f;
            float startZ = 0;
            for (int i = 0; i < startRowsRunners; i++)
            {
                for (int j = 0; j < startColumnsRunners; j++)
                {
                    var position = new Vector3(
                        startX + xOffset * j,
                        0,
                        startZ + zOffset * i);
                    SpawnRunner(position);
                }
            }
        }

        private void SpawnRunner(Vector3 localPosition)
        {
            Runner runner = Instantiate(runnerPrefab, transform);
            runner.transform.localPosition = localPosition;
            runner.Died += OnRunnerDied;
            _runners.Add(runner);
        }

        private void OnRunnerDied(Runner runner)
        {
            _runners.Remove(runner);
            runner.transform.SetParent(null);
            runner.Died -= OnRunnerDied;
        }
    }
}