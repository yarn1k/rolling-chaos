using Core.Battle;
using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.UI;
using Core.NPC;
using Core.Proof;
using Core.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Player
{
    public class PlayerController : IDisposable
    {
        private SignalBus _signalBus;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        private Camera _camera;
        private static QuestModel _currentQuest;
        private static List<ProofModel> _proofs = new();
        private static List<NPCModel> _talkings = new();
        private NPCModel _lastInterlocutor;

        public PlayerModel Model => _model;
        public static QuestModel CurrentQuest => _currentQuest;
        public Transform PlayerTransform => _view.transform;

        public PlayerController(SignalBus signalBus, PlayerModel playerModel, PlayerView playerView)
        {
            _signalBus = signalBus;
            _model = playerModel;
            _view = playerView;

            _camera = Camera.main;

            _signalBus.Subscribe<CheckPossibilityOfBattle>(OnCheckBattle);
            _signalBus.Subscribe<BattleLoadScene>(LoadBattleScene);
            _signalBus.Subscribe<PlayerCollectedProof>(AddProof);

            _view.InitAgent(_model.MovementSpeed);
        }

        public void Update()
        {
            MovementInput();
            _view.FaceTarget();
            _view.SetAnimations();
        }

        private void MovementInput()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

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

        private void OnCheckBattle(CheckPossibilityOfBattle signal)
        {
            var interactable = CheckBattle(signal.npc);
            _signalBus.Fire(new InteractableBattleButton { Value = interactable });
        }

        private void LoadBattleScene()
        {
            List<ProofModel> proofs = GetAvailableProofs();
            SceneManager.LoadScene(Constants.Scenes.Battle);
            BattleView.InitBattle(new BattleInitialize {
                Player = this,
                npc = _lastInterlocutor,
                Proofs = proofs });
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

        public void OnQuest—ompleted(QuestModel nextQuest)
        {
            _talkings.Clear();
            _currentQuest = nextQuest;
        }

        public void MarkNotTalkingNPC(NPCModel enemy)
        {
            _talkings.Add(enemy);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CheckPossibilityOfBattle>(OnCheckBattle);
            _signalBus.Unsubscribe<BattleLoadScene>(LoadBattleScene);
            _signalBus.Unsubscribe<PlayerCollectedProof>(AddProof);
        }
    }
}
