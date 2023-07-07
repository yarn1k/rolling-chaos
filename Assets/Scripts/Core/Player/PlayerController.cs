using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.UI;
using Core.NPC;
using Core.Proof;
using Core.Quest;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Core.Player
{
    public class PlayerController : IInitializable
    {
        private Camera _camera;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        private QuestModel _currentQuest;
        private List<ProofModel> _proofs = new();
        private List<NPCModel> _talkings = new();

        public Transform PlayerTransform => _view.transform;

        public PlayerController(PlayerModel playerModel, PlayerView playerView)
        {
            _camera = Camera.main;
            _model = playerModel;
            _view = playerView;
        }

        void IInitializable.Initialize()
        {
            _view.InitAgent(_model.MovementSpeed);
        }

        public void Update()
        {
            MovementInput();
        }

        private void MovementInput()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, _view.Ground))
                {
                    _view.Move(hit);
                }
            }
        }

        public void InitQuest(QuestModel quest)
        {
            _currentQuest = quest;
        }

        private void AddProof(ProofModel proof)
        {
            _proofs.Add(proof);
        }

        public bool CheckBattle(NPCModel npc)
        {
            if (!_talkings.Contains(npc) && 
                _currentQuest.NPC.Where(x => x.Model == npc && 
                x.QuestType != QuestType.QuestGiver).Count() > 0)
                return true;
            return false;
        }

        public void OnQuest—ompleted(Quest—ompleted signal)
        {
            _talkings.Clear();
            _currentQuest = signal.NextQuest;
        }
    }
}
