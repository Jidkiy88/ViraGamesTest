using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class HumanAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void StartRun()
        {
            animator.SetTrigger("Running");
        }

        public void Idle()
        {
            animator.SetTrigger("Idle");
        }

        public void Cast()
        {
            animator.SetTrigger("Casting");
        }

        public void Dance()
        {
            animator.SetTrigger("Dance");
        }
    }
}
