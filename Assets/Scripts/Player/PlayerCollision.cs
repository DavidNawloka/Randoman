using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astutos.Randoman.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        public event Action<int> OnCoinCountIncrease;

        public int _coinCount = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Coin")
            {
                _coinCount++;
                Destroy(collision.gameObject);
                OnCoinCountIncrease?.Invoke(_coinCount);
            }
            
        }
    }

}