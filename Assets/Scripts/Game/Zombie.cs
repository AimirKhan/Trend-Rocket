using System;
using DG.Tweening;
using Events;
using Services;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Zombie : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer zombieSprite;
        [SerializeField] private float enemyVelocityMin = 10;
        [SerializeField] private float enemyVelocityMax = 15;
        [SerializeField] private float[] respawnDelay = {0, 2};
        [Header("Animation")]
        [SerializeField] private float animOffsetScale = .05f;
        [SerializeField] private Vector3 startScale;

        private EGameState gameState;
        private float zombieVelocity;
        private float currentDirection = -0.1f;
        private Vector2 targetPosition;
        private GlobalEvents globalEvents;
        private GlobalVariables globalVariables;
        private float respawnSide;
        private CapGame capGame;
        private float nextFrame;
        
        private void Awake()
        {
            globalEvents = GlobalEvents.Instance;
            globalVariables = GlobalVariables.Instance;
            globalVariables.GameState
                .Subscribe(ctx => gameState = ctx)
                .AddTo(this);
            
            startScale = transform.localScale;
        }
        
        public void InitZombie(float side, CapGame cap)
        {
            respawnSide = side;
            capGame = cap;
            Observable.EveryUpdate()
                .Where(_ => gameState == EGameState.Started || gameState == EGameState.Continued)
                .Subscribe(_ => MoveZombie())
                .AddTo(this);
            globalVariables.GameState
                .Where(ctx => ctx == EGameState.Started || ctx == EGameState.Stopped)
                .Subscribe(_ =>
                {
                    RespawnZombie();
                    SetZombieSprite();
                    RespawnDelay();
                    ZombieAnim();
                }).AddTo(this);
        }

        public void InitZombieMove(Vector2 targetPos)
        {
            targetPosition = targetPos;
        }

        private void MoveZombie()
        {
            var position = transform.position;
            var positionX = position.x;
            if (positionX < respawnSide)
            {
                SetZombieSprite();
                position = RespawnZombie();
                positionX = position.x;
                zombieVelocity = 0;
                RespawnDelay();
            }
            if (Time.frameCount >= nextFrame)
            {
                zombieVelocity = Random.Range(enemyVelocityMin, enemyVelocityMax);
            }
            var posX = positionX + currentDirection * zombieVelocity * Time.deltaTime;
            position = new Vector3(posX, position.y, position.z);
            transform.position = position;
        }

        private void SetZombieSprite()
        {
            var zombieId = Random.Range(0, globalVariables.zombies.Count);
            var zombie = zombieSprite.sprite = globalVariables.zombies[zombieId].Sprite;
            var hero = zombieSprite;
            hero.sprite = zombie;
            hero.size = zombie.rect.size;
        }

        private Vector3 RespawnZombie()
        {
            var enemySpawnY = Random.Range(capGame.EnemySpawnEnd.position.y,
                capGame.EnemySpawnStart.position.y);
            var result = new Vector3(-respawnSide, enemySpawnY, enemySpawnY);
            transform.position = result;
            return result;
        }

        private void RespawnDelay()
        {
            var delay = Random.Range(respawnDelay[0], respawnDelay[1]);
            delay *= 60;
            nextFrame = Time.frameCount + delay;
        }

        private void ZombieAnim()
        {
            var scale = new Vector3(startScale.x + animOffsetScale, startScale.y - animOffsetScale, startScale.z);
            transform.DOScale(scale, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}