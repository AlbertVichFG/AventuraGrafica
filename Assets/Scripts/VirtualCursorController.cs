using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class VirtualCursorController : MonoBehaviour
{
    private enum InputMode { Mouse, Gamepad }
    private enum CursorType { Walk, Talk, Interact, ChangeZone }

    [Header("UI")]
    [SerializeField] 
    private RectTransform cursorRect;
    [SerializeField] 
    private Image cursorImage;
    [SerializeField] 
    private Canvas canvas;

    [Header("Cursor Sprites")]
    [SerializeField] 
    private Sprite walkSprite;
    [SerializeField] 
    private Sprite talkSprite;
    [SerializeField] 
    private Sprite interactSprite;
    [SerializeField] 
    private Sprite changeZoneSprite;

    [Header("Gamepad")]
    [SerializeField] 
    private float gamepadSpeed = 800f;
    [SerializeField] 
    private float stickDeadZone = 0.15f;

    [Header("Raycast")]
    [SerializeField] 
    private LayerMask interactableLayer;
    [SerializeField] 
    private LayerMask walkZoneLayer;

    InputMode currentMode = InputMode.Mouse;
    Vector2 cursorPosition;
    private Camera mainCamera;
    private Mouse virtualMouse;

    void Start()
    {
        mainCamera = Camera.main;
        cursorPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        UpdateCursorVisual();
      //  Debug.Log("VirtualCursorController started");

        if (virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
            InputSystem.EnableDevice(virtualMouse);
         //   Debug.Log("Virtual mouse created: " + virtualMouse);
        }

        Cursor.visible = false;
    }

    void Update()
    {
        DetectInputMode();
        UpdateCursorPosition();
        UpdateCursorVisual();
        UpdateCursorType();
        UpdateVirtualMouse();
        SendGamepadClick();
        DebugUIRaycast();
    }

    void DetectInputMode()
    {
        if (Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
        {
            currentMode = InputMode.Mouse;
        }

        if (Gamepad.current != null &&
            Gamepad.current.leftStick.ReadValue().sqrMagnitude > stickDeadZone * stickDeadZone)
        {
            currentMode = InputMode.Gamepad;
        }
    }

    public void SetActiveCamera(Camera cam)
    {
        mainCamera = cam;
    }

    void UpdateCursorPosition()
    {
        if (currentMode == InputMode.Gamepad && Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            cursorPosition += stick * gamepadSpeed * Time.unscaledDeltaTime;
        }
        else if (currentMode == InputMode.Mouse && Mouse.current != null)
        {
            cursorPosition = Mouse.current.position.ReadValue();
        }

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, Screen.width);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, Screen.height);
    }

    void UpdateCursorVisual()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
            canvas.transform as RectTransform,
            cursorPosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out Vector2 localPoint
        );

        cursorRect.localPosition = localPoint;
    }

    void UpdateVirtualMouse()
    {
        if (virtualMouse == null) 
        {
            return;
        }

        virtualMouse.WarpCursorPosition(cursorPosition);
        InputState.Change(virtualMouse.position, cursorPosition);
    }

    void SendGamepadClick()
    {
        if (virtualMouse == null || Gamepad.current == null) 
        {
            return;
        }

        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("GAMEPAD CLICK DOWN");
            InputState.Change(virtualMouse.leftButton, 1);
        }

        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("GAMEPAD CLICK UP");
            InputState.Change(virtualMouse.leftButton, 0);
        }
    }

    private void UpdateCursorType()
    {
        Ray ray = mainCamera.ScreenPointToRay(cursorPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer))
        {
            switch (hit.collider.tag)
            {
                case "Talk":
                    SetCursor(CursorType.Talk);
                    return;

                case "Interact":
                    SetCursor(CursorType.Interact);
                    return;

                case "ChangeZone":
                    SetCursor(CursorType.ChangeZone);
                    return;
            }
        }

        if (Physics.Raycast(ray, out hit, 100f, walkZoneLayer))
        {
            SetCursor(CursorType.Walk);
            {
                return;
            }
        }

        SetCursor(CursorType.Walk);
    }

    private void SetCursor(CursorType type)
    {
        switch (type)
        {
            case CursorType.Walk:
                cursorImage.sprite = walkSprite;
                break;

            case CursorType.Talk:
                cursorImage.sprite = talkSprite;
                break;

            case CursorType.Interact:
                cursorImage.sprite = interactSprite;
                break;

            case CursorType.ChangeZone:
                cursorImage.sprite = changeZoneSprite;
                break;
        }
    }

    void DebugUIRaycast()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = cursorPosition;
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
           // Debug.Log("UI HIT: " + results[0].gameObject.name);
        }
    }

    public Vector2 GetCursorScreenPosition()
    {
        return cursorPosition;
    }
}