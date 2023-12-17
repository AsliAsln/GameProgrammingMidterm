using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_ColorData", menuName = "Data/CD_ColorData", order = 0)]
    public class CD_ColorData : ScriptableObject
    {
        public ColorData ColorData;
    }
}