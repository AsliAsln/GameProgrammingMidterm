using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_InputData", menuName = "Data/CD_InputData", order = 0)]
    public class CD_InputData : ScriptableObject
    {
        public InputData InputData;
    }
}