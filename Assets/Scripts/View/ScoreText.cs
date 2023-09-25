using TMPro;
using UniRx;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        GlobalVariables.Instance.CurrentScore
            .Select(ctx => ctx.ToString())
            .Subscribe(ctx => scoreText.text = ctx + "km")
            .AddTo(this);
    }
}
