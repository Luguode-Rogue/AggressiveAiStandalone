using System;
using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace AggressiveAi
{
    /// <summary>可由任意模组入口调用；不依赖 New_ZZZF 的技能或战斗模型。</summary>
    public static class AggressiveAiBootstrap
    {
        private const string HarmonyId = "AggressiveAi.AgentProperties";
        private static bool _installed;

        public static void Install()
        {
            if (_installed)
                return;

            var method = AccessTools.Method(typeof(Agent), nameof(Agent.UpdateAgentProperties));
            if (method == null)
                throw new MissingMethodException(typeof(Agent).FullName,
                    nameof(Agent.UpdateAgentProperties));

            var harmony = new Harmony(HarmonyId);
            harmony.Patch(method, postfix: new HarmonyMethod(
                typeof(AggressiveAiBootstrap), nameof(AfterAgentPropertiesUpdated)));
            _installed = true;
        }

        public static void AddMissionBehavior(Mission mission)
        {
            mission?.AddMissionBehavior(new AggressiveAiMissionLogic());
        }

        private static void AfterAgentPropertiesUpdated(Agent __instance)
        {
            // 原生 UpdateAgentProperties 已计算并写入属性。调整后只同步驱动属性，
            // 不再次调用 UpdateAgentProperties，以免递归重算战斗模型。
            if (AiDefenseThreatAdjustment.Apply(__instance, __instance.AgentDrivenProperties))
                __instance.UpdateCustomDrivenProperties();
        }
    }
}
