using System.Collections;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    private string _savePath;
    private float _autoSaveInterval = 5f;
    private bool _isDataChanged;
    
    public static GameDataManager Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _savePath = Application.persistentDataPath + "/GameData.json"; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(AutoSaveRoutine());
    }

    public void OnDataChanged()
    {
        _isDataChanged = true;
    }
    
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(GameData.Instance, true);
        File.WriteAllText(_savePath, json);
        Debug.Log("Game saved" + _savePath);
    }

    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_autoSaveInterval);
            if (_isDataChanged)
            {
                SaveGame();
                Debug.Log("Auto save");
            }
            Debug.Log("AutoSave: Data don't Changed");
        }
    }
    
    public void LoadGame()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(json);
            GameData.SetInstance(loadedData);
            Debug.Log("SaveLoaded");
        }
        else
        {
            {
                Debug.Log("Save file not found, started from the beginning");
            }
        }
    }
}
