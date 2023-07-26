using Core.NPC;
using System;
using UnityEngine;
using Zenject;

namespace Core.Models
{
    [Serializable]
    public class GameSettings
    {
        public Vector3 OffsetCamera;
        [Range(1f, 10f)]
        public float SmoothSpeedCamera;
    }

    [Serializable]
    public class PlayerSettings
    {
        [Header("Base Parameters")]
        public string Name;
        [Range(18f, 100f)]
        public byte Age;
        public Gender Gender;
        public string AppearanceDescription;
        public string ShortBiography;
        public string PersonalityTraits;
        public string Goals;
        [Space]
        [Header("Game Parameters")]
        public Sprite Portrait;
        [Range(1f, 10f)]
        public float MovementSpeed;
        [Range(0f, 4f)]
        public byte Persuasion;
        [Range(0f, 4f)]
        public byte Intimidation;
        [Range(0f, 4f)]
        public byte Deception;
        [Range(0f, 4f)]
        public byte Insight;
    }

    [CreateAssetMenu(menuName = "Configuration/Settings/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private GameSettings _gameSettings;
        [SerializeField]
        private PlayerSettings _playerSettings;

        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromInstance(_gameSettings).IfNotBound();
            Container.Bind<PlayerSettings>().FromInstance(_playerSettings).IfNotBound();
        }
    }
}
