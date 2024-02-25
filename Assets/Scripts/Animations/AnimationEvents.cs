using UnityEngine;
using UnityEngine.Events;

namespace Animations
{
    public class AnimationEvents : MonoBehaviour
    {
        public static event UnityAction endGameToLobby;
        public static event UnityAction endLobbyToGame;

        public void GameToLobbyEndAnimation() =>
            endGameToLobby?.Invoke();
    
        public void LobbyToGameEndAnimation() =>
            endLobbyToGame?.Invoke();
    }
}
