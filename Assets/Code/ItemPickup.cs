using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemIn item; // Đối tượng item để thêm vào Inventory
    private bool isPlayerNearby = false; // Kiểm tra xem người chơi có ở gần không

    void Pickup()
    {
        Destroy(this.gameObject); // Xóa đối tượng sau khi nhặt
        InventoryManager.Instance.Add(item); // Thêm item vào Inventory
    }

    void OnMouseDown()
    {
        Pickup();
    }

    void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem người chơi đã vào vùng gần đối tượng chưa
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Press E to pick up the item.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Kiểm tra xem người chơi đã rời khỏi vùng gần đối tượng chưa
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
