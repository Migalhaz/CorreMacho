using Game.Player;
using MigalhaSystem.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem
{
	[CreateAssetMenu(fileName = "Background", menuName = "Scriptable Object/Background")]
	public class Background : ScriptableObject
	{
		[SerializeField] List<BackgroundPart> m_backgrounds;
		public void DoSpawnObjects()
		{
			int spriteCount = m_backgrounds.Count;

            for (int i = 0; i < spriteCount; i++)
			{
				m_backgrounds[i].SpawnObject(i - spriteCount);
			}
		}

		public void MoveObjects()
		{
			foreach (BackgroundPart part in m_backgrounds)
			{
				part.MoveBackground();
            }
        }
	}

	[System.Serializable]
	public class BackgroundPart
	{
		[Header("Move Settings")]
		[SerializeField, Min(0)] float m_backgroundSpeed;
		[SerializeField] bool m_dynamicSpeed = true;
		[SerializeField] bool m_enableSpeedClamp = true;
		[SerializeField] FloatRange m_speedClamp;

		[Header("Object Settings")]
		[SerializeField] Vector2 m_backgroundSize;
		[SerializeField] Sprite m_backgroundSprite;

        Vector3 m_startPosition = Vector3.zero;
		Transform m_parent;
		public void SpawnObject(int index)
		{
			if (m_parent != null)
			{
                Object.Destroy(m_parent.gameObject);
            }
			m_parent = null;
			SpawnParent(index);
			SpawnLeftChild(index);
			SpawnRightChild(index);
        }

		void SpawnParent(int index)
		{
            m_parent = new GameObject(m_backgroundSprite.name).transform;
            m_parent.SetPositionAndRotation(m_startPosition, Quaternion.identity);
            SpriteRenderer spriteRenderer = m_parent.gameObject.GetOrAdd<SpriteRenderer>();

            spriteRenderer.sprite = m_backgroundSprite;
			spriteRenderer.sortingOrder = index;
        }

		void SpawnLeftChild(int index)
		{
            GameObject leftChild = new GameObject($"{m_backgroundSprite.name} Left Child", typeof(SpriteRenderer));
            leftChild.transform.SetPositionAndRotation(Vector3.left * m_backgroundSize.x, Quaternion.identity);
            leftChild.transform.parent = m_parent.transform;

            SpriteRenderer spriteRenderer = leftChild.gameObject.GetOrAdd<SpriteRenderer>();
            spriteRenderer.sprite = m_backgroundSprite;
            spriteRenderer.sortingOrder = index;
        }

		void SpawnRightChild(int index)
		{
            GameObject rightChild = new GameObject($"{m_backgroundSprite.name} Right Child", typeof(SpriteRenderer));
            rightChild.transform.SetPositionAndRotation(Vector3.right * m_backgroundSize.x, Quaternion.identity);
            rightChild.transform.parent = m_parent.transform;

            SpriteRenderer spriteRenderer = rightChild.gameObject.GetOrAdd<SpriteRenderer>();
            spriteRenderer.sprite = m_backgroundSprite;
            spriteRenderer.sortingOrder = index;
        }

		public void MoveBackground()
		{
			m_parent.Translate(GetTranslation(), Space.World);
			if (m_parent.transform.position.x <= -m_backgroundSize.x)
			{
				m_parent.transform.position = m_startPosition;
			}
		}

		float GetSpeed()
		{
			if (!m_dynamicSpeed) return m_backgroundSpeed;

            PlayerPoints  playerPoints = PlayerManager.ProvideInstance().m_PlayerPoints;
			if (!m_enableSpeedClamp)
			{
				return playerPoints.GetFinalSpeed(m_backgroundSpeed);
			}
			return playerPoints.GetFinalSpeed(m_backgroundSpeed, m_speedClamp.minValue, m_speedClamp.maxValue);
		}

		float GetFinalSpeed() => GetSpeed() * Time.deltaTime;
        Vector3 GetTranslation() => GetFinalSpeed() * Vector3.left;
    }
}