using Core.NPC;
using Core.Proof;

namespace Core.Infrastructure.Signals.Battle
{
    public struct BattleSkillSelected
    {
        public Combination Skill;
    }
    public struct BattleProofSelected
    {
        public int Index;
    }
    public struct BattleLogMessage
    {
        public string Message;
        public string Sender;
    }
    public struct BattleProofUsed
    {
        public int Index;
    };
}
