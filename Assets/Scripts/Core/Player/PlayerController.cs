using Core.Battle;
using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.UI;
using Core.NPC;
using Core.Proof;
using Core.Quest;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Core.Player
{
    public class PlayerController : IInitializable
    {
        private SignalBus _signalBus;
        private Camera _camera;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        private QuestModel _currentQuest;
        private List<ProofModel> _proofs = new();
        private List<NPCModel> _talkings = new();
        private NPCModel _lastInterlocutor;

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

        public void AddProof(PlayerCollectedProof signal)
        {
            _proofs.Add(signal.Proof);
        }

        public bool CheckBattle(NPCModel npc)
        {
            _lastInterlocutor = npc;
            if (!_talkings.Contains(npc) && 
                _currentQuest.NPC.Where(x => x.Model == npc && 
                x.QuestType != QuestType.QuestGiver).Count() > 0)
                return true;
            return false;
        }

        public void LoadBattleScene(SignalBus signalBus)
        {
            List<ProofModel> proofs = GetAvailableProofs();
            SceneManager.LoadScene(Constants.Scenes.Battle);
            BattleView.InitBattle(new BattleInitialize { Player = _model, npc = _lastInterlocutor, Proofs = proofs });
        }

        private List<ProofModel> GetAvailableProofs()
        {
            List<ProofModel> proofs = new();
            foreach (var proof in _proofs)
            {
                if (proof.Quest == _currentQuest)
                    proofs.Add(proof);
            }
            return proofs;
        }

        public void OnQuest—ompleted(Quest—ompleted signal)
        {
            _talkings.Clear();
            _currentQuest = signal.NextQuest;
        }
    }
}
