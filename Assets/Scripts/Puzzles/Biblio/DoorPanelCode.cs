using UnityEngine;

public class DoorPanelCode : Interactable
{
    [SerializeField]
    private GameObject codePanel;

    public override void Interact(PlayerController player)
    {
        codePanel.SetActive(true);
    }
}
