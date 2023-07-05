using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraView _cameraView;

        public override void InstallBindings()
        {
            Container.Bind<CameraView>().FromInstance(_cameraView).AsSingle();
            Container.BindInterfacesTo<Game>().AsSingle();

            BindFactories();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
        }
    }
}