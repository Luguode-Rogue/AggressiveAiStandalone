using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace AggressiveAi
{
    internal sealed class AggressiveAiAgentComponent : AgentComponent
    {
        private const float RefreshInterval = 0.5f;
        private float _refreshTimer;

        public AggressiveAiAgentComponent(Agent agent) : base(agent)
        {
            // 错开大规模战场的刷新时刻。
            _refreshTimer = (agent.Index % 10) * (RefreshInterval / 10f);
        }

        public override void OnTick(float dt)
        {
            if (Agent == null || !Agent.IsActive())
                return;
            _refreshTimer -= dt;
            if (_refreshTimer > 0f)
                return;
            _refreshTimer += RefreshInterval;
            if (_refreshTimer <= 0f)
                _refreshTimer = RefreshInterval;
            AiDefenseThreatAdjustment.RefreshForCurrentTarget(Agent);
        }

        public override void OnAIInputSet(ref Agent.EventControlFlag eventFlag,
            ref Agent.MovementControlFlag movementFlag, ref Vec2 inputVector)
        {
            AiDefenseThreatAdjustment.SuppressDefenseInput(Agent, ref movementFlag);
        }
    }
}
