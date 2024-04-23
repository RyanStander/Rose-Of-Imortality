using UnityEngine;

namespace AnimatorScripts
{
    /// <summary>
    /// Sets a boolean through a behavior in the animator
    /// </summary>
    public class SetAnimatorBool : StateMachineBehaviour
    {
        [SerializeField] private string boolName;
        [SerializeField] private bool value;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(boolName, value);
        }
    }
}
