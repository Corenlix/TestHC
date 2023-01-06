using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PointsCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        private void Start()
        {
            counterText.text = "0";
        }

        public void UpdatePoints(int points)
        {
            counterText.text = Convert.ToString(points);
        }
    }
}