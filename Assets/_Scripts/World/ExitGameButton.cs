using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        Admin.Global.EventSystem.GetCommandByName<Event<GameStateSystem.State>>("game_state_sys", "set_game_state").Invoke(GameStateSystem.State.MainMenu);
        Screen.fullScreen = !Screen.fullScreen;
    }
}
