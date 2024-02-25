using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exoa.TutorialEngine
{
    public class TutorialPopup : Popup
    {
        public AnimatedButton nextBtn;
        public Button closeBtn;
        public Text contentText;
        public UnityEvent OnClickNext;
        public Transform bg;
        private RectTransform popupRt;

        public RectTransform PopupRt { get => popupRt; set => popupRt = value; }

#if TUTORIAL_ENGINE_LOCALIZATION
        [HideInInspector]
        public string tableName = "";
        private UnityEngine.Localization.LocalizedString localizedString;
#endif

        /// <summary>
        /// Initialize the popup
        /// </summary>
        public void Init()
        {
            popupRt = GetComponent<RectTransform>();

            nextBtn.onClick.RemoveAllListeners();
            nextBtn.onClick.AddListener(OnClickNextHandler);

#if TUTORIAL_ENGINE_LOCALIZATION
            localizedString = new UnityEngine.Localization.LocalizedString(tableName, "");
            localizedString.StringChanged += UpdateLocalization;
#endif
        }

        private void UpdateLocalization(string value)
        {
            contentText.text = value;
        }

        private void OnClickNextHandler()
        {
            OnClickNext.Invoke();
        }

        /// <summary>
        /// Set step details in the popup
        /// </summary>
        /// <param name="s"></param>
        public void SetStep(TutorialSession.TutorialStep s)
        {
#if TUTORIAL_ENGINE_LOCALIZATION
            if (string.IsNullOrEmpty(tableName) == false)
            {
                localizedString.SetReference(tableName, s.text);
                localizedString.RefreshString();
                contentText.text = localizedString.GetLocalizedString();
            }
            else
                contentText.text = s.text;
#else
            contentText.text = s.text;
#endif
            UpdateHGroup();
        }

        /// <summary>
        /// Calculate the popup's position regarding the object hightlighted
        /// </summary>
        /// <param name="newRect"></param>
        /// <returns></returns>
        public Vector2 CalculatePopupPosition(Rect newRect)
        {
            Vector2 maskPosition = newRect.position;
            float maskDistanceFromCenter = maskPosition.magnitude;

            float maskDiagonal = Mathf.Max(newRect.width, newRect.height);


            float popupDiagonal = Mathf.Sqrt(Mathf.Pow(popupRt.rect.width, 2) + Mathf.Pow(popupRt.rect.height, 2));
            Vector2 popupPostion = (maskPosition.magnitude - (maskDiagonal * .5f) - (popupDiagonal * .5f)) * maskPosition.normalized;

            RectTransform parent = popupRt.parent as RectTransform;
            float maxX = parent.rect.width * .5f - popupRt.rect.width * .5f;
            float maxY = parent.rect.height * .5f - popupRt.rect.height * .5f;

            popupPostion.x = Mathf.Clamp(popupPostion.x, -maxX, maxX);
            popupPostion.y = Mathf.Clamp(popupPostion.y, -maxY, maxY);

            return popupPostion;
        }

        private void UpdateHGroup()
        {
            ContentSizeFitter csf = bg.GetComponent<ContentSizeFitter>();
            csf.enabled = false;
            csf.SetLayoutVertical();
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)bg);
            csf.enabled = true;
        }
    }
}
