using Game.Player;
using MigalhaSystem.Extensions;
using MigalhaSystem.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Obstacles
{
    public class ObstacleLogic : MonoBehaviour
    {
        [Header("LifeTime Settings")]
        [SerializeField] PoolData m_obstaclePool;
        [SerializeField] Timer m_lifeTime;

        [Header("Move Settings")]
        [SerializeField, Min(0)] float m_speed;
        [SerializeField, Min(1)] float m_speedBuffer;
        [SerializeField] FloatRange m_speedRange;

        private void OnEnable()
        {
            m_lifeTime.StartTimer();
        }

        float GetMoveSpeed()
        {
            PlayerManager player = PlayerManager.ProvideInstance();
            int points = player.m_PlayerPoints.Points;
            float buff = points / m_speedBuffer;
            float finalSpeed = m_speedRange.Clamp(m_speed + buff);
            Debug.Log($"MoveSpeed: {finalSpeed}");
            return finalSpeed;
        }

        private void Update()
        {
            Move();
            LifeTime();
        }

        void Move()
        {
            float moveSpeed = GetMoveSpeed() * Time.deltaTime;
            Vector3 finalMoveVector = moveSpeed * Vector3.left;
            transform.Translate(finalMoveVector, Space.World);
        }

        void LifeTime()
        {
            if (m_lifeTime.TimerElapse(Time.deltaTime))
            {
                m_obstaclePool.PushObject(gameObject);
            }
        }
    }
}
