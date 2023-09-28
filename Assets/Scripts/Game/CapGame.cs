using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Services;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class CapGame : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [Header("Player")] [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private float playerVelocity;
        [SerializeField] private Transform playerRangeMin;
        [SerializeField] private Transform playerRangeMax;

        [Header("Enemies")] [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemiesCount = 3;
        [SerializeField] private Transform enemySpawnEnd;
        [SerializeField] private Transform enemySpawnStart;

        public Transform EnemySpawnEnd => enemySpawnEnd;
        public Transform EnemySpawnStart => enemySpawnStart;

        private float respawnSides;
        private GameObject playerGameObject;
        private Player player;
        private List<Zombie> zombies = new();

        private void Awake()
        {
            SetScreenLimits();
            SetEnemiesCount();
            GlobalVariables.Instance.GameState
                .Where(ctx => ctx == EGameState.Started)
                .Subscribe(ctx =>
                {
                    SpawnPlayer();
                    SpawnEnemies();
                }).AddTo(this);
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

        private void SpawnEnemies()
        {
            foreach (var zombie in zombies)
            {
                zombie.InitZombieMove(player.transform.position);
            }
        }

        private void SetEnemiesCount()
        {
            for (int i = 0; i < enemiesCount; i++)
            {
                var enemy = Instantiate(enemyPrefab.GetComponent<Zombie>(), enemySpawnStart);
                enemy.InitZombie(respawnSides, this);
                zombies.Add(enemy);
            }
        }

        private void SetScreenLimits()
        {
            var offset = Screen.width / 10;
            var playerSpawnPosX = GetScreenToWorldPos(new Vector3(offset, Screen.height));
            playerSpawn.position = new Vector2(playerSpawnPosX.x, playerSpawn.position.y);

            var enemySpawnOffset = Screen.width + offset;
            var enemySpawnPosX = GetScreenToWorldPos(new Vector3(enemySpawnOffset, Screen.height));
            enemySpawnEnd.position = new Vector2(enemySpawnPosX.x, enemySpawnEnd.position.y);
            enemySpawnStart.position = new Vector2(enemySpawnPosX.x, enemySpawnStart.position.y);
            respawnSides = -enemySpawnPosX.x;
        }

        private Vector2 GetScreenToWorldPos(Vector2 position)
        {
            return camera.ScreenToWorldPoint(new Vector3(position.x, position.y, 0));
        }
    }
}