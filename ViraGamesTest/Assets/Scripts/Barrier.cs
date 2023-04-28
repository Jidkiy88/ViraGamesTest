using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Barrier : MonoBehaviour
    {
        [SerializeField] private List<GameObject> twins;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image fade;
        [SerializeField] private string textTemplate;
        [SerializeField] private int minValue;
        [SerializeField] private int maxValue;
        [SerializeField] private Color plusColor;
        [SerializeField] private Color minusColor;
        [SerializeField] private Color multiplyÑolor;
        [SerializeField] private bool isKinematic;


        private int _value;
        private string _symbol;

        private void Start()
        {
            SpawnBarrier();
        }

        private void SpawnBarrier()
        {
            _value = Random.Range(minValue, maxValue);
            var randomSymbol = Random.Range(1, 4);
            switch (randomSymbol)
            {
                case 1:
                    {
                        _symbol = "+";
                        fade.color = plusColor;
                        break;
                    }
                case 2:
                    {
                        _symbol = "-";
                        fade.color = minusColor;
                        break;
                    }
                case 3:
                    {
                        _symbol = "*";
                        fade.color = multiplyÑolor;
                        break;
                    }
            }
            label.text = string.Format(textTemplate, _symbol, _value);
        }

        public int UseBarrier(int prevValue)
        {
            int value = new int();
            switch (_symbol)
            {
                case "+":
                    {
                        value = prevValue + _value;
                        break;
                    }
                case "-":
                    {
                        value = prevValue - _value;
                        break;
                    }
                case "*":
                    {
                        value = prevValue * _value;
                        break;
                    }
            }
            if (twins.Any())
            {
                twins.ForEach(t => t.gameObject.SetActive(false));
            }
            gameObject.SetActive(false);
            return value;
        }
    }
}
