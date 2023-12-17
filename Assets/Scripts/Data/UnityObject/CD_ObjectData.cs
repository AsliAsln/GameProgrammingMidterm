using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_ObjectData", menuName = "Data/CD_ObjectData", order = 0)]
    public class CD_ObjectData : ScriptableObject
    {
        public ObjectData ObjectData;
    }
}