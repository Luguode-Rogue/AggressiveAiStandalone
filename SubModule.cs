using TaleWorlds.MountAndBlade;

namespace AggressiveAi.Standalone
{
    public sealed class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            AggressiveAiBootstrap.Install();
        }

        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);
            AggressiveAiBootstrap.AddMissionBehavior(mission);
        }
    }
}
