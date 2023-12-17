using Enums;
using Extentions;
using Keys;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {

        public UnityAction<byte> onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction<GameObject> onCheckAreaControl = delegate { };
        public Func<byte> onGetLevelID = delegate { return 0; };

        public UnityAction onMiniGameEntered = delegate { };
        public UnityAction onMiniGameStart = delegate { };
        public Func<byte> onGetIncomeLevel = delegate { return 0; };
        public Func<byte> onGetStackLevel = delegate { return 0; };

        public UnityAction onGameChange = delegate { };
        public UnityAction<GameStates> onPlayerGameChange = delegate { };
        public UnityAction<bool> onStation = delegate { };
        public UnityAction<string> minigameState = delegate { };

    }
}