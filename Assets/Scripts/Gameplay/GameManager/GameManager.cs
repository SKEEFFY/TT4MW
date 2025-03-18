using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    
    [SerializeField] private Canvas _mainMenuButtons;

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
                { GameAction.StartGame, StartGame },
                { GameAction.ExitGame, ExitGame }
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

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        GameDataManager.Instance.LoadGame(); 
    }

    private void ExitGame()
    {
        GameDataManager.Instance.SaveGame(); 
        Debug.Log("Game exited");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            GameDataManager.Instance.SaveGame(); 
    }

    private void OnApplicationQuit()
    {
        ExitGame();
    }
}