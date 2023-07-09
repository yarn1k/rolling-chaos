using Core.Infrastructure.Signals.Battle;
using Core.Proof;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Battle
{
    public class ProofSelection : MonoBehaviour
    {
        private SignalBus _signalBus;
        [SerializeField]
        private Color _selectedColor;
        [SerializeField]
        private Color _defaultColor;
        private TMP_Text[] _proofTexts;
        private int _selectedProofIndex = -1;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<BattleSkillSelected>(OnSkillChanged);
            _signalBus.Subscribe<BattleProofUsed>(OnBattleProofUsed);
        }

        public void Init()
        {
            _proofTexts = GetComponentsInChildren<TMP_Text>();

            foreach (TMP_Text proofText in _proofTexts)
            {
                proofText.color = _defaultColor;

                proofText.GetComponent<Button>().onClick.AddListener(() =>
                {
                    int index = System.Array.IndexOf(_proofTexts, proofText);
                    SelectProof(index);
                });
            }
        }

        private void SelectProof(int index)
        {
            if (index >= 0 && index < _proofTexts.Length)
            {
                if (_selectedProofIndex >= 0)
                {
                    _proofTexts[_selectedProofIndex].color = _defaultColor;
                }

                _proofTexts[index].color = _selectedColor;
                _selectedProofIndex = index;
                _signalBus.Fire(new BattleProofSelected { Index = index });
            }
        }

        private void OnSkillChanged(BattleSkillSelected signal)
        {
            if (_selectedProofIndex >= 0)
            {
                _proofTexts[_selectedProofIndex].color = _defaultColor;
                _selectedProofIndex = -1;
            }

            foreach (var proof in _proofTexts)
            {
                var button = proof.GetComponent<Button>();
                var combination = proof.GetComponent<ProofView>().Model.Combination;
                button.interactable = signal.Skill == combination;
            }
        }

        private void OnBattleProofUsed(BattleProofUsed signal)
        {
            _proofTexts[signal.Index].gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<BattleSkillSelected>(OnSkillChanged);
            _signalBus.Unsubscribe<BattleProofUsed>(OnBattleProofUsed);
        }
    }
}
