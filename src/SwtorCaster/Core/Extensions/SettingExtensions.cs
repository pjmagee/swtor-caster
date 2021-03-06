namespace SwtorCaster.Core
{
    using Domain.Log;
    using Domain.Settings;

    public static class SettingExtensions
    {
        public static bool CanPlay(this EventSetting setting, CombatLogEvent line)
        {
            return setting.IsPlayerDeath(line) ||
                   setting.IsPlayerKill(line) ||
                   setting.IsEnterCombat(line) ||
                   setting.IsExitCombat(line) ||
                   setting.IsAbilityActivate(line) ||
                   setting.IsAbilityCancel(line) || 
                   setting.IsPlayerSurrender(line);
        }

        public static bool IsPlayerSurrender(this EventSetting setting, CombatLogEvent line)
        {
            return line.IsSurrender();
        }

        public static bool IsAbilityActivate(this EventSetting setting, CombatLogEvent line)
        {
            return setting.EffectName == SoundEvent.AbilityActivate && line.IsAbilityActivate()
                && setting.AbilityId == line.Ability.EntityId.ToString();
        }

        public static bool IsEnterCombat(this EventSetting setting, CombatLogEvent line)
        {
            return setting.EffectName == SoundEvent.EnterCombat && line.IsEnterCombat();
        }

        public static bool IsExitCombat(this EventSetting setting, CombatLogEvent line)
        {
            return setting.EffectName == SoundEvent.ExitCombat && line.IsExitCombat();
        }

        public static bool IsPlayerDeath(this EventSetting setting, CombatLogEvent line)
        {
            return setting.EffectName == SoundEvent.Death && line.IsThisPlayerDeath();
        }

        public static bool IsPlayerKill(this EventSetting setting, CombatLogEvent line)
        {
            return setting.EffectName == SoundEvent.Kill && (line.IsPlayerKill() || line.IsNpcKill());
        }

        public static bool IsAbilityCancel(this EventSetting setting, CombatLogEvent line)
        {
            return setting.EffectName == SoundEvent.AbilityCancel &&
                   line.IsAbilityCancel() && line.IsThisPlayer() &&
                   line.IsAbility(setting.AbilityId);
        }
    }
}