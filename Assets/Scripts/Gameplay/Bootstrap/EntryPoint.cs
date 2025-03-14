using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private GameObject _gameManagerPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        InitializeGameManager();
        InitializeUI();
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void InitializeUI()
    {
        if (_uIManager != null)
        {
            _uIManager.Initialize();
        }
        else
        {
            Debug.LogWarning("UIManager not set in bootstrap");
        }
    }

    private void InitializeGameManager()
    {
        if (FindObjectsOfType<GameManager>() == null)
        {
            Instantiate(_gameManagerPrefab);
        }
    }
}
