using Mystie.UI;
using NaughtyAttributes;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Mystie.Core
{
    public class LevelManager : MonoBehaviour
    {
        public UIState victoryState;

        //public GameObject player { get; private set; }
        //[field: SerializeField] public GameObject playerObj { get; private set; }
        //[field: SerializeField] public GameObject playerPrefab { get; private set; }
        //[field: SerializeField] public Transform respawnPoint { get; private set; }
        //[field: SerializeField] private float respawnTime = 4f;

        [field: Scene] public List<string> scenes;
        public int sceneIndex;

        private Transform currentRespawn;

        private static LevelManager _instance;
        public static LevelManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindAnyObjectByType<LevelManager>();
                return _instance;
            }
        }

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            //currentRespawn = respawnPoint;

            //GameObject playerInstance = Instantiate(playerPrefab.gameObject, currentRespawn.position, Quaternion.identity);
        }

        public void CompleteLevel()
        {
            Debug.Log("Complete level");
            victoryState.SetState();
        }

        public void NextLevel()
        {
            Debug.Log("Next level");
            SceneManager.LoadScene(scenes[sceneIndex + 1]);
        }

        /*

        private void OnDestroy()
        {
        }

        public void SetRespawnPoint(Transform newRespawn)
        {
            currentRespawn = newRespawn;
        }

        public void OnPlayerDeath(GameObject player)
        {
            StartCoroutine(OnResapwn(player));
        }

        private IEnumerator OnResapwn(GameObject player)
        {
            player.gameObject.SetActive(false);

            yield return new WaitForSeconds(respawnTime);

            player.gameObject.transform.position = currentRespawn.position;
            player.gameObject.SetActive(true);
            player.gameObject.transform.position = respawnPoint.position;
        }*/
    }

}
