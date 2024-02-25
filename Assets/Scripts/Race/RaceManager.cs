using System.Collections.Generic;
using Animations;
using CameraLogic;
using Level;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Window;

namespace Race
{
    public partial class RaceManager : MonoBehaviour
    {
        private static RaceManager instance;

        [Header("UI")]
        [SerializeField] private Text _timerText;

        [Header("Player controls")]
        [SerializeField] private List<PlayerControl> _playerControls; // in play

        private void Awake() => instance = this;

        public static void CreateRace()
        {
            LevelManager.CreateLevel();     // create level
            CreatePlayers();                // create players
            UIGameManager.UpdateButtons();  // update buttons
            InitializeCamera();             // init camera

            AnimationEvents.endLobbyToGame += EndAnimationLobbyToGame;
            WindowController.SwitchWindow(WindowTransitionType.LobbyToGame);
        }

        private static void CreatePlayers()
        {
            instance._playerControls.Clear();

            Player.Player[] players = GameManager.GetPlayers();
            Vector3[] positions = LevelManager.PlayerPositionsOnLevel;
            Vector3 position;

            for (int i = 0; i < players.Length; i++)
            {
                var go = Instantiate(
                    GameManager.GetPlayerPrefab(players[i].HeroId).Prefab,
                    Vector3.zero,
                    Quaternion.identity
                );

                position = positions[i];
                position.z = -1;
                go.transform.position = position;

                PlayerControl pc = go.GetComponent<PlayerControl>();
                pc.Init(players[i].HeroId);
                pc.SetEnabled(false);
                instance._playerControls.Add(pc);
            }
        }

        private static void InitializeCamera()
        {
            var _cameraMovement = UnityEngine.Camera.main.gameObject.GetComponent<CameraMovement>();
            _cameraMovement.SetStartPosition();
            _cameraMovement.SetActive(true);
        }

        public static PlayerControl GetPlayerControl(int heroId)
        {
            foreach (var item in instance._playerControls)
            {
                if (item.HeroId == heroId)
                    return item;
            }

            Debug.LogError($"heroId = {heroId} not found");
            return null;
        }

        public static PlayerControl[] GetPlayerControls()
        {
            return instance._playerControls.ToArray();
        }

        // вызывается, когда анимация перехода из лобби в игру закончена
        private static void EndAnimationLobbyToGame()
        {
            var timer = instance.gameObject.AddComponent<RaceStartTimer>();
            timer.Init(instance._timerText, EndGameTimer);
            AnimationEvents.endLobbyToGame -= EndAnimationLobbyToGame;
        }

        // вызывается, когда заканчивается таймер начала игры
        private static void EndGameTimer()
        {
            foreach(var item in instance._playerControls)
            {
                item.SetEnabled(true);
            }
        }

        public static void PlayerFinished(int heroId)
        {
            GameManager.GetPlayer(heroId).Score += instance._playerControls.Count;
            RemovePlayerControl(heroId);
            CheckEndRace();
        }

        public static void PlayerLost(int heroId)
        {
            GameManager.GetPlayer(heroId).Score += GameManager.CountPlayers - instance._playerControls.Count + 1;
            RemovePlayerControl(heroId);
            CheckEndRace();
        }

        private static void RemovePlayerControl(int heroId)
        {
            for (int i = 0; i < instance._playerControls.Count; i++)
            {
                if (instance._playerControls[i].HeroId == heroId)
                { instance._playerControls.RemoveAt(i); break; }
            }
        }

        private static void CheckEndRace()
        {
            if (instance._playerControls.Count > 0) return; // race continues

            instance._playerControls.Clear();

            WindowController.SwitchWindow(WindowTransitionType.GameToLobby);
        }
    }
}
