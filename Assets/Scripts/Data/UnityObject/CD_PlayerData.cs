using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PlayerData", menuName = "Data/CD_PlayerData", order = 0)]
    public class CD_PlayerData : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}