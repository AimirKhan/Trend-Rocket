using System;
using Events;
using UniRx;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer playerSprite;
        [SerializeField] private SpriteRenderer ball;
        [SerializeField] private float playerVelocity;

        private float currentDirection;
        private GlobalEvents globalEvents;
        private GlobalVariables globalVariables;
        private float playerRangeMin;
        private float playerRangeMax;

        private void Awake()
        {
            globalEvents = GlobalEvents.Instance;
            globalVariables = GlobalVariables.Instance;
        }

        public void Init(float rangeMin, float rangeMax, float velocity)
        {
            playerRangeMin = rangeMin;
            playerRangeMax = rangeMax;
            playerVelocity = velocity;
            Observable.EveryUpdate()
                .Where(ctx => currentDirection != 0)
                .Subscribe(ctx => MovePlayer())
                .AddTo(this);
        }

        private void OnEnable()
        {
            globalEvents.OnMovePlayer += ChangeDirection;
            UpdatePreviewHero();
        }

        private void ChangeDirection(int side)
        {
            currentDirection = side;
        }

        private void MovePlayer()
        {
            var position = transform.position;
            var clampY = Mathf.Clamp(
                position.y + currentDirection * playerVelocity * Time.deltaTime,
                playerRangeMin, playerRangeMax);
            position = new Vector3(position.x, clampY, position.z);
            transform.position = position;
        }
        
        private void UpdatePreviewHero()
        {
            var zombie = globalVariables.zombies
                [globalVariables.CurrentZombie.Value].Sprite;
            var hero = playerSprite;
            hero.sprite = zombie;
            hero.size = zombie.rect.size;
        }
        
        private void OnDisable()
        {
            globalEvents.OnMovePlayer -= ChangeDirection;
        }
    }
}
