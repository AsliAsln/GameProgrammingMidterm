using System;
using DG.Tweening;
using UnityEngine;
using Managers;
using Signals;

namespace Controllers.Clone
{
    public class PlayerClonePhysicController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private Managers.PlayerClonesManager playerClonesManager;
        [SerializeField] private PlayerCloneController playerClonesController;

        #endregion
        #region Private Variables

        private bool _bullet;
        private bool CasualGame;
        private bool _trigger;
        
        #endregion
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ColorChangeDoor"))
            {
                playerClonesManager.PlayerColorChange(other.gameObject);
            }

            if (other.CompareTag("MinigameHelicopter"))
            {
                playerClonesManager.PlayersMinigameControl();
            }
            
            if (other.CompareTag("Platform"))
            {
                playerClonesController.PlayerExecution(other.gameObject);
            }

            if (other.CompareTag("MinigameTarret"))
            {
                playerClonesController.MinigameAnimationChange();
            }

            if (other.CompareTag("Bullet"))
            {
                if (_bullet != true)
                {
                    playerClonesController.PlayExecution();
                    playerClonesController.Bullet();
                }
                _bullet = true;
            }

            if (other.CompareTag("IdleMap"))
            {
                _trigger = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("IdleObj"))
            {
                if (other.transform.position == transform.position)
                {
                    if (!_trigger)
                    {
                        playerClonesController.ObjCasualStack();
                        _trigger = true;
                    }
                }
            }
        }
    }
}