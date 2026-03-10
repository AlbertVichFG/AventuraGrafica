using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Adventure/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description;
}