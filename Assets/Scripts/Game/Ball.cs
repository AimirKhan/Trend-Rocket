using System;
using DG.Tweening;
using Events;
using Services;
using UnityEngine;

namespace Game
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Ease ballEase = Ease.InQuart;
        [SerializeField] private float kickHeight = .7f;
        [SerializeField] private float easeSpeed = .7f;
        
        private void Start()
        {
            PlayTween();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var zombie = other.GetComponent<Zombie>();
            if (zombie != null)
            {
                GlobalVariables.Instance.GameState.Value = EGameState.Finished;
            }
        }

        private void PlayTween()
        {
            var localPosition = transform.localPosition;
            var newPos = new Vector3(localPosition.x,
                localPosition.y + kickHeight,
                localPosition.z);
            var anim = DOTween.To(
                () => transform.localPosition,
                pos => transform.localPosition = pos,
                newPos, easeSpeed).SetEase(ballEase).SetAutoKill(false).SetLoops(-1, LoopType.Yoyo);
            anim.Play();
        }
    }
}