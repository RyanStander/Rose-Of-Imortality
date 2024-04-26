using UnityEngine;

namespace UI
{
    public class ToggleLockAndHideMouse : MonoBehaviour
    {
        [SerializeField] private bool isCursorLocked = true;
        [SerializeField] private bool allowToggle = true;

        private void SetCursorState()
        {
            Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isCursorLocked;
        }

        private void Start()
        {
            SetCursorState();
        }

        private void Update()
        {
            if (allowToggle)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isCursorLocked = !isCursorLocked;
                    SetCursorState();
                }
                
                if (Input.GetMouseButtonDown(0) && !isCursorLocked)
                {
                    isCursorLocked = !isCursorLocked;
                    SetCursorState();
                }
            }
        }
    }
}
