using Core;
using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.Battle;
using Zenject;
using Core.UI;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        DeclareSignals();
        Container.Bind<ILogger>().To<StandaloneLogger>().AsCached();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<PlayerCollectedProof>();
    }
}
