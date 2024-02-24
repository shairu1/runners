using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class LobbyManager : MonoBehaviour
{
    private static LobbyManager instance;
    public delegate void Action(LobbyPlayerContent a);

    [Header("UI")]
    [SerializeField] private GameObject _newGameButton;
    [SerializeField] private Text[] _gameCountText; // тексты с остатком игр
    [SerializeField] private LobbyPlayerContent[] _playerContents; // контенты для игроков

    [Header("Lobby hero")]
    [SerializeField] private GameObject _prefabLobbyHero; // prefab
    [SerializeField] private Transform _parentLobbyHero; // parent

    private LobbyHeroController[] _lobbyHeroControllers;


    private void Awake() => instance = this;
    
    public static void UpdateGameСountText()
    {
        foreach (var item in instance._gameCountText)
            item.text = "Осталось игр: " + GameManager.CountGames.ToString();
    }

    public static void SetActiveGameСountText(bool value)
    {
        foreach(var item in instance._gameCountText)
            item.gameObject.SetActive(value);
    }

    public static void SetActiveNewGameButton(bool value)
    {
        instance._newGameButton.SetActive(value);
    }

    public static void CreateLobby()
    {
        if (GameManager.GetPlayers().Length != 0) return; // players have already been created

        SetActiveGameСountText(false);
        SetActiveNewGameButton(false);

        foreach (var item in instance._playerContents)
        {
            item.Init();
            item.SetActiveScore(false);
            item.SetSprite(null);
        }
        
        HeroPrefab[] players = GameManager.GetPlayerPrefabs();
        instance._lobbyHeroControllers = new LobbyHeroController[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            GameObject go = Instantiate(instance._prefabLobbyHero, Vector3.zero, Quaternion.identity, instance._parentLobbyHero);
            float sizeX = go.GetComponent<RectTransform>().sizeDelta.x;
            go.transform.localPosition = new Vector3(-((players.Length * sizeX)/2-sizeX/2) + i*sizeX, 0, 0);
            instance._lobbyHeroControllers[i] = go.AddComponent<LobbyHeroController>();
            instance._lobbyHeroControllers[i].Init(players[i].HeroId);
            go.GetComponent<Image>().sprite = players[i].Icon;
        }

        AnimationEvents.endGameToLobby -= UpdateLobby;
        AnimationEvents.endGameToLobby += UpdateLobby;
    }

    private void SetPlayerInContent(LobbyPlayerContent content, int heroId)
    {
        GameManager.AddPlayer(heroId, content.GetPositionOnTheScreen());
        content.SetPlayer(heroId);

        if(GameManager.GetPlayers().Length != GameManager.CountPlayers)
            return; // create more players

        // max players

        Utilities.RemoveAllChildObjects(_parentLobbyHero); // delete all controllers
        _lobbyHeroControllers = null;

        foreach (var item in _playerContents)
        {
            if (!item.used)
                item.SetActive(false);
            else
                item.SetScore(0);

            item.AddActionOnTouchButton(OnTouchPlayerButton);
        }

        UpdateGameСountText();
        SetActiveGameСountText(true);
    }

    public void OnTouchPlayerButton(LobbyPlayerContent content)
    {
        if(GameManager.CountGames == 0 || RaceManager.GetPlayerControls().Length != 0) return;

        content.select = true;
        
        for (int i = 0; i < _playerContents.Length; i++)
            if(!_playerContents[i].select && _playerContents[i].used) return;

        // if all players have clicked

        GameManager.CreateRace();
    }

    // начало обновления лобби после игры
    public static void StartUpdateLobby()
    {
        for (int i = 0; i < instance._playerContents.Length; i++)
        {
            instance._playerContents[i].select = false;
        }

        UpdateGameСountText();

        if (GameManager.CountGames == 0)
            SetFinalPlaces();
    }


    // обновление лобби после анимации
    public static void UpdateLobby()
    {
        InternalAdsManagement.FullscreenAds();
        //instance.StartCoroutine(instance.UpdatingLobby());
    }

    public static void UpdateScore()
    {
        foreach (var item in instance._playerContents)
            item.SetScore(); 
    }

    public static void SetFinalPlaces()
    {
        int[] players = new int[GameManager.GetPlayers().Length];
        for (int i = 0; i < players.Length; i++)
            players[i] = GameManager.GetPlayers()[i].HeroId;

        for (int i = 0; i < players.Length - 1; i++)
        {
            if (players[i] < players[i+1])
            {
                int temp = players[i];
                players[i] = players[i+1];
                players[i+1] = temp;
                i--;
            }
        }

        for (int i = 0; i < players.Length; i++)
        {
            foreach(var item in instance._playerContents)
            {
                if (players[i] == item.heroId)
                {
                    item.SetScore(i + 1);
                }
            }
        }
    }
}