using UnityEngine;
using UnityEngine.UI;

namespace FarmAdventure
{
    public class Tooltip : MonoBehaviour
    {
        public GameObject TooltipPanel;
        public Text TooltipText;

        public void ShowTooltip()
        {
            TooltipPanel.SetActive(true);
        }

        public void HideTooltip()
        {
            TooltipPanel.SetActive(false);
        }

        public void SetText(string text)
        {
            TooltipText.text = text;
        }

        public void ResizeToFitText()
        {
            var PanelRect = TooltipPanel.GetComponent<RectTransform>();
            var LayoutGroup = TooltipPanel.GetComponent<HorizontalLayoutGroup>();
            var horizontalPadding = LayoutGroup.padding.horizontal;
            var verticalPadding = LayoutGroup.padding.vertical;
            PanelRect.sizeDelta = new Vector2(TooltipText.preferredWidth + horizontalPadding, TooltipText.preferredHeight + verticalPadding);
        }
    }
}
