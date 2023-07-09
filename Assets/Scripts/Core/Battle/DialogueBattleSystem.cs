using Core.Infrastructure.Signals.Battle;
using Core.NPC;
using Core.Player;
using Core.Proof;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Battle
{
    public class DialogueBattleSystem : IInitializable
    {
        private readonly SignalBus _signalBus;
        private int _playerMentalPoints = 3;
        private int _enemyMentalPoints = 3;
        private PlayerModel _player;
        private NPCModel _enemy;
        private Combination? _selectedSkill;
        private int _clueBonusIndex = -1;
        private Button _fight;

        public DialogueBattleSystem(SignalBus signalBus, Button fight)
        {
            _signalBus = signalBus;
            _fight = fight;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BattleSkillSelected>(SelectSkill);
            _signalBus.Subscribe<BattleProofSelected>(SelectProof);

            _player = BattleView.BattleInit.Player;
            _enemy = BattleView.BattleInit.npc;
            _fight.onClick.AddListener(Fight);
        }

        private void SelectSkill(BattleSkillSelected signal)
        {
            _selectedSkill = signal.Skill;
        }

        private void SelectProof(BattleProofSelected signal)
        {
            _clueBonusIndex = signal.Index;
        }

        private int PlayerAttack()
        {
             return RollDice() + GetPlayerSkillValue(_selectedSkill) + GetClueBonus(_clueBonusIndex);
        }

        private int PlayerDefence()
        {
            return RollDice() + _player.Insight;
        }

        private int EnemyAttack()
        {
            return RollDice() + GetEnemySkillValue(_selectedSkill);
        }

        private int EnemyDefence()
        {
            return RollDice() + _enemy.Insight;
        }

        private int RollDice()
        {
            int sum = 0;
            for (int i = 0; i < 4; i++)
            {
                int roll = Random.Range(-1, 2);
                sum += roll;
            }

            return sum;
        }

        private int GetPlayerSkillValue(Combination? skill)
        {
            switch (skill)
            {
                case Combination.Persuasion:
                    return _player.Persuasion;
                case Combination.Intimidation:
                    return _player.Intimidation;
                case Combination.Deception:
                    return _player.Deception;
                default:
                    return 0;
            }
        }

        private int GetEnemySkillValue(Combination? skill)
        {
            switch (skill)
            {
                case Combination.Persuasion:
                    return _enemy.Persuasion;
                case Combination.Intimidation:
                    return _enemy.Intimidation;
                case Combination.Deception:
                    return _enemy.Deception;
                default:
                    return 0;
            }
        }

        private int GetClueBonus(int clue)
        {
            return clue != -1 ? 2 : 0;
        }

        private void Fight()
        {
            if (_selectedSkill != null)
            {
                _fight.interactable = false;

                int attack = PlayerAttack();
                int defense = EnemyDefence();

                if (attack > defense)
                {
                    _enemyMentalPoints--;
                }

                _signalBus.Fire(new BattleLogMessage { Message = "Hello!", Sender = "Player" });
                _signalBus.Fire(new BattleLogMessage { Message = "Hello!", Sender = "Enemy" });

                if (_enemyMentalPoints == 0)
                {
                    EndBattle();
                    return;
                }

                Debug.Log("Enemy Mental Points: " + _enemyMentalPoints);

                int attack2 = EnemyAttack();
                int defense2 = PlayerDefence();

                if (attack > defense)
                {
                    _playerMentalPoints--;
                }

                Debug.Log("Player Mental Points: " + _playerMentalPoints);

                if (_playerMentalPoints == 0)
                {
                    EndBattle();
                    return;
                }

                _signalBus.Fire(new BattleLogMessage { Message = "Hello!", Sender = "Enemy" });
                _signalBus.Fire(new BattleLogMessage { Message = "Hello!", Sender = "Player" });

                EndRound();
            }
        }

        private void EndRound()
        {
            _selectedSkill = null;
            //снять выделение со скилла
            if (_clueBonusIndex != -1)
            {
                _signalBus.Fire(new BattleProofUsed { Index = _clueBonusIndex });
                _clueBonusIndex = -1;
            }
            _fight.interactable = true;
        }

        private void EndBattle()
        {
            if (_enemyMentalPoints == 0)
                Debug.Log("Win");
            //выдать награду
            //npc добавляется в список talkings
            Debug.Log("EndBattle");
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<BattleSkillSelected>(SelectSkill);
            _signalBus.Unsubscribe<BattleProofSelected>(SelectProof);
        }
    }
}