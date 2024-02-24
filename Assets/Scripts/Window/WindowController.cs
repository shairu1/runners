using UnityEngine;


public class WindowController : MonoBehaviour
{
    private static WindowController instance;

    [Header("Menu")]
    [SerializeField] private Animator _windowAnimator;

    private void Awake()
    {
        instance = this;
    }

    public static void SwitchWindow(WindowTransitionType animation)
    {
        switch (animation)
        {
            case WindowTransitionType.Menu:
                instance._windowAnimator.SetTrigger("StartGame");
                break;
            case WindowTransitionType.MenuToLobby:
                instance._windowAnimator.SetTrigger("StartGameToLobby");
                LobbyManager.CreateLobby();
                break;
            case WindowTransitionType.LobbyToGame:
                instance._windowAnimator.SetTrigger("LobbyToGame");
                break;
            case WindowTransitionType.GameToLobby:
                instance._windowAnimator.SetTrigger("GameToLobby");
                LobbyManager.StartUpdateLobby();
                break;
        }
    }
}
