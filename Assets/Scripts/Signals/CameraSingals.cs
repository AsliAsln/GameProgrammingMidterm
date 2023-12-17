using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction<CameraStates> onChangeCameraState = delegate { };
    }
}