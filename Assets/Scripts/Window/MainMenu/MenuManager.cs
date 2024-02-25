using UnityEngine;
using UnityEngine.UI;

namespace Window.MainMenu
{
	public class MenuManager : MonoBehaviour
	{
		private static MenuManager instance;

		[Header("Player count")]
		[SerializeField] private Color _activeColor;            // Color
		[SerializeField] private Color _inactiveColor;          // Color
		[SerializeField] private Image[] _playerCountBgImages;  // Image

		[Header("Game count")]
		[SerializeField] private Slider _gameCountSlider;       // Slider
		[SerializeField] private Text _gameCountText;           // Text

		[Header("Map size")]
		[SerializeField] private Slider _mapSizeSlider;         // Slider
		[SerializeField] private Text _mapSizeText;             // Text

		private int _countPlayers;
		private int _countGames;
		private int _mapSize;
		private bool _toLobby;


		private void Awake() => instance = this;
    
		private void Start() => SetStartValues();

		public static void SetStartValues()
		{
			instance._toLobby = false;

			// player
			instance.ChangePlayerCount(1);

			// games
			instance._gameCountSlider.value = 2;
			instance.ChangeGameCount();

			// map
			instance._mapSizeSlider.value = 1;
			instance.ChangeMapSize();
		}

		public void ChangePlayerCount(int count)
		{
			_countPlayers = count;

			for (int i = 0; i < _playerCountBgImages.Length; i++)
				_playerCountBgImages[i].color = _inactiveColor; 

			_playerCountBgImages[count-1].color = _activeColor;
		}

		public void ChangeGameCount()
		{
			_countGames = (int)_gameCountSlider.value;
			_gameCountText.text = _countGames.ToString();
		}
	
		public void ChangeMapSize()
		{
			_mapSize = (int)_mapSizeSlider.value;
			switch(_mapSize)
			{
				case 1:
					_mapSizeText.text = "Маленькая";
					break;
				case 2:
					_mapSizeText.text = "Средняя";
					break;
				case 3:
					_mapSizeText.text = "Большая";
					break;
				case 4:
					_mapSizeText.text = "Огромная";
					break;
			}
		}

		// button in ui
		public void ClickStartGame()
		{
			if(!_toLobby)
			{
				GameManager.SetGameParameters(_countPlayers, _countGames, _mapSize);
				WindowController.SwitchWindow(WindowTransitionType.MenuToLobby);  
				_toLobby = true;
			}
		}
	}
}