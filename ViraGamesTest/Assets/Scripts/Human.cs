using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Human : MonoBehaviour
    {
        [SerializeField] private HumanAnimator animator;
        [SerializeField] private HumansHolder holder;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;

        public void Spawn(Material material)
        {
            meshRenderer.material = material;
            gameObject.SetActive(true);
            if (holder.IsMoving())
            {
                animator.StartRun();
            }
        }

        private void KillHuman()
        {
            gameObject.SetActive(false);
            holder.HideHuman();
        }

        public void StartDance()
        {
            animator.Dance();
        }

        public void Idle()
        {
            animator.Idle();
        }

        public void StartCast()
        {
            animator.Cast();
        }

        public void StartRun()
        {
            animator.StartRun();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Barrier"))
            {
                if (!holder.IsPlayer())
                {
                    return;
                }

                Barrier barrier = other.gameObject.GetComponent<Barrier>();
                int newValue = barrier.UseBarrier(holder.GetValue());
                holder.SetValue(newValue);
            }

            if (other.gameObject.CompareTag("Coin"))
            {
                other.gameObject.SetActive(false);
                holder.CollectCoin();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Trap"))
            {
                KillHuman();
            }
        }
    }

}