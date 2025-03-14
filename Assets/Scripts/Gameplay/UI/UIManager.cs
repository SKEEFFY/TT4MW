using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")] [Header("Popups")] [SerializeField]
    private GameObject _settingsWindow;

    [SerializeField] private GameObject _shopWindow;

    [Header("Buttons")] [SerializeField] private GameObject _shopButton;
    [SerializeField] private GameObject _settingsButton;

    private void Awake()
    {
        if (_settingsWindow)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        ShowHideUIElements(_shopWindow);
        ShowHideUIElements(_settingsWindow);
    }

    public void ShowHideUIElements(GameObject element)
    {
        element.SetActive(!element.activeSelf);
    }
}
