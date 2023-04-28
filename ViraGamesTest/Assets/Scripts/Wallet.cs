using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private HumansHolder player;
        [SerializeField] private TextMeshProUGUI coinsLabel;
        public int _coinsValue = 10;
        private string _coinsSaveKey = "CoinsValue";

        private void Awake()
        {
            player.OnCoinPick += PickCoin;
        }

        private void Start()
        {
            Load();
            UpdateLabel();
        }

        public int GetCoins()
        {
            return _coinsValue;
        }

        private void OnDestroy()
        {
            player.OnCoinPick -= PickCoin;
        }

        private void Load()
        {
            _coinsValue = PlayerPrefs.GetInt(_coinsSaveKey, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(_coinsSaveKey, _coinsValue);
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            coinsLabel.text = _coinsValue.ToString();
        }

        public void IncreaseCurrency(int value)
        {
            _coinsValue += value;
            Save();
        }

        public void PickCoin()
        {
            _coinsValue++;
            Save();
        }
    }
}
