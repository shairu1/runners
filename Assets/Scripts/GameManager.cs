using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;


    public static int CountGames { private set; get; }
    public static int CountPlayers { private set; get; }
    public static int SizeMap { private set; get; }

    [Header("Menu")]
    [SerializeField] private Sprite[] _numbers; // Числа от 0 до 9 (спрайты)

    [Header("Game")]
    [SerializeField] private HeroPrefab[] _heroPrefabs;
    [SerializeField] private List<Player> _players;
    [SerializeField] private Transform _mapTransform;
    [SerializeField] private int[] _places; // место (которое занял игрок) (по ид)


    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _players = new List<Player>();
        var _cameraMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
        _cameraMovement.Init();
        _cameraMovement.SetActive(false);

        MenuManager.SetStartValues();
        WindowController.SwitchWindow(WindowTransitionType.Menu);
    }

    public static HeroPrefab GetPlayerPrefab(int heroId)
    {
        for (int i = 0; i < instance._heroPrefabs.Length; i++)
        {
            if(instance._heroPrefabs[i].HeroId == heroId)
            {
                return instance._heroPrefabs[i];
            }
        }

        Debug.LogError($"PlayerPrefab(); heroId = \"{heroId}\" not found");
        return null;
    }

    public static HeroPrefab[] GetPlayerPrefabs()
    {
        return instance._heroPrefabs;
    }

    public static void AddPlayer(int heroId, PositionOnTheScreen screenPosition, int score = 0)
    {
        instance._players.Add(new Player(heroId, screenPosition, score));
    }

    public static Player[] GetPlayers()
    {
        return instance._players.ToArray();
    }

    public static Player GetPlayer(int heroId)
    {
        for (int i = 0; i < instance._players.Count; i++)
        {
            if(instance._players[i].HeroId == heroId)
            {
                return instance._players[i];
            }
        }

        Debug.LogError("GetPlayer(int heroId) heroId = \"" + heroId + "\" not found!");
        return null;
    }

    public static void SetGameParameters(int players, int games, int sizeMap)
    {
        CountPlayers = players;
        CountGames = games;
		SizeMap = sizeMap;
    }

    public static void CreateRace() // создание забега
    {
        CountGames--;
        RaceManager.CreateRace();
    }
}
