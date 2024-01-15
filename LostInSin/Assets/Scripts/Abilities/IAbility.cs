using LostInSin.Characters;

namespace LostInSin.Abilities
{
    public interface IAbility
    {
        public bool CanCast();
        public void CastAbility(Character target);
    }
}