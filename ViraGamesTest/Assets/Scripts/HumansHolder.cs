using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class HumansHolder : MonoBehaviour
    {
        [SerializeField] private List<Human> humans;
        [SerializeField] private bool isPlayer;
        [SerializeField] private Material playerMaterial;
        [SerializeField] private Material enemyMaterial;
        [SerializeField] private int minEnemyValue;
        [SerializeField] private int maxEnemyValue;
        [SerializeField] private TextMeshProUGUI valueLabel;
        [SerializeField] private GameObject canvas;

        public Action<bool> OnStateChange;
        public Action OnStartRun;
        public Action OnDeath;
        public Action OnFinish;
        public Action OnCoinPick;

        [SerializeField] private int _humansValue = 1;
        private bool _isAlive = true;
        private bool _isMoving = false;
        private bool _isFinished = false;

        private void Start()
        {
            if (!isPlayer)
            {
                _humansValue = Random.Range(minEnemyValue, maxEnemyValue);
            }
            ShowHumans();
        }

        public bool IsMoving()
        {
            return _isMoving;
        }

        public void HideHuman()
        {
            var aliveHumans = humans.Where(h => h.gameObject.activeSelf).ToList().Count;
            _humansValue = Mathf.Clamp(_humansValue, 0, aliveHumans);
            if (_humansValue <= 0)
            {
                Death();
            }
            UpdateLabel();
        }

        public void StartRun()
        {
            humans.ForEach(h => h.StartRun());
            _isMoving = true;
            OnStartRun?.Invoke();
            OnStateChange?.Invoke(_isMoving);
        }

        public void CollectCoin()
        {
            OnCoinPick?.Invoke();
        }

        public bool IsPlayer()
        {
            return isPlayer;
        }

        public int GetValue()
        {
            return _humansValue;
        }

        public void SetValue(int value)
        {
            _humansValue = value;
            if (_humansValue <= 0)
            {
                Death();
            }
            ShowHumans();
        }

        private void Death()
        {
            _humansValue = 0;
            _isAlive = false;
            _isMoving = false;
            OnStateChange?.Invoke(_isMoving);
            OnDeath?.Invoke();
            ShowHumans();
        }

        public void Idle()
        {
            humans.ForEach(h => h.Idle());
        }

        private void Finish()
        {
            _isFinished = true;
            _isMoving = false;
            OnStateChange?.Invoke(_isMoving);
            humans.ForEach(h => h.StartDance());
            OnFinish?.Invoke();
        }

        private void ShowHumans()
        {
            humans.ForEach(h => h.gameObject.SetActive(false));
            int humansToShow = Mathf.Clamp(_humansValue, 0, humans.Count);
            for (int i = 0; i < humansToShow; i++)
            {
                var material = isPlayer ? playerMaterial : enemyMaterial;
                humans[i].Spawn(material);
            }
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            valueLabel.text = _humansValue.ToString();
            canvas.SetActive(_isAlive);
        }

        public void SetMovingState(bool state)
        {
            _isMoving = state;
            OnStateChange?.Invoke(_isMoving);
        }

        private IEnumerator AttackHolder(HumansHolder holder)
        {
            var hol = isPlayer ? this : holder;
            hol.Idle();
            hol.SetMovingState(false);
            humans.ForEach(h => h.StartCast());
            yield return new WaitForSeconds(2f);
            hol.SetMovingState(true);
            var newValue = GetValue() + holder.GetValue();
            SetValue(newValue);
            if (isPlayer)
            {
                holder.SetValue(0);
                humans.ForEach(h => h.StartRun());
            }
            else
            {
                holder.Death();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isPlayer)
            {
                return;
            }

            if (other.gameObject.CompareTag("HumansHolder"))
            {
                HumansHolder holder = other.gameObject.GetComponent<HumansHolder>();
                if (holder.GetValue() <= GetValue())
                {
                    AttackEnemy(holder);
                }
                else
                {
                    holder.AttackEnemy(this);
                }
            }

            if (other.gameObject.CompareTag("Finish"))
            {
                if (!_isFinished)
                {
                    Finish();
                }
            }
        }

        private void AttackEnemy(HumansHolder holder)
        {
            StartCoroutine(AttackHolder(holder));
        }
    }
}
