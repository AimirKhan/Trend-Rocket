using Events;
using Services;
using TMPro;
using UniRx;
using UnityEngine;

namespace View
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private bool isRecordScore;
        private TextMeshProUGUI scoreText;

        [SerializeField] private string score;
        [SerializeField] private string recordScore;

        private GlobalVariables globalVar;
        
        private void Awake()
        {
            globalVar = GlobalVariables.Instance;
            scoreText = GetComponent<TextMeshProUGUI>();
            
            globalVar.CurrentScore
                .Select(Mathf.RoundToInt)
                .Where(ctx => !isRecordScore)
                .Subscribe(ctx =>
                {
                    scoreText.text = ctx + "km";
                    score = ctx.ToString();
                })
                .AddTo(this);
            globalVar.RecordScore
                .Select(Mathf.RoundToInt)
                .Where(ctx => isRecordScore)
                .Subscribe(ctx =>
                {
                    recordScore = ctx.ToString();
                    scoreText.text = ctx + "km";
                })
                .AddTo(this);
            globalVar.GameState
                .Subscribe(SetRecord)
                .AddTo(this);
        }

        private void SetRecord(EGameState gameState)
        {
            if (globalVar.CurrentScore.Value >= globalVar.RecordScore.Value)
            {
                globalVar.RecordScore.Value = globalVar.CurrentScore.Value;
            }
        }
    }
}
