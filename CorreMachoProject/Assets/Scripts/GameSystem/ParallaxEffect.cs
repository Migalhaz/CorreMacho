using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] Transform m_visualGameObject;
        [SerializeField, Min(0)] float m_speed;
        [SerializeField, Min(0)] float m_xSize;
        [SerializeField] float m_leftBound;
        [SerializeField] float m_rightBound;
        [SerializeField, Min(0)] float m_gizmosSize;
        private void Update()
        {
            MoveVisual();
        }

        void MoveVisual()
        {
            if (!m_visualGameObject) return;
            LeftBoundCheck();
            m_visualGameObject.localPosition += GetSpeed() * Vector3.left;
        }

        void LeftBoundCheck()
        {
            float xSize = m_xSize * .5f;
            float xBoundPosition = m_visualGameObject.localPosition.x - xSize;

            if (xBoundPosition <= m_leftBound)
            {
                float startXPosition = m_rightBound - xSize;
                Vector3 rightBoundPosition = m_visualGameObject.localPosition.With(x: startXPosition);
                m_visualGameObject.localPosition = rightBoundPosition;
            }
        }

        float GetSpeed()
        {
            return m_speed * Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            Vector2 gameObjectPos = transform.position;
            Vector2 visualPos = m_visualGameObject ? m_visualGameObject.position : transform.position;

            Gizmos.color = Color.red;
            Vector3 leftBoundPos = new(gameObjectPos.x + m_leftBound, visualPos.y);
            Gizmos.DrawSphere(leftBoundPos, m_gizmosSize);
            

            Gizmos.color = Color.green;
            Vector3 rightBoundPos = new(gameObjectPos.x + m_rightBound, visualPos.y);
            Gizmos.DrawSphere(rightBoundPos, m_gizmosSize);
            
            
            Gizmos.color = Color.blue;

            float xSize = m_xSize * .5f;
            Vector2 lineStartPos = visualPos.With(x: visualPos.x - xSize);
            Vector2 lineEndPos = visualPos.With(x: visualPos.x + xSize);
            Gizmos.DrawLine(lineStartPos, lineEndPos);

        }
    }
}
