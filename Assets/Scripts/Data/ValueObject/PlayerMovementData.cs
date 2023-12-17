using System;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerMovementData
    {
        public float SidewaysSpeed = 10;
        public float ForwardSpeed = 100;
    }
}