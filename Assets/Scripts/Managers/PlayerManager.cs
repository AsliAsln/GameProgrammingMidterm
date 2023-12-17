using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables
        #region public variables
        public GameStates CurrentGameState;
        #endregion
        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [Header("Data")] public PlayerData Data;


        #endregion

        #region Private Variables

        private GameObject _platform;


        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnNormalMovement;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onStation += OnStation;
            CoreGameSignals.Instance.onPlayerGameChange += OnChangeGameState;
            InputSignals.Instance.onJoystickMovement += OnJoystickMovement;
            MinigameSignals.Instance.onSlowMove += OnSlowMove;
            PlayerObjectsSignals.Instance.onIdleObjScale += OnIdleObjScale;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnNormalMovement;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onStation -= OnStation;
            CoreGameSignals.Instance.onPlayerGameChange -= OnChangeGameState;
            InputSignals.Instance.onJoystickMovement -= OnJoystickMovement;
            MinigameSignals.Instance.onSlowMove -= OnSlowMove;
            PlayerObjectsSignals.Instance.onIdleObjScale -= OnIdleObjScale;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }
        #endregion
        private PlayerData GetPlayerData() => Resources.Load<CD_PlayerData>("Data/CD_PlayerData").PlayerData; 

        private void OnStation(bool variable)
        {
            playerMovementController.Station(variable);
        }
        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(Data);
        }
        private void OnReset()
        {
            playerMovementController.OnReset();
        }

        private void OnPlay()
        {
            playerMovementController.Play();
        }

        public void OnFinish()
        {
            playerMovementController.Finish();
        }
        
        private void OnNormalMovement(HorizontalInputParams horizontalInputParams)
        {
            playerMovementController.UpdateInputValue(horizontalInputParams);
        }

        private void OnJoystickMovement(JoystickInputParams joystickInputParams)
        {
            playerMovementController.JoystickInputValue(joystickInputParams);
        }

        private void OnSlowMove()
        {
            playerMovementController.SpeedDown();
        }
        private void OnIdleObjScale(bool state)
        {
           // playerMovementController.IdleObjScale(state);
        }
        private void OnChangeGameState(GameStates CurrentState)
        {
            CurrentGameState = CurrentState;
            if (CurrentState == GameStates.Idle)
            {
                ActivateAllMovement(true);
            }
        }
        public void ActivateAllMovement(bool Activate)
        {
            playerMovementController.IsReadyToPlay(Activate);
        }
    }
}