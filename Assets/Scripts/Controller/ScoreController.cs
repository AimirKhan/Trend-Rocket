using System;
using Events;
using Services;
using UniRx;
using UnityEngine;

namespace Controller
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private float scoreSpeed = .5f;
        [SerializeField] private EGameState gameState;

        private void Awake()
        {
            GlobalVariables.Instance.GameState
                .Subscribe(ctx => gameState = ctx)
                .AddTo(this);
            Observable.EveryUpdate()
                .Where(ctx => gameState == EGameState.Started || gameState == EGameState.Continued)
                .Subscribe(ctx => ChangeScore())
                .AddTo(this);
        }
        
        private void ChangeScore()
        {
            GlobalVariables.Instance.CurrentScore.Value += scoreSpeed * Time.deltaTime;
        }
    }
}
