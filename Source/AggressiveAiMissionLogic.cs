using TaleWorlds.MountAndBlade;

namespace AggressiveAi
{
    internal sealed class AggressiveAiMissionLogic : MissionLogic
    {
        public override void OnAgentCreated(Agent agent)
        {
            base.OnAgentCreated(agent);
            if (agent != null && agent.IsHuman &&
                agent.GetComponent<AggressiveAiAgentComponent>() == null)
                agent.AddComponent(new AggressiveAiAgentComponent(agent));
        }
    }
}
