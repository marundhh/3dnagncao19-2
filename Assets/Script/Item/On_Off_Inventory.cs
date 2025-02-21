using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.Cinemachine;
using System.Collections.Generic;

public class On_Off_Inventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] float fadeDuration = 0.3f;

    [SerializeField] ChangeWeapon changeWeapon;
    [SerializeField] PlayerControll playerControll;
    [SerializeField] GameObject freelookCam;

    [SerializeField] Transform contentParent; // Reference đến GameObject Content
    [SerializeField] GameObject dragItemPrefab; // Prefab UI item kéo thả

    bool inventoryVisible = false;

    void Start()
    {
        SetInventoryAlpha(0); // Khởi tạo alpha là 0
        inventoryMenu.SetActive(false); // Tắt inventory lúc đầu

        // Nếu chưa gán trong Inspector
        if (contentParent == null)
        {
            contentParent = GameObject.Find("Content").transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryVisible = !inventoryVisible;
            StartCoroutine(FadeInventory(inventoryVisible));

            Debug.Log("Đã nhấn Esc");
        }
    }

    IEnumerator FadeInventory(bool show)
    {
        float startAlpha = show ? 0 : 1;
        float targetAlpha = show ? 1 : 0;
        float time = 0;

        if (show) 
        {
            inventoryMenu.SetActive(true);

            changeWeapon.enabled = false;

            freelookCam.SetActive(false);

            playerControll.enabled = false;

            // Thêm các item từ InventoryManager vào Content khi mở inventory
            PopulateInventoryContent();
        }

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            SetInventoryAlpha(alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SetInventoryAlpha(targetAlpha);

        if (!show)
        {
            inventoryMenu.SetActive(false);

            changeWeapon.enabled = true;

            freelookCam.SetActive(true);

            playerControll.enabled = true;
        }
    }

    void PopulateInventoryContent()
    {
        // Xóa các item cũ trong Content trước khi thêm mới
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Lấy danh sách item từ InventoryManager
        List<ItemIn> inventoryItems = InventoryManager.Instance.items;

        // Tạo UI item cho từng item trong inventory
        foreach (ItemIn item in inventoryItems)
        {
            CreateDragItem(item);
        }
    }

    void CreateDragItem(ItemIn item)
    {
        if (dragItemPrefab == null || contentParent == null) return;

        // Tạo prefab drag item mới trong Content
        GameObject dragItem = Instantiate(dragItemPrefab, contentParent);

        // Cấu hình UI của drag item
        Image imageComponent = dragItem.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.sprite = item.image;
        }

        // Cấu hình script DragUIItems nếu cần
        DragUIItems dragScript = dragItem.GetComponent<DragUIItems>();
        if (dragScript != null)
        {
            // Có thể thiết lập thêm các thuộc tính cho drag item ở đây
        }
    }

    void SetInventoryAlpha(float alpha)
    {
        foreach (Graphic graphic in inventoryMenu.GetComponentsInChildren<Graphic>())
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }
}
