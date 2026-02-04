using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VirtualCursorController : MonoBehaviour
{

    private enum InputMode { Mouse, Gamepad }
    private enum CursorType { Walk, Talk, Interact, ChangeZone }

    [Header("UI")]
    [SerializeField]
    private RectTransform cursorRect; // punter UI
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

    void Start()
    {
        mainCamera = Camera.main;
        // Inicialitzem cursor  centre  pantalla
        cursorPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        UpdateCursorVisual();
    }

    void Update()
    {
        DetectInputMode();
        UpdateCursorPosition();
        UpdateCursorVisual();
        UpdateCursorType();
    }

    //detectar input type 
    void DetectInputMode()
    {
        // Mouse
        if (Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
        {
            currentMode = InputMode.Mouse;
        }

        // Gamepad
        if (Gamepad.current != null &&
            Gamepad.current.leftStick.ReadValue().sqrMagnitude > stickDeadZone * stickDeadZone)
        {
            currentMode = InputMode.Gamepad;
        }
    }


    //mov cursor

    void UpdateCursorPosition()
    {
        // Si hi ha moviment de joystick punter virtual
        if (currentMode == InputMode.Gamepad && Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            cursorPosition += stick * gamepadSpeed * Time.deltaTime;
        }
        else if(currentMode == InputMode.Mouse && Mouse.current != null)
        {
            // Si no, mouse
            cursorPosition = Mouse.current.position.ReadValue();
        }

        // Limitem el cursor dins la pantalla
        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, Screen.width);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, Screen.height);
    }


    //update UI
    void UpdateCursorVisual()
    {
        // Convertim coordenades de pantalla a Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            cursorPosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out Vector2 localPoint
        );

        cursorRect.localPosition = localPoint;
    }

    //canvi tipus cursor segons raycast
    private void UpdateCursorType()
    {
        Ray ray = mainCamera.ScreenPointToRay(cursorPosition);

        //Donar prioritat a objectes abans que terra
        //objectes interactuables
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

        // no objecte, comprovem terra
        if (Physics.Raycast(ray, out hit, 100f, walkZoneLayer))
        {
            SetCursor(CursorType.Walk);
            return;
        }

        //default
        SetCursor(CursorType.Walk);
    }

    //canvi sprite cursor
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





    //  POSICIÓ PER AL RAYCAST 

    public Vector2 GetCursorScreenPosition()
    {
        return cursorPosition;
    }
}
