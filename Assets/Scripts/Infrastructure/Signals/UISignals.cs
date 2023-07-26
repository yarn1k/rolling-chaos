using UnityEngine;

namespace Core.Infrastructure.Signals.UI
{
    public struct OpenDialoguePanel
    {
        public Sprite Portrait;
        public string Name;
    }
    public struct CloseDialoguePanel {}
    public struct InteractableBattleButton
    {
        public bool Value;
    }
}