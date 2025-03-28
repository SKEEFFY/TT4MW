using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreCard : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _countUpgrades;
    [SerializeField] private TMP_Text _cost;

    public void InitShopCard(Sprite background, Sprite icon, int countUpgrades, int cost)
    {
        _background.sprite = background;
        _icon.sprite = icon;
        _countUpgrades.text = countUpgrades.ToString();
        _cost.text = cost.ToString();
    }

    public void SetLevel(int level)
    {
        _countUpgrades.text = level.ToString();
    }

    public void SetCost(int cost)
    {
        _cost.text = cost.ToString();
    }
}

