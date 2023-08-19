namespace GameDataBase.Ui
{
    using TMPro;
    using UnityEngine;

    public sealed class MaxYPanelObject : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void SetText(string prefix, float maxY)
        {
            text.text = $"{prefix} - {maxY:#.##}";
        }
    }
}