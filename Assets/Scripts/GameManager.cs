using System;
using System.Collections;
using System.Collections.Generic;
using Mystie.UI.Transition;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mystie.Core
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<GameState> onGameStateChanged;
        public static event Action onPause;
        public static event Action onUnpause;

        //public GameSettings gameSettings { get; private set; }
        public SystemDataScriptable systemData { get; private set; }
        public InputManager inputManager { get; private set; }
        public SceneTransitioner sceneTransitioner { get; private set; }
        public CursorManager cursor { get; private set; }

        //public BondManager bondManager { get; private set; }

        public static GameState gameState { get; private set; }
        public static bool isPaused { get; private set; } = false;

        public static string playerName;

        private const string systemDataPath = "System Data";


        #region Singleton

        public static GameManager Instance
        {
            get
            {
                if (instance == null) Instantiate();
                return instance;
            }
        }

        protected static GameManager instance;

        #endregion

        private static GameManager Instantiate()
        {
            GameObject gmObj = new GameObject("Game Manager");
            instance = gmObj.AddComponent<GameManager>();
            instance.Initialize();

            return instance;
        }

        private void Initialize()
        {
            DontDestroyOnLoad(gameObject);

            //gameState = GameState.StartScreen;

            isPaused = false;

            //SceneManager.sceneLoaded += OnSceneLoaded;

            systemData = Resources.Load<SystemDataScriptable>(systemDataPath);
            if (systemData == null) Debug.LogError("GameManager: System Data not found.");


            inputManager = new InputManager();

            Cursor.visible = false;
            cursor = Instantiate(systemData.cursorPrefab);

            sceneTransitioner = SceneTransitioner.Instance;
        }

        /*IEnumerator Start()
        {
            // Wait for the localization system to initialize, loading Locales, preloading etc.
            //yield return LocalizationSettings.InitializationOperation;
            //gameSettings.LoadLocale();
        }*/

        public static void SetGameState(GameState state)
        {
            if (gameState != state) return;

            gameState = state;
            onGameStateChanged?.Invoke(gameState);
        }

        public static void Pause()
        {
            if (isPaused) return;

            gameState = GameState.Pause;

            Time.timeScale = 0f;
            isPaused = true;
            onPause?.Invoke();
        }

        public static void Unpause()
        {
            if (!isPaused) return;

            gameState = GameState.Play;

            Time.timeScale = 1f;
            isPaused = false;
            onUnpause?.Invoke();
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Unpause();
        }

        public void LoadMainMenu()
        {
            Debug.Log("Loading main menu...");
            gameState = GameState.MainMenu;
            sceneTransitioner.LoadScene(systemData.mainMenuScene, SceneTransitionMode.Fade);
        }

        public void LoadCamp()
        {
            Debug.Log("Loading camp...");
            gameState = GameState.Camp;
            sceneTransitioner.LoadScene(systemData.campScene, SceneTransitionMode.Fade);
        }

        public void Quit()
        {
            Debug.Log("Quitting the game...");

#if UNITY_EDITOR

            if (UnityEditor.EditorApplication.isPlaying == true)
                UnityEditor.EditorApplication.isPlaying = false;

#endif

            Application.Quit();
        }
    }

    public enum GameState
    {
        StartScreen,
        MainMenu,
        Camp,
        Play,
        Pause,
        Gameover
    }
}
