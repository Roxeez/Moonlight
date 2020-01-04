using NtCore.Clients;
using NtCore.Game.Battle;
using NtCore.Game.Entities;

namespace NtCore.Events.Battle
{
    public class EntityDamageEvent : Event
    {
        public ILivingEntity Target { get; }
        public ILivingEntity Caster { get; }
        public int Damage { get; }


        public EntityDamageEvent(IClient client, ILivingEntity target, ILivingEntity caster, int damage) : base(client)
        {
            Target = target;
            Caster = caster;
            Damage = damage;
        }
    }
}