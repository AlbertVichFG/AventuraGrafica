using UnityEngine;

public class CodePuzzle : MonoBehaviour
{
    [SerializeField] 
    private DoorCode digit1;
    [SerializeField] 
    private DoorCode digit2;
    [SerializeField] 
    private DoorCode digit3;
    [SerializeField] 
    private DoorCode digit4;

    [SerializeField] 
    private string correctCode = "1234";

    [SerializeField] 
    private DoorBiblio door;

    public void CheckCode()
    {
        string enteredCode =
            digit1.GetValue().ToString() +
            digit2.GetValue().ToString() +
            digit3.GetValue().ToString() +
            digit4.GetValue().ToString();

        if (enteredCode == correctCode)
        {
            door.OpenDoor();
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);  
        }
    }
}
