using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            InputManager[] managers = FindObjectsOfType(typeof(InputManager)) as InputManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("InputManager not present on the scene. Creating a new one.");
                InputManager manager = new GameObject("Input Manager").AddComponent<InputManager>();
                _instance = manager;
                return _instance;
            }
            else
            {
                return managers[0];
            }
        }
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Debug.LogError("You can only use one InputManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static InputManager _instance = null;
    #endregion

    private InputMap inputMap;

    private bool fingerDown = false;

    public delegate void TouchPressEvent(Vector2 position);
    public event TouchPressEvent OnTouchPressed;

    public delegate void TouchEvent(Vector2 position);
    public event TouchEvent OnTouch;

    public delegate void TouchReleaseEvent(Vector2 position);
    public event TouchReleaseEvent OnTouchReleased;

    private void Awake() {
        inputMap = new InputMap();
    }

    private void OnEnable() {
        inputMap.Enable();

        inputMap.Touch.TouchPress.started += ctx => TouchPressed();
        inputMap.Touch.TouchPress.canceled += ctx => TouchReleased();
    }

    private void OnDisable() {
        inputMap.Disable();
    }

    private void FixedUpdate() {
        if(fingerDown) {
            Touch();
        }
    }

    private void Touch() {
        //Debug.Log("Finger at " + inputMap.Touch.TouchPosition.ReadValue<Vector2>());

        if(OnTouch != null) {
            OnTouch(inputMap.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }

    private void TouchPressed()
    {
        //Debug.Log("Touch press at " + inputMap.Touch.TouchPosition.ReadValue<Vector2>());
        
        fingerDown = true;

        if(OnTouchPressed != null) {
            OnTouchPressed(inputMap.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }

    private void TouchReleased()
    {
        //Debug.Log("Touch release at " + inputMap.Touch.TouchPosition.ReadValue<Vector2>());
        
        fingerDown = false;
        
        if(OnTouchReleased != null) {
            OnTouchReleased(inputMap.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }
}
