using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIItems : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    GameObject PrefabToInstantiate;

    [SerializeField] GameObject prefabUIDrag;
    [SerializeField] ItemIn item;

    [SerializeField]
    RectTransform UIDragElement;

    [SerializeField]
    RectTransform Canvas;

    [SerializeField] private Camera mainCamera;

    private Vector2 mOriginalLocalPointerPosition;
    private Vector3 mOriginalPanelLocalPosition;
    private Vector2 mOriginalPosition;

    // Thêm biến để điều chỉnh độ cao
    [Header("Prefab Spawn Settings")]
    [SerializeField] private float verticalOffset = 0f; // Độ cao điều chỉnh thêm
    private bool useRaycastHeight = true; // Cho phép sử dụng độ cao từ Raycast
    [SerializeField] private float raycastMaxDistance = 3.0f; // Khoảng cách Raycast tối đa

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mOriginalPosition = UIDragElement.localPosition;

        if (UIDragElement == null || Canvas == null)
        {
            Canvas = GameObject.Find("InventoryCanvas").GetComponent<RectTransform>();
            Debug.LogError("UIDragElement or Canvas is not assigned.");
        }
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned in the Inspector.");
            mainCamera = Camera.main;
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        mOriginalPanelLocalPosition = UIDragElement.localPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
          Canvas,
          data.position,
          data.pressEventCamera,
          out mOriginalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
          Canvas,
          data.position,
          data.pressEventCamera,
          out localPointerPosition))
        {
            Vector3 offsetToOriginal =
              localPointerPosition -
              mOriginalLocalPointerPosition;
            UIDragElement.localPosition =
              mOriginalPanelLocalPosition +
              offsetToOriginal;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(
          Coroutine_MoveUIElement(
            UIDragElement,
            mOriginalPosition,
            0.5f));

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned.");
            return;
        }

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, raycastMaxDistance))
        {
            Vector3 worldPoint = hit.point;
            CreateObject(worldPoint);
        }
    }

    public IEnumerator Coroutine_MoveUIElement(
    RectTransform r,
    Vector2 targetPosition,
    float duration = 0.1f)
    {
        float elapsedTime = 0;
        Vector2 startingPos = r.localPosition;
        while (elapsedTime < duration)
        {
            r.localPosition =
              Vector2.Lerp(
                startingPos,
                targetPosition,
                (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        r.localPosition = targetPosition;
    }

    public void CreateObject(Vector3 position)
    {
        if (PrefabToInstantiate == null)
        {
            Debug.Log("No prefab to instantiate");
            return;
        }

        // Nếu sử dụng độ cao từ Raycast
        if (useRaycastHeight)
        {
            RaycastHit hit;
            // Thực hiện Raycast từ vị trí hiện tại xuống
            if (Physics.Raycast(new Vector3(position.x, position.y + 10f, position.z),
                Vector3.down, out hit, raycastMaxDistance))
            {
                // Sử dụng điểm va chạm + offset bổ sung
                position.y = hit.point.y + verticalOffset;
            }
        }
        else
        {
            // Nếu không sử dụng Raycast, chỉ thêm offset
            position.y += verticalOffset;
        }

        GameObject obj = Instantiate(
            PrefabToInstantiate,
            position,
            Quaternion.identity);

        Invoke("DestroyThis", 0.2f);
    }

    void DestroyThis()
    {
        Destroy(prefabUIDrag);
        InventoryManager.Instance.RemoveItem(item);
    }
}
