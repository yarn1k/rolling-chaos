using Core.Infrastructure.Signals.Battle;
using Core.Proof;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Battle
{
    public class SkillSelection : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [SerializeField]
        private Color _selectedColor;
        [SerializeField]
        private Color _defaultColor;
        [SerializeField]
        private TMP_Text[] _skillTexts;
        private int _selectedSkillIndex = -1;

        private void Start()
        {
            //skillTexts = GetComponentsInChildren<TMP_Text>();

            // ������������� ����������� ���� ��� ���� �������
            foreach (TMP_Text skillText in _skillTexts)
            {
                skillText.color = _defaultColor;

                // ��������� ���������� ������� ����� ��� ������� ������
                skillText.GetComponent<Button>().onClick.AddListener(() =>
                {
                    int index = System.Array.IndexOf(_skillTexts, skillText);
                    SelectSkill(index);
                });
            }
        }

        private void SelectSkill(int index)
        {
            // ���������, ��� ������ ������ ��������� � �������� �������
            if (index >= 0 && index < _skillTexts.Length)
            {
                // ������� ��������� � ����������� ���������� ������
                if (_selectedSkillIndex >= 0)
                {
                    _skillTexts[_selectedSkillIndex].color = _defaultColor;
                }

                // �������� ��������� �����
                _skillTexts[index].color = _selectedColor;
                _selectedSkillIndex = index;

                var skill = GetCombination(_skillTexts[index].text);
                _signalBus.Fire(new BattleSkillSelected { Skill = skill });
            }
        }

        private Combination GetCombination(string skillName)
        {
            return skillName switch
            {
                "Persuasion" => Combination.Persuasion,
                "Intimidation" => Combination.Intimidation,
                "Deception" => Combination.Deception,
                _ => throw new System.Exception("pizda"),
            };
        }
    }
}