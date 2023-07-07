using Core.Infrastructure.Signals.UI;
using Zenject;

namespace Core.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<OpenDialoguePanel>();
            Container.DeclareSignal<CloseDialoguePanel>();
            Container.DeclareSignal<InteractableBattleButton>();
        }
    }
}