using Core.Infrastructure.Signals.Battle;
using Core.NPC;
using Core.Player;
using Core.Proof;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Zenject;

namespace Core.Battle
{
    public class AsyncProcessor : MonoBehaviour
    {
        // Purposely left empty
    }

    public class DialogueBattleSystem : IInitializable
    {
        private readonly SignalBus _signalBus;
        private AsyncProcessor _asyncProcessor;
        private int _playerMentalPoints = 3;
        private int _enemyMentalPoints = 3;
        private PlayerModel _player;
        private NPCModel _enemy;
        private Combination? _selectedSkill;
        private int _clueBonusIndex = -1;
        private Button _fight;
        private List<object> _history = new ();

public DialogueBattleSystem(SignalBus signalBus, AsyncProcessor asyncProcessor, Button fight)
        {
            _signalBus = signalBus;
            _asyncProcessor = asyncProcessor;
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

                _asyncProcessor.StartCoroutine(SendPostRequest("player", true));

                if (attack > defense)
                {
                    _enemyMentalPoints--;
                    _asyncProcessor.StartCoroutine(SendPostRequest("enemy", true));
                } 
                else
                {
                    _asyncProcessor.StartCoroutine(SendPostRequest("enemy", false));
                }

                //_history.Add(new Dictionary<string, object>() { { "role", "user" }, { "content", "Hello!" } });
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

                / _asyncProcessor.StartCoroutine(SendPostRequest("player", true));

                if (attack > defense)
                {
                    _playerMentalPoints--;
                    //_asyncProcessor.StartCoroutine(SendPostRequest("enemy", false));
                }
                else
                {
                    //_asyncProcessor.StartCoroutine(SendPostRequest("enemy", true));
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

        IEnumerator<UnityWebRequestAsyncOperation> SendPostRequest(string role, bool win)
        {
            Dictionary<string, object> parameters = new();
            if (role == "player")
            {
                parameters.Add("role", _player.Name);
                parameters.Add("facts", new object[0]);
            }
            else
            {
                parameters.Add("role", _enemy.Name);
                parameters.Add("facts", new[]
                {
                    "украл хлеб из местной пекарни",
                    "fed homeless"
                });
            }
            parameters.Add("acceptence", win);
            parameters.Add("history", _history);
            
            
            string jsonBody = JsonConvert.SerializeObject(parameters);
            //jsonBody= "[" + jsonBody + "]";
            Debug.Log(jsonBody);

            UnityWebRequest www = UnityWebRequest.Post("https://jam.bezsoul.studio/qa", jsonBody);
            www.SetRequestHeader("accept", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Response: " + www.downloadHandler.text);
                Debug.Log("POST request sent successfully.");
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
            {
                Debug.Log("Win");
                //если quest_finisher = завершить квест
                //иначе выдать награду
                //npc добавляется в список talkings
            }
            Debug.Log("EndBattle");
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<BattleSkillSelected>(SelectSkill);
            _signalBus.Unsubscribe<BattleProofSelected>(SelectProof);
        }
    }
}