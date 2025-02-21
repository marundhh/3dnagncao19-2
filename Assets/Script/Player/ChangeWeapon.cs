using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject[] weapons; // Các vũ khí được thêm vào array
    private int currentWeaponIndex = 0; // Chỉ số vũ khí hiện tại

    public InputActionReference scrollAction; // Tham chiếu đến hành động cuộn chuột
    public Animator animator; // Animator Controller của nhân vật
    public string switchTriggerName = "Switch"; // Tên của Trigger trong Animator

    private void OnEnable()
    {
        scrollAction.action.Enable();
        scrollAction.action.performed += OnScrollPerformed;
    }

    private void OnDisable()
    {
        scrollAction.action.performed -= OnScrollPerformed;
        scrollAction.action.Disable();
    }

    private void OnScrollPerformed(InputAction.CallbackContext context)
    {
        Vector2 scrollValue = context.ReadValue<Vector2>();
        if (scrollValue.y > 0)
        {
            SwitchWeapon(1); // Cuộn lên
        }
        else if (scrollValue.y < 0)
        {
            SwitchWeapon(-1); // Cuộn xuống
        }
    }

    private void SwitchWeapon(int direction)
    {
        // Ẩn vũ khí hiện tại
        weapons[currentWeaponIndex].SetActive(false);

        // Cập nhật chỉ số vũ khí
        currentWeaponIndex += direction;

        // Đảm bảo chỉ số không vượt quá giới hạn
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        else if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        // Hiển thị vũ khí mới
        weapons[currentWeaponIndex].SetActive(true);

        // Kích hoạt trigger đổi vũ khí trong Animator
        if (animator != null)
        {
            animator.SetTrigger(switchTriggerName);
        }

        Debug.Log($"Switched to weapon: {weapons[currentWeaponIndex].name}");
    }
}
