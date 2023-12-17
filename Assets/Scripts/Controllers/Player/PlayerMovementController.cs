using System;
using Data.ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Managers;
using Unity.Mathematics;
using Enums;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Managers.PlayerManager manager;
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        [Header("Data")] [ShowInInspector] private PlayerMovementData _data;
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay;
        [ShowInInspector] private float _inputValue;
        [ShowInInspector] private Vector3 _joystickInput;
        [ShowInInspector] private Vector2 _clampValues;
        bool _station;
        #endregion

        #endregion

        internal void SetMovementData(PlayerData playerData)
        {
            _data = playerData.PlayerMovementData;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
                //PlayerSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
                //PlayerSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
        }

        private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;

        private void UnSubscribeEvents()
        {
            //PlayerSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;
            //PlayerSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
            Stop();
        }
        public void UpdateInputValue(HorizontalInputParams inputParams)
        {
            _inputValue = inputParams.HorizontalInputValue;
            _clampValues = inputParams.HorizontalInputClampSides;
        }

        public void JoystickInputValue(JoystickInputParams joystickInputParams)
        {
            _joystickInput = joystickInputParams.JoystickMove;
        }


        private void Update()
        {
            if (_isReadyToPlay)
            {
                //manager.SetStackPosition();
            }
        }

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                    Move();
                }
                else
                {
                    if (manager.CurrentGameState == GameStates.Runner)
                    {
                        StopSideways();

                    }
                    else if (manager.CurrentGameState == GameStates.Idle)
                    {
                        Stop();
                    }
                }
            }
        }
        private void Move()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _data.SidewaysSpeed, velocity.y,
                _data.ForwardSpeed);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                    _clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
            Debug.Log("moving");
        }
        private void IdleMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _data.SidewaysSpeed, velocity.y,
                _data.ForwardSpeed);
            rigidbody.velocity = velocity;
        }
        public void Station(bool variable)
        {
           // _station = variable;
        }
        public void Finish()
        {
            _station = true;
        }
        public void Play()
        {
            _station = false;
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(Enums.PlayerAnimationStates.run);
            OnMoveConditionChanged(true);
            IsReadyToPlay(true);
        }

        public void SpeedDown()
        {
            _data.ForwardSpeed = 3;
        }

        private void StopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
            manager.transform.position = Vector3.zero;
            gameObject.transform.rotation = quaternion.identity;
        }
    }
}