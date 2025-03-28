using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float _panSpeed = 0.01f;

    [Header("Camera movement limit")] [SerializeField]
    private Vector2 _minLimits = new Vector2(0f, -10f);

    [SerializeField] private Vector2 _maxLimits = new Vector2(10f, 10f);
    private GameControls _controls;
    private Vector2 _touchDelta;

    private void Awake()
    {
        _controls = new GameControls();
        _controls.Touch.PrimaryTouch.performed += ctx => _touchDelta = ctx.ReadValue<Vector2>();
        _controls.Touch.PrimaryTouch.canceled += ctx => _touchDelta = Vector2.zero;

        SignalBus.Subscribe<UIPopupState>(OnUIWindowStateChanged);
    }
    
    private void Update()
    {
        if (_touchDelta != Vector2.zero)
        {
            Vector3 move = new Vector3(_touchDelta.y, 0, -_touchDelta.x) * _panSpeed;
            Vector3 newPosition = transform.position + move;

            newPosition.x = Mathf.Clamp(newPosition.x, _minLimits.x, _maxLimits.x);
            newPosition.z = Mathf.Clamp(newPosition.z, _minLimits.y, _maxLimits.y);

            transform.position = newPosition;
        }
    }
    
    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();
    
    private void OnUIWindowStateChanged(UIPopupState state)
    {
        Debug.Log("Hui");
        if (state.isOpen)
        {
            _controls.Disable();
        }
        else
        {
            _controls.Enable();
        }
    }
}