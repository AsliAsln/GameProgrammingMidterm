using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Command.StackController
{
    public class ListChangeCommand
    {
        #region Self Variables
        #region Private Variables

        private List<GameObject> _stackList;
        private List<GameObject> _poolList;
        private Transform _stackTransform;
        private Transform _poolTransform;
        private Transform _player;
        private StackData _stackData;
        private int list;

        #endregion
        #endregion

        public ListChangeCommand(ref List<GameObject> stackList, ref List<GameObject> poolList, Transform stackStackTransform, Transform poolTransform ,Transform player, ref StackData stackData)
        {
            _stackList = stackList;
            _poolList = poolList;
            _stackTransform = stackStackTransform;
            _poolTransform = poolTransform;
            _stackData = stackData;
            _player = player;
        }
        
        public void ListChange(GameObject obj, string listName)
        {
            if (listName == "Stack") list = 2;
            else if (listName == "Pool") list = 1;

            switch (list)
            {
                case 1:
                    _poolList.Add(obj);
                    if (_stackList.Contains(obj) != null)
                    {
                        _stackList.Remove(obj);
                    }
                    _stackList.TrimExcess();
                    obj.transform.SetParent(_poolTransform);
                    obj.SetActive(false);
                    break;
                
                case 2:
                    _stackList.Add(obj);
                    if (_poolList.Contains(obj) != null)
                    {
                        _poolList.Remove(obj);   
                    }
                    _poolList.TrimExcess();
                    Vector3 newPos = _player.position;
                    var index = _stackList.Count;
                    newPos.z -= index - (_stackData.CollectableOffsetInStack * index);
                    obj.transform.position = newPos;
                    
                    obj.transform.SetParent(_stackTransform);
                    obj.SetActive(true);
                    break;
            }
        }
    }
}