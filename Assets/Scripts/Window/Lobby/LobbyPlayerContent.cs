using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Window.Lobby
{
    public partial class LobbyManager
    {
        [System.Serializable]
        public class LobbyPlayerContent
        {
            [Header("Position")]
            [SerializeField] private PositionOnTheScreen _positionOnScreen;
            [SerializeField] public Transform _transform;

            [Header("UI")]
            [SerializeField] private Button _heroButton;
            [SerializeField] private Image _heroImage;
            [SerializeField] private Text _heroText;
        
            public bool used { set; get; } // is used?
            public int heroId { set; get; }

            private bool _select;
            public bool select 
            { 
                set
                {
                    _select = value;
                    if(_select)
                        SetColor(new Color32(5,255,5,100));
                    else
                        SetColor(new Color32(255,255,255,100));
                }
                get
                {
                    return _select;
                } 
            }

            private event Action _onClick;

            public void Init()
            {
                _heroButton.onClick.RemoveAllListeners();
                _heroButton.onClick.AddListener(TouchButton);
                _onClick = null;
                used = false;
                select = false;
                heroId = 0;
                SetActive(true);
            }

            public void SetPlayer(int heroId)
            {
                used = true;
                this.heroId = heroId;
                SetSprite(GameManager.GetPlayerPrefab(heroId).Icon);
            }

            public void SetActiveScore(bool value)
            {
                _heroText.gameObject.SetActive(value);
            }

            public void SetSprite(Sprite sprite)
            {
                _heroImage.sprite = sprite;
                _heroImage.color = new Color32(255, 255, 255, (sprite == null ? (byte)0 : (byte)255));
            }

            public void SetActive(bool value)
            {
                _transform.gameObject.SetActive(value);
            }

            public void SetColor(Color32 color)
            {
                _transform.GetComponent<Image>().color = color;
            }

            public void AddActionOnTouchButton(LobbyManager.Action action)
            {
                _onClick += action;
            }

            public void RemoveAllActionOnTouchButton()
            {
                _onClick = null;
            }

            public PositionOnTheScreen GetPositionOnTheScreen()
            {
                return _positionOnScreen;
            }

            private void TouchButton()
            {
                _onClick?.Invoke(this);
            }

            // -1 - default value
            public void SetScore(int score = -1)
            {
                if (score == -1)
                    _heroText.text = GameManager.GetPlayer(heroId).ToString();
                else
                    _heroText.text = score.ToString();

                SetActiveScore(true);
            }
        }
    }
}