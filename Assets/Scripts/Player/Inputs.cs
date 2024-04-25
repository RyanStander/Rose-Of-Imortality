using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class Inputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool fire;
		public bool reload;
		public bool interact;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		
		public void OnFire(InputValue value)
		{
			FireInput(value.isPressed);
		}
		
		public void OnReload(InputValue value)
		{
			ReloadInput(value.isPressed);
		}
		
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		public void FireInput(bool newFireState)
		{
			fire = newFireState;
		}
		
		public void ReloadInput(bool newReloadState)
		{
			reload = newReloadState;
		}

		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		public void SetInputs()
		{
			fire = false;
			reload = false;
			interact = false;
		}
	}
	
}
