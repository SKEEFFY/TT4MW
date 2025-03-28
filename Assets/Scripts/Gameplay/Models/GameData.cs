using UnityEngine;

public class GameData
{
    private static GameData _instance;

    private int _money;
    private int _foodStoreLevel;
    private int _hardwareStoreLevel;
    private int _travelStoreLevel;
    
    [SerializeField] private int _foodStoreCost = 500;
    [SerializeField] private int _hardwareStoreCost = 500;
    [SerializeField] private int _travelStoreCost = 500;

    public int Money
    {
        get => _money;
        set => _money = value;
    }

    public int FoodStoreLevel
    {
        get => _foodStoreLevel;
        set => _foodStoreLevel = value;
    }

    public int HardwareStoreLevel
    {
        get => _hardwareStoreLevel;
        set => _hardwareStoreLevel = value;
    }

    public int TravelStoreLevel
    {
        get => _travelStoreLevel;
        set => _travelStoreLevel = value;
    }

    public int FoodStoreCost
    {
        get => _foodStoreCost;
        set => _foodStoreCost = value;
    }

    public int HardwareStoreCost
    {
        get => _hardwareStoreCost;
        set => _hardwareStoreCost = value;
    }

    public int TravelStoreCost
    {
        get => _travelStoreCost;
        set => _travelStoreCost = value;
    }

    private GameData()
    {
        _money = 500;
        _foodStoreLevel = 0;
        _hardwareStoreLevel = 0;
        _travelStoreLevel = 0;
    }

    public static GameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameData();
            }
            return _instance;
        }
    }

    public static void SetInstance(GameData loadedData)
    {
        _instance = loadedData;
        Debug.Log($"{loadedData.Money}" +
                  $"{loadedData.FoodStoreLevel} " +
                  $"{loadedData.HardwareStoreLevel} " +
                  $"{loadedData.TravelStoreLevel} " +
                  $"{loadedData.FoodStoreCost} " +
                  $"{loadedData.HardwareStoreCost} " +
                  $"{loadedData.TravelStoreCost}");
    }
}