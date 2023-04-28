using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts
{
    public class VictoryWindow : Window
    {
        [SerializeField] private HumansHolder player;
        [SerializeField] private Button claimButton;
        [SerializeField] private Wallet wallet;
        [SerializeField] private TextMeshProUGUI rewardLabel;
        [SerializeField] private string rewardLabelTextTemplate;
        [SerializeField] private int minRewardValue;
        [SerializeField] private int maxRewardValue;

        private int _rewardValue;

        private void Awake()
        {
            player.OnFinish += Open;
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            base.Open();
            player.OnFinish -= Open;
            Initialize();
            claimButton.onClick.AddListener(ClaimReward);
        }

        public override void Close()
        {
            player.OnFinish -= Open;
            claimButton.onClick.RemoveListener(ClaimReward);
            base.Close();
        }

        private void Initialize()
        {
            _rewardValue = Random.Range(minRewardValue, maxRewardValue);
            SetLabel();
        }

        private void SetLabel()
        {
            rewardLabel.text = string.Format(rewardLabelTextTemplate, _rewardValue);
        }

        private void ClaimReward()
        {
            wallet.IncreaseCurrency(_rewardValue);
            claimButton.onClick.RemoveListener(ClaimReward);
            player.OnFinish -= Open;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Close();
        }
    }
}
