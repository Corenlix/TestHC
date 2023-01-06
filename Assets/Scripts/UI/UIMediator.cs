using UnityEngine;

namespace UI
{
    public class UIMediator : MonoBehaviour
    {
        public static UIMediator Instance { get; private set; }
        [SerializeField] private PointsCounter pointsCounter;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void UpdatePoints(int points) => pointsCounter.UpdatePoints(points);
    }
}