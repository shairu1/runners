using Race;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameManager : MonoBehaviour
    {
        private static UIGameManager uiGameManager;
    
        [SerializeField] private PlayerButton[] _playerButtons;

        private void Awake()
        {
            uiGameManager = this;
        }

        public static void UpdateButtons()
        {
            var players = GameManager.GetPlayers();

            for (int j = 0; j < uiGameManager._playerButtons.Length; j++)
            {
                uiGameManager._playerButtons[j].SetActive(false);
                uiGameManager._playerButtons[j].ClearActionsOnClick();
            }

            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < uiGameManager._playerButtons.Length; j++)
                {
                    if(players[i].ScreenPosition == uiGameManager._playerButtons[j].PositionOnTheScreen)
                    {
                        uiGameManager._playerButtons[j].SetActive(true);
                        uiGameManager._playerButtons[j].SetImage(GameManager.GetPlayerPrefab(players[i].HeroId).Icon);
                        uiGameManager._playerButtons[j].AddActionOnClick(RaceManager.GetPlayerControl(players[i].HeroId).Flip);
                        break;
                    }
                }
            }
        }

        public static void DisappearanceButton(PositionOnTheScreen positionOnTheScreen)
        {
            for (int j = 0; j < uiGameManager._playerButtons.Length; j++)
            {
                if(uiGameManager._playerButtons[j].PositionOnTheScreen == positionOnTheScreen)
                {
                    uiGameManager._playerButtons[j].SetActive(false);
                    return;
                }
            }
        }

        [System.Serializable]
        public class PlayerButton
        {
            public PositionOnTheScreen PositionOnTheScreen;
            public GameObject Object;
            public Button Button;
            public Image Image;

            public void SetActive(bool value) => Object.SetActive(value);
            public void SetImage(Sprite sprite) => Image.sprite = sprite;
            public void AddActionOnClick(UnityEngine.Events.UnityAction action) => Button.onClick.AddListener(action);
            public void ClearActionsOnClick() => Button.onClick.RemoveAllListeners();
        }
    }
}
