using DG.Tweening;
using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Obstacles
{
    public class LocalYMoveAnimation : MonoBehaviour
    {
        [SerializeField] float m_localYValue;
        [SerializeField] FloatRange m_duration;
        void Start()
        {
            float yThreshold = transform.localPosition.y;
            if (ChancePicker.PickChance(50))
            {
                StartAnimation(yThreshold + m_localYValue, yThreshold - m_localYValue);
            }
            else
            {
                StartAnimation(yThreshold - m_localYValue, yThreshold + m_localYValue);
            }
        }

        void StartAnimation(float startValue, float finalValue)
        {
            transform.localPosition = transform.localPosition.With(y: startValue);
            transform.DOLocalMoveY(finalValue, m_duration.GetRandomValue()).
                SetEase(Ease.InOutQuad).
                SetLoops(-1, LoopType.Yoyo);
        }
    }
}
