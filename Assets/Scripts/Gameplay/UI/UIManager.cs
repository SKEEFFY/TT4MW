using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")] 
    [Header("Popups")] 
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _shopWindow;

    [Header("Buttons")] 
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _settingsButton;

    [Header("Rewards")]
    [SerializeField] private GameObject _chipsRewards;

    private GameObject _currentOpenWindow;
    private Button[] _buttonsList;

    private Dictionary<GameAction, Action> _uiActions;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            SignalBus.Subscribe<GameAction>(HandleUIActions);

            _uiActions = new Dictionary<GameAction, Action>
            {
                {GameAction.OpenSettings, OpenSetting},
                {GameAction.OpenShop, OpenShop},
                {GameAction.CloseButton, CloseWindow}
            };
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    private void Start()
    {
        Initialize();
        _buttonsList = new Button[]
        {
            _shopButton,
            _settingsButton
        };
    }
    private void CloseWindow()
    {
        ShowHidePopups(_currentOpenWindow);
    }

    private void OpenShop()
    {
        ShowHidePopups(_shopWindow);
        _currentOpenWindow = _shopWindow;
    }

    private void OpenSetting()
    {
        ShowHidePopups(_settingsWindow);
        _currentOpenWindow = _settingsWindow;
    }
    
    private void HandleUIActions(GameAction action)
    {
        if (_uiActions == null)
        {
            Debug.Log("_uiActions is NULL '_uiManager(UIManager Dictionary)'");
            return;
        }

        if (_uiActions.TryGetValue(action, out Action uiAction))
        {
            uiAction();
        }
    }

    public void Initialize()
    {
        _settingsWindow.SetActive(false);
        _shopWindow.SetActive(false);
        _shopButton.transform.parent?.parent?.gameObject.SetActive(true);
        _shopButton.gameObject.SetActive(true);
        _settingsButton.gameObject.SetActive(true);
        _chipsRewards.transform.parent?.gameObject.SetActive(true);
        _chipsRewards.gameObject.SetActive(true);
    }

    public void ShowHidePopups(GameObject element)
    {
        element.SetActive(!element.activeSelf);
        foreach (var button in _buttonsList)
        {
            button.gameObject.SetActive(!element.activeSelf);
        }
        
        SignalBus.Fire(new UIPopupState(element.activeSelf));
    }
}