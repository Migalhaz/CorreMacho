using Game.GameSystem;
using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.HUD
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class RecordPointCount : MonoBehaviour
    {
        TMPro.TextMeshProUGUI m_pointCountTMPro;
        const string POINTKEY = "Points";

        void Start()
        {
            Setup();
        }

        int GetRecord()
        {
            int lastPoints = GameScenesManager.ProvideInstance().GetData<int>();
            return lastPoints;

            //int playerPrefs = PlayerPrefs.GetInt(POINTKEY, 0);
            //if (playerPrefs >= lastPoints) return playerPrefs;
            //return lastPoints;
        }

        void Setup()
        {
            int record = GetRecord();
            SaveRecord(record);
            SetText(record);
        }

        void SaveRecord(int record)
        {
            //PlayerPrefs.SetInt(POINTKEY, record);
        }

        void SetText(int record)
        {
            if (!m_pointCountTMPro)
            {
                m_pointCountTMPro = gameObject.GetOrAdd<TMPro.TextMeshProUGUI>();
                if (!m_pointCountTMPro) return;
            }

            m_pointCountTMPro.text = $"{record}m";
        }

        private void Reset()
        {
            m_pointCountTMPro = gameObject.GetOrAdd<TMPro.TextMeshProUGUI>();
        }
    }
}
