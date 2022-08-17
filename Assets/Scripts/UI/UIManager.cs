using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Astutos.Randoman.Player;

namespace Astutos.Randoman.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] PlayerCollision _playerCollision;
        [SerializeField] TextMeshProUGUI _coinDisplay;
        [SerializeField] TextMeshProUGUI _timeDisplay;

        private void OnEnable()
        {
            _playerCollision.OnCoinCountIncrease += NewCoinAmount;
        }
        private void OnDisable()
        {
            _playerCollision.OnCoinCountIncrease -= NewCoinAmount;
        }

        private void NewCoinAmount(int coinAmount)
        {
            _coinDisplay.text = coinAmount.ToString();
        }

        private void Update()
        {
            _timeDisplay.text = Mathf.RoundToInt(Time.time).ToString() + "s";
        }

    }

}