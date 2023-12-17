using System;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers.PlayerManager
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BasketPlatform"))
            {
                DOVirtual.DelayedCall(1f,()=>PlayerObjectsSignals.Instance.onBasket?.Invoke());
            }
        }
    }
}