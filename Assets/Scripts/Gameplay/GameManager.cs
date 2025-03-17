using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas _mainMenuButtons;
    public static GameManager Instance { get; private set; }

    private Dictionary<GameAction, Action> _gameActions;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            SignalBus.Subscribe<GameAction>(HandleGameActions);
            
            _gameActions = new Dictionary<GameAction, Action>
            {
                {GameAction.StartGame, StartGame},
                {GameAction.ExitGame, ExitGame}
            };
        }
        else
        {
            Destroy(gameObject);
        }
        
        Instantiate(_mainMenuButtons);
    }

    private void HandleGameActions(GameAction action)
    {
        if (_gameActions == null)
        {
            Debug.Log("_gameAction is NULL '_gameAction(GameManager Dictionary)");
            return;
        }
        if (_gameActions.TryGetValue(action, out var gameAction))
        {
            gameAction();
        }
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<GameAction>(HandleGameActions);
    }

    private void StartGame() => SceneManager.LoadScene("GameScene");
    private void ExitGame() => Debug.Log("Game exited");
}