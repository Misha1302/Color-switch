namespace GameDataBase.Ui
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public sealed class MaxYPanels : GameClass
    {
        [SerializeField] private MaxYPanelObject maxYPanel;

        protected override void AtInit()
        {
            GameManager.GameDataManager.ReadyEvent.Subscribe(OnReady);
        }

        private void SetAllUsersPanels(List<PlayerDto> list)
        {
            var l = list.OrderBy(x => x.maxY).ToList();
            var pos = 0f;

            foreach (var player in l)
                if (player.maxY - pos > 10)
                {
                    InstantiatePanel(player.login, player.maxY);
                    pos = player.maxY;
                }
        }

        private void OnReady()
        {
            GameManager.FirebaseManager.GetAllUsers<PlayerDto>(SetAllUsersPanels);

            var maxY = GameManager.GameDataManager.MaxY;

            if (maxY <= 0) return;

            InstantiatePanel("Your max", maxY);
        }

        private void InstantiatePanel(string prefixText, float maxY)
        {
            var obj = Instantiate(maxYPanel);
            obj.transform.position = new Vector3(0, maxY, 0);
            obj.SetText(prefixText, maxY);
        }
    }
}