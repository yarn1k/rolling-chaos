using Core;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<ILogger>().To<StandaloneLogger>().AsCached();
    }
}
