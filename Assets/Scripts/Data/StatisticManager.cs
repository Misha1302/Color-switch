using TMPro;
using UnityEngine;

namespace Data
{
    public sealed class StatisticManager : GameClass
    {
        [SerializeField] private TMP_Text earnedScore;
        [SerializeField] private string textFormat;


        protected override void AtStart()
        {
            AtUpdate();
        }

        protected override void AtUpdate()
        {
            earnedScore.text = string.Format(textFormat, GameManager.GameDataManager.GetEarnedScore());
        }
    }
}