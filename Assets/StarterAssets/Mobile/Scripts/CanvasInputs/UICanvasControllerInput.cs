using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [FormerlySerializedAs("starterAssetsInputs")] [Header("Output")]
        public Inputs Inputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            Inputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            Inputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            Inputs.JumpInput(virtualJumpState);
        }
        
        public void VirtualFireInput(bool virtualFireState)
        {
            Inputs.FireInput(virtualFireState);
        }
        
        public void VirtualReloadInput(bool virtualReloadState)
        {
            Inputs.ReloadInput(virtualReloadState);
        }
        
        public void VirtualInteractInput(bool virtualInteractState)
        {
            Inputs.InteractInput(virtualInteractState);
        }
        
        public void VirtualCheckArmInput(bool virtualCheckArmState)
        {
            Inputs.CheckArmInput(virtualCheckArmState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            Inputs.SprintInput(virtualSprintState);
        }
        
    }

}
