using TMPro;
using UnityEngine;

public sealed class StatisticManager : GameClass
{
    [SerializeField] private TMP_Text totalScore;
    [SerializeField] private TMP_Text earnedScore;

    private string totalScoreFormat;
    private string earnedScoreFormat;

    protected override void AtStart()
    {
        totalScoreFormat = totalScore.text;
        earnedScoreFormat = earnedScore.text;
    }

    protected override void AtUpdate()
    {
        totalScore.text = string.Format(totalScoreFormat, GameManager.GameDataManager.GetTotalScore());
        earnedScore.text = string.Format(earnedScoreFormat, GameManager.GameDataManager.GetEarnedScore());
    }
}