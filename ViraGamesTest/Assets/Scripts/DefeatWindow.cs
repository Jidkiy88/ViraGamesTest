using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts
{
    public class DefeatWindow : Window
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private HumansHolder player;

        private void Awake()
        {
            player.OnDeath += Open;
            gameObject.SetActive(false);
        }

        public override void Open()
        {
            base.Open();
            player.OnDeath -= Open;
            restartButton.onClick.AddListener(Close);
        }

        public override void Close()
        {
            restartButton.onClick.RemoveListener(Close);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            base.Close();
        }
    }
}
