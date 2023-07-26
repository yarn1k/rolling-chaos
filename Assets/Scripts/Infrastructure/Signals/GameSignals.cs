using Core.NPC;
using Core.Player;
using Core.Proof;
using Core.Quest;
using System.Collections.Generic;

namespace Core.Infrastructure.Signals.Game
{
    public struct CheckPossibilityOfBattle
    {
        public NPCModel npc;
    }
    public struct BattleLoadScene {}
    public struct BattleInitialize
    {
        public PlayerController Player;
        public NPCModel npc;
        public List<ProofModel> Proofs;
    }
    public struct PlayerCollectedProof
    {
        public ProofModel Proof;
    };
}