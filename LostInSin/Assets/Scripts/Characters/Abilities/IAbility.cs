namespace LostInSin.Characters.Abilities
{
    public interface IAbility
    {
        public bool CanCast();
        public void CastAbility(Character target);
    }
}