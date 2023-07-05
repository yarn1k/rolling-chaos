using Zenject;

namespace Core.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Game>().AsSingle();

            BindFactories();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
        }
    }
}