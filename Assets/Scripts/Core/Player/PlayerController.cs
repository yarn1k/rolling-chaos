using UnityEngine;

namespace Core.Player
{
    public class PlayerController
    {
        private Camera _camera;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public PlayerController(PlayerModel playerModel, PlayerView playerView)
        {
            _camera = Camera.main;
            _model = playerModel;
            _view = playerView;

            _view.InitAgent(_model.MovementSpeed);
        }

        private void MovementInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, _view.Ground))
                {
                    _view.Move(hit);
                }
            }
        }

        public void Update()
        {
            MovementInput();
        }
    }
}
