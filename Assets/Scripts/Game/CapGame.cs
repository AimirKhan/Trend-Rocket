using System;
using Events;
using UnityEngine;

namespace Game
{
    public class CapGame : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private float playerVelocity;
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private Transform playerRangeMin;
        [SerializeField] private Transform playerRangeMax;
        [SerializeField] private Transform enemySpawnEnd;
        [SerializeField] private Transform enemySpawnStart;

        private GameObject playerGameObject;
        private Player player;

        private void Awake()
        {
            GlobalEvents.Instance.OnGameState += ctx =>
            {
                if (ctx == EGameState.Started)
                {
                    SpawnPlayer();
                }
            };
        }

        private void SpawnPlayer()
        {
            if (playerGameObject != null)
            {
                playerGameObject.transform.position = playerSpawn.position;
                return;
            }
            
            playerGameObject = Instantiate(playerPrefab, playerSpawn);
            player = playerGameObject.GetComponent<Player>();
            player.Init(playerRangeMin.localPosition.y,
                playerRangeMax.localPosition.y, playerVelocity);
        }
    }
}
