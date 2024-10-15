using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GoolTrigger : MonoBehaviour
    {
        public static UnityAction OnGoooal;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnGoooal?.Invoke();
        }
    }
}