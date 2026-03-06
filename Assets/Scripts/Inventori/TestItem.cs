using UnityEngine;

public class TestItem : MonoBehaviour
{
    public Sprite itemSprite;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            InventoryManager.instance.AddItem(itemSprite);
        }
    }
}
