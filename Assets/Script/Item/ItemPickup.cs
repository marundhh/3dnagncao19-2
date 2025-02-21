using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemIn item;

    void OnMouseDown()
    {
        Pickup();
    }

    void Pickup()
    {
        Destroy(this.gameObject);
        InventoryManager.Instance.AddItem(item);
    }
}
