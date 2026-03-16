using TMPro;
using UnityEngine;

public class DoorCode : MonoBehaviour
{
    [SerializeField] private RectTransform numberContainer;
    [SerializeField] private float digitHeight;

    private int value = 0;

    public void Increase()
    {
        value++;

        if (value > 9)
            value = 0;

        UpdatePosition();
    }

    public void Decrease()
    {
        value--;

        if (value < 0)
            value = 9;

        UpdatePosition();
    }

    void UpdatePosition()
    {
        Vector2 pos = numberContainer.anchoredPosition;
        pos.y = value * digitHeight;
        numberContainer.anchoredPosition = pos;
    }

    public int GetValue()
    {
        return value;
    }
}
