using Core.Infrastructure.Signals.Game;
using Core.Infrastructure.Signals.Battle;
using Core.Proof;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Battle
{
    public class BattleView : MonoBehaviour
    {
        private SignalBus _signalBus;
        private static BattleInitialize _battleInit;
        [SerializeField]
        private Image _playerPortrait;
        [SerializeField]
        private Image _enemyPortrait;
        [SerializeField]
        private Transform _battleLog;
        [SerializeField]
        private Transform _proofs;
        [SerializeField]
        private GameObject _message;
        [SerializeField]
        private GameObject _proof;

        public static BattleInitialize BattleInit => _battleInit;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<BattleLogMessage>(AddMessage);
        }

        private void Start()
        {
            InitPortraits(_battleInit.Player.Model.Portrait, _battleInit.npc.Portrait);
            InitProofs(_battleInit.Proofs);
        }

        public static void InitBattle(BattleInitialize signal)
        {
            _battleInit = signal;
        }

        private void InitPortraits(Sprite playerPortrait, Sprite enemyPortrait)
        {
            _playerPortrait.sprite = playerPortrait;
            _enemyPortrait.sprite = enemyPortrait;
        }

        private void InitProofs(List<ProofModel> proofs)
        {
            foreach (var proof in proofs)
            {
                GameObject pro = Instantiate(_proof, _proofs);
                var textMesh = pro.GetComponent<TMP_Text>();
                textMesh.text = proof.Name;
                var proofView = pro.AddComponent<ProofView>();
                proofView.SetModel(proof);
            }
            _proofs.GetComponent<ProofSelection>().Init();
        }

        private void AddMessage(BattleLogMessage signal)
        {
            var proof = Instantiate(_message, _battleLog);
            var textMesh = proof.GetComponent<TMP_Text>();
            textMesh.text = signal.Message;
            if (signal.Sender == "user")
                textMesh.alignment = TextAlignmentOptions.Left;
            else
                textMesh.alignment = TextAlignmentOptions.Right;
        }
    }
}
