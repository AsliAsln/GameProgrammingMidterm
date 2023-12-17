using Controllers.Clone;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerClonesManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerCloneController playerClonesController;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState += OnPlayerAnimation;
            MinigameSignals.Instance.onPlayHelicopterExecution += OnPlayHelicopterExecution; 
            StackSignals.Instance.onMinigameColor += OnMinigameColor;
            StackSignals.Instance.onSetOutlineBorder += OnSetOutlineBorder;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState -= OnPlayerAnimation;
            MinigameSignals.Instance.onPlayHelicopterExecution -= OnPlayHelicopterExecution;
            StackSignals.Instance.onMinigameColor -= OnMinigameColor;
            StackSignals.Instance.onSetOutlineBorder -= OnSetOutlineBorder;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnPlayerAnimation(PlayerAnimationStates animation)
        {
            playerClonesController.PlayerAnimation(animation);
        }

        public void PlayersMinigameControl()
        {
            playerClonesController.MinigameControl();
        }

        public void PlayerColorChange(GameObject door)
        {
            playerClonesController.CheckColor(door);
        }

        private void OnPlayHelicopterExecution()
        {
            playerClonesController.PlayHelicopterExecution();
        }

        private void OnMinigameColor(GameObject gameObject)
        {
            playerClonesController.MinigameColor(gameObject);
        }

        private void OnSetOutlineBorder(bool isOutlineOn)
        {
            playerClonesController.SetOutlineBorder(isOutlineOn);
        }
        private void OnReset()
        {
            playerClonesController.Reset();
        }
        
    }
}