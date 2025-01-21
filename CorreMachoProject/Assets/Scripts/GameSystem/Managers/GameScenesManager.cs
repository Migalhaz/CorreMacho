using MigalhaSystem.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameSystem
{
    public class GameScenesManager : Singleton<GameScenesManager>
    {
        [Header("Scene Index")]
        [SerializeField, Min(0)] int m_menuScene;
        [SerializeField, Min(0)] int m_gameScene;
        [SerializeField, Min(0)] int m_deathScreenScene;

        public int MenuScene => m_menuScene;
        public int GameScene => m_gameScene;
        public int DeathScreenScene => m_deathScreenScene;

        Dictionary<int, LoadSceneMode> m_loadedScenes = new();
        object m_data;

        private void OnEnable()
        {
            m_loadedScenes ??= new();
            SceneManager.sceneLoaded += AddSceneToLoadedList;
            SceneManager.sceneUnloaded += RemoveSceneToLoadedList;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= AddSceneToLoadedList;
            SceneManager.sceneUnloaded -= RemoveSceneToLoadedList;
        }

        public T GetData<T>()
        {
            if (m_data == null)
            {
                Debug.LogWarning("Data is null!");
                return default;
            }
            if (m_data is T data)
            {
                m_data = null;
                return data;
            }
            try
            {
                data = (T)m_data;
                m_data = null;
                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
                return default;
            }
        }

        public void LoadSingleSceneWithData(int sceneIndex, object data)
        {
            m_data = data;
            LoadSingleScene(sceneIndex);
        }

        void AddSceneToLoadedList(Scene scene, LoadSceneMode loadSceneMode)
        {
            m_loadedScenes ??= new();
            m_loadedScenes.Add(scene.buildIndex, loadSceneMode);
        }

        void RemoveSceneToLoadedList(Scene scene)
        {
            m_loadedScenes ??= new();
            int sceneBuildIndex = scene.buildIndex;
            if (!IsSceneLoaded(sceneBuildIndex)) return;
            m_loadedScenes.Remove(sceneBuildIndex);
        }

        public bool IsSceneLoaded(int sceneIndex)
        {
            m_loadedScenes ??= new();
            return m_loadedScenes.ContainsKey(sceneIndex);
        }

        public void LoadSingleScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }

        public void LoadAdditiveScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
        }

        public void UnloadAditiveScene(int sceneIndex)
        {
            if (!IsSceneLoaded(sceneIndex)) return;
            if (m_loadedScenes[sceneIndex] != LoadSceneMode.Additive) return;
            StartCoroutine(UnloadSceneProccess(sceneIndex));
        }

        IEnumerator UnloadSceneProccess(int SceneIndex)
        {
            yield return SceneManager.UnloadSceneAsync(SceneIndex);
        }
    }
}
