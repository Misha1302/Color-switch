namespace GameDataBase.Ui
{
    using System;
    using UnityEngine;

    public sealed class MaxYPanel : GameClass
    {
        [SerializeField] private MaxYPanelObject maxYPanel;

        protected override void AtInit()
        {
            GameManager.GameDataManager.ReadyEvent.Subscribe(OnReady);
        }

        private void OnReady()
        {
            var maxY = GameManager.GameDataManager.MaxY;

            if (maxY <= 0) return;

            var obj = Instantiate(maxYPanel);
            obj.transform.position = new Vector3(0, maxY, 0);
            obj.SetText("Your max height", maxY);
        }
    }
}