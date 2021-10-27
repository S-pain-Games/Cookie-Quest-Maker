using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadGameStateButton : MonoBehaviour
{
    [SerializeField]
    private GameStateSystem.State m_TargetState;
    private Button b;

    private GameStateSystem gameStateSystem;

    private void Awake()
    {
        b = GetComponent<Button>();
        gameStateSystem = Admin.g_Instance.gameStateSystem;
    }

    private void OnEnable()
    {
        b.onClick.AddListener(LoadGameState);
    }

    private void OnDisable()
    {
        b.onClick.RemoveListener(LoadGameState);
    }

    private void LoadGameState()
    {
        gameStateSystem.SetState(m_TargetState);
    }
}
