using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private GameAction _action;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        
        if (button != null)
        {
            button.onClick.AddListener(() => SignalBus.Fire(_action));
        }
        else
        {
            Debug.Log("No Button");
        }
    }
}
