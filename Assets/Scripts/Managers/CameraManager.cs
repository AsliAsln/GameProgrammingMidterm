using System;
using Cinemachine;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        [ShowInInspector] private float3 _initialPosition;
        private PlayerManager _playerManager;


        #endregion

        #endregion

        #region Event Subscriptions

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _initialPosition = transform.position;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            CameraSignals.Instance.onChangeCameraState += OnChangeCameraState;
            CameraSignals.Instance.onChangeCameraState += OnSetCameraTarget;
        }

    
        private void OnChangeCameraState(CameraStates state)
        {
            animator.SetTrigger(state.ToString());
        }
        private void OnSetCameraTarget(CameraStates Currentstate)
        {
            if (!_playerManager)
            {
                _playerManager = FindObjectOfType<PlayerManager>();
            }
            if (Currentstate == CameraStates.Follow)
            {
                stateDrivenCamera.Follow = _playerManager.transform;
                stateDrivenCamera.LookAt = null;
            }
            else
            {
                stateDrivenCamera.LookAt = _playerManager.transform;
                stateDrivenCamera.enabled = false;
            }
            animator.SetTrigger(Currentstate.ToString());

        }
     
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CameraSignals.Instance.onChangeCameraState -= OnChangeCameraState;
            CameraSignals.Instance.onChangeCameraState -= OnSetCameraTarget;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnReset()
        {
            CameraSignals.Instance.onChangeCameraState?.Invoke(CameraStates.Initial);
            stateDrivenCamera.Follow = null;
            stateDrivenCamera.LookAt = null;
            transform.position = _initialPosition;
        }
    }
}