using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MigalhaSystem.Extensions;

namespace Game.Obstacles
{
    public class LocalRotationAnimation : MonoBehaviour
    {
        [SerializeField] Vector3 m_startRotation;
        [SerializeField] Vector3 m_finalRotation;
        [SerializeField] FloatRange m_duration;
        
        void Start()
        {
            if (ChancePicker.PickChance(50))
            {
                StartAnimation(m_startRotation, m_finalRotation);
            }
            else
            {
                StartAnimation(m_finalRotation, m_startRotation);
            }
        }

        void StartAnimation(Vector3 startRotation, Vector3 finalRotation)
        {
            transform.localEulerAngles = startRotation;
            transform.DOLocalRotate(finalRotation, m_duration.GetRandomValue()).
                SetLoops(-1, LoopType.Yoyo).
                SetEase(Ease.InOutQuad);
        }
    }
}
