using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerPoints : MonoBehaviour
    {
        [SerializeField, Min(0)] float m_basePointsAdd = 1;
        [SerializeField, Min(1)] float m_secondsToBuffPoints = 30;

        [Header("Game Speed")]
        [SerializeField, Min(1)] float m_speedBuffer;

        float m_points;
        float m_timeAlive = 0f;

        public int Points => Mathf.FloorToInt(m_points);
        public float TimeAlive => m_timeAlive;

        private void OnEnable()
        {
            m_timeAlive = 0;
            m_points = 0;
        }

        private void Update()
        {
            if (Time.timeScale <= 0) return;
            m_timeAlive += Time.deltaTime;
            m_points += m_basePointsAdd + PointsBuffer();
            PlayerPointsObserver.UpdatePoint(Points);
        }

        float PointsBuffer()
        {
            float wholeTimeInt = Mathf.FloorToInt(TimeAlive / m_secondsToBuffPoints);
            return wholeTimeInt;
        }

        public float GetFinalSpeed(float currentSpeed)
        {
            float buff = m_points / m_speedBuffer;
            float finalSpeed = currentSpeed + buff;
            return finalSpeed;
        }

        public float GetFinalSpeed(float currentSpeed, float? minValue = null, float? maxValue = null)
        {
            float buff = m_points / m_speedBuffer;
            float finalSpeed = Mathf.Clamp(currentSpeed + buff, minValue ?? -9999, maxValue ?? 9999);
            return finalSpeed;
        }
    }
}
