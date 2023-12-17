using System;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using Color = Enums.Color;

namespace Controllers.Clone
{
    public class PlayerCloneController : MonoBehaviour
    {
        #region Self Variables
        #region public Variables

        #endregion
        #region Serialized Variables
        
        [SerializeField] private List<Material> materials;
        [SerializeField] private Animator animator;

        #endregion
        #region Private Variables
        
        private ObjectData _objectData;
        private ColorData _colorData;
        private PlayerMovementData _playerData;
        private GameObject _execution;
        private Transform _oldTransform;
        private bool _bullet;
        private GameObject _player;
        private GameObject _firstPlayer;
        private bool _hyperCasual;

        #endregion
        #endregion

        private void Awake()
        {
            _objectData = GetObjectData();
            _colorData = GetColorData();
            _playerData = GetPlayerData();
            materials = _colorData.Colors;
        }

        private void Start()
        {
            _player = FindObjectOfType<Managers.PlayerManager>().gameObject;
        }

        private void FixedUpdate()
        {
            if (MinigameSignals.Instance.onStackCount?.Invoke() >= 0)
            {
                Visibility();
            }
        }

        public void Visibility()
        {
            //if (transform.position.z < _player.transform.position.z - 11.7f)
            //{
            //    transform.GetChild(2).gameObject.SetActive(false);
            //    _firstPlayer = PlayerObjectsSignals.Instance.onFirstPlayerObject?.Invoke();
            //    CheckColor(_firstPlayer.transform.GetChild(0).gameObject);
            //}
            //else
            //{
            //    transform.GetChild(2).gameObject.SetActive(true);
            //}
        }

        private ObjectData GetObjectData(){return Resources.Load<CD_ObjectData>("Data/CD_ObjectData").ObjectData;}
        private ColorData GetColorData(){return Resources.Load<CD_ColorData>("Data/CD_ColorData").ColorData;}
        private PlayerMovementData GetPlayerData(){return Resources.Load<CD_PlayerData>("Data/CD_PlayerData").PlayerData.PlayerMovementData;}
        public void PlayerExecution(GameObject other){ _execution = other; }
        public void CheckColor(GameObject door)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i].color == door.GetComponent<Renderer>().material.color)
                {
                    Color index = (Color)Enum.Parse(typeof(Color), materials[i].name);
                    ColorChange(index);
                }
            }
        }

        public void MinigameColor(GameObject obj)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i].color == obj.transform.GetChild(0).GetComponent<Renderer>().material.color)
                {
                    Color index = (Color)Enum.Parse(typeof(Color), materials[i].name);
                    ColorChange(index);
                }
            }
        }
        
        public void ColorChange(Color color)
        {
            Material material = materials[(int)color];
            transform.GetChild(0).GetComponent<Renderer>().material = material;
        }

        public void MinigameAnimationChange()
        {
            if (_playerData.ForwardSpeed == 3)
            {
                PlayerAnimation(PlayerAnimationStates.hideWalk);
            }
            else if (_playerData.ForwardSpeed == 10)
            {
                PlayerAnimation(PlayerAnimationStates.run);
            }
        }

        public void ObjCasualStack()
        {
            PlayerObjectsSignals.Instance.onCasualStack?.Invoke();
            PlayerObjectsSignals.Instance.onIdleObjScale?.Invoke(true);
            transform.gameObject.SetActive(false);
        }

        public void MinigameControl()
        {
            PlayerObjectsSignals.Instance.minigameState?.Invoke("HelicopterMinigame");
            float distance = _objectData.distance;
            float i = _objectData.quantity;
            if (transform.position.x < 0)
            {
                transform.DOMoveX(-1.5f, .5f);
            }
            else
            {
                transform.DOMoveX(1.5f, .8f);
            }
            if (distance >= 7)
            {
                i = -.5f;
            }
            else if (distance <= 2.5f)
            {
                i = .5f;
            }
            distance += i;
            _objectData.distance = distance;
            _objectData.quantity = i;
            transform.DOMoveZ(transform.position.z + distance, 1).OnComplete(() => PlayerAnimation(PlayerAnimationStates.hide));
        }
        public  void Bullet(){ _bullet = true;}

        public void PlayExecution()
        {
            if (_bullet != true)
            {
                PlayerObjectsSignals.Instance.onMinigameAdd?.Invoke(transform.gameObject);
                PlayerAnimation(PlayerAnimationStates.dead); 
                DOVirtual.DelayedCall(5, ()=>PlayerObjectsSignals.Instance.onMinigamePoolAdd?.Invoke(transform.gameObject));
            }
        }

        public void SetOutlineBorder(bool isOutlineOn)
        {
            transform.GetChild(0).GetComponent<Renderer>().material.DOFloat(isOutlineOn ? 0f : 50f, "_OutlineSize", 1f);
        }
        public void PlayHelicopterExecution()
        {
            if (transform.GetChild(0).GetComponent<Renderer>().material.color != _execution.GetComponent<Renderer>().material.color ) // renkleri enum ataması yap ve onun üzerinden işlet.
            {
                transform.DOMoveY(_oldTransform.position.y + .4f, .2f);
                animator.SetTrigger("Dead");
                transform.DOMoveY(_oldTransform.position.y + .1f, .5f).SetDelay(1);
                DOVirtual.DelayedCall(5, ()=>PlayerObjectsSignals.Instance.onMinigamePoolAdd?.Invoke(transform.gameObject));
            }
            else
            {
                PlayerObjectsSignals.Instance.onSlowlyStack?.Invoke(transform.gameObject);
                DOVirtual.DelayedCall(.5f, () => SetOutlineBorder(false));
            }
        }

        public void Reset()
        {
            PlayerObjectsSignals.Instance.onMinigamePoolAdd?.Invoke(transform.gameObject);
        }

        public void PlayerAnimation(PlayerAnimationStates animationState)
        {
            animator.SetTrigger(animationState.ToString());
        }
    }
}