using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class TutorialWindow : Window
    {
        [SerializeField] private Button startButton;
        [SerializeField] private HumansHolder player;
        [SerializeField] private GameObject mainWIndow;
        [SerializeField] private TextMeshProUGUI levelLabel;
        [SerializeField] private string levelLabelTextTemplate;

        private string _levelIdSaveKey = "LvlIdSaveKey";

        private int _currentLvlId;

        private void Awake()
        {
            player.OnFinish += OnLevelCompleted;
        }

        private void OnDestroy()
        {
            player.OnFinish -= OnLevelCompleted;
        }

        private void Start()
        {
            Open();
        }

        public override void Open()
        {
            base.Open();
            _currentLvlId = PlayerPrefs.GetInt(_levelIdSaveKey, 1);
            ShowLvlInfo();
            startButton.onClick.AddListener(Close);
        }

        private void OnLevelCompleted()
        {
            _currentLvlId++;
            PlayerPrefs.SetInt(_levelIdSaveKey, _currentLvlId);
        }

        private void ShowLvlInfo()
        {
            levelLabel.text = string.Format(levelLabelTextTemplate, _currentLvlId);
        }

        public override void Close()
        {
            startButton.onClick.RemoveListener(Close);
            player.StartRun();
            mainWIndow.SetActive(true);
            base.Close();
        }
    }
}
