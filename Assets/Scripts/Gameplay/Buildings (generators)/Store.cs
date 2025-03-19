using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _shopCardPrefab;  // Префаб карточки
    [SerializeField] private Transform _contentPanel;    // ScrollView Content
    [SerializeField] private Sprite _moneyIcon;          // Иконка денег
    [SerializeField] private Sprite _defaultBackground;  // Фон карточек

    private void OnEnable()
    {
        SignalBus.Subscribe<GameLoadedSignal>(OnGameLoaded);
    }


    private void Start()
    {
        LoadShop();
    }

    private void OnDisable()
    {
        SignalBus.Unsubscribe<GameLoadedSignal>(OnGameLoaded);
    }

    private void OnGameLoaded(GameLoadedSignal signal)
    {
        LoadShop();
    }

    private void LoadShop()
    {
        CreateShopCard(GameData.Instance.FoodStoreLevel, GameData.Instance.FoodStoreCost);
        CreateShopCard(GameData.Instance.HardwareStoreLevel, GameData.Instance.HardwareStoreCost);
        CreateShopCard(GameData.Instance.TravelStoreLevel, GameData.Instance.TravelStoreCost);
    }

    private void CreateShopCard(int upgrades, int cost)
    {
        GameObject newCard = Instantiate(_shopCardPrefab, _contentPanel);
        StoreCard storeCard = newCard.GetComponent<StoreCard>();

        if (storeCard != null)
        {
            storeCard.InitShopCard(_defaultBackground, _moneyIcon, upgrades, cost);
        }
    }
}