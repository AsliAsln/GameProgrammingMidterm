using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_StackData", menuName = "Data/CD_StackData", order = 0)]
    public class CD_StackData : ScriptableObject
    {
        public StackData StackData;
    }
}