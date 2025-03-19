using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _shopCardPrefab;  // Префаб карточки
    [SerializeField] private Transform _contentPanel;    // ScrollView Content
    [SerializeField] private Sprite _moneyIcon;          // Иконка денег
    [SerializeField] private Sprite _defaultBackground;  // Фон карточек

    private void Start()
    {
        LoadShop();
    }

    private void LoadShop()
    {
        CreateShopCard(GameData.Instance.FoodStoreLevel, 100);
        CreateShopCard(GameData.Instance.HardwareStoreLevel, 200);
        CreateShopCard(GameData.Instance.TravelStoreLevel, 300);
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