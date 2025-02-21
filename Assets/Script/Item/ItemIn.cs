using UnityEngine;


[CreateAssetMenu(fileName = "ItemIn", menuName = "Inventory/Item")]
public class ItemIn : ScriptableObject
{
    public enum ItemType
    {
        Consumables, Weapons
    }

    public int id;
    public string itemName;
    public int value;
    public Sprite image;
    public ItemType type;
}
