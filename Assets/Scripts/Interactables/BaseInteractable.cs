using UnityEngine;

namespace Interactables
{
    public class BaseInteractable : MonoBehaviour
    {
        public virtual void Interact()
        {
            Debug.LogWarning("No override done");
        }

        public virtual void EnterRange()
        {
            Debug.LogWarning("No override done");
        }
    }
}
