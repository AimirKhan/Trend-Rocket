using DG.Tweening;
using Events;
using Services;
using UniRx;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer playerSprite;
        [SerializeField] private float playerVelocity;
        [Header("Animation")]
        [SerializeField] private float animOffsetScale = .05f;
        [SerializeField] private float easeSpeed = .7f;
        [SerializeField] private Vector3 startScale;

        [SerializeField] private EGameState gameState;
        private float currentDirection;
        private GlobalEvents globalEvents;
        private GlobalVariables globalVariables;
        private float playerRangeMin;
        private float playerRangeMax;

        private void Awake()
        {
            startScale = transform.localScale;
            globalEvents = GlobalEvents.Instance;
            globalVariables = GlobalVariables.Instance;
            globalVariables.GameState
                .Subscribe(ctx => gameState = ctx)
                .AddTo(this);
            Observable.EveryUpdate()
                .Where(_ => currentDirection != 0
                            & gameState == EGameState.Started
                            || gameState == EGameState.Continued)
                .Subscribe(_ => MovePlayer())
                .AddTo(this);
        }

        public void Init(float rangeMin, float rangeMax, float velocity)
        {
            playerRangeMin = rangeMin;
            playerRangeMax = rangeMax;
            playerVelocity = velocity;
        }

        private void OnEnable()
        {
            globalEvents.OnMovePlayer += ChangeDirection;
            UpdatePreviewHero();
        }

        private void ChangeDirection(int side)
        {
            currentDirection = side;
            PlayerAnim(side != 0);
        }

        private void MovePlayer()
        {
            
            var position = transform.position;
            var clampY = Mathf.Clamp(
                position.y + currentDirection * playerVelocity * Time.deltaTime,
                playerRangeMin, playerRangeMax);
            position = new Vector3(position.x, clampY, clampY);
            //position = new Vector3(position.x, clampY, position.z);
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
        
        private void PlayerAnim(bool play)
        {
            var newScale = new Vector3(startScale.x + animOffsetScale,
                startScale.y - animOffsetScale, startScale.z);
            
            //transform.DOScale(newScale, 1f).SetEase(Ease.InOutCirc).SetLoops(-1, LoopType.Yoyo);
            
            var scaleAnim = DOTween.To(
                () => startScale,
                scale => transform.localScale = scale,
                newScale, easeSpeed).SetEase(Ease.Linear).SetAutoKill(false).SetLoops(-1, LoopType.Yoyo);
            if (play)
                scaleAnim.Play();
            else
                scaleAnim.Pause();
        }
        
        private void OnDisable()
        {
            globalEvents.OnMovePlayer -= ChangeDirection;
        }
    }
}
