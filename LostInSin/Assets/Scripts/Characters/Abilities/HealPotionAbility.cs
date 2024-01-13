namespace LostInSin.Characters.Abilities
{
    public class HealPotionAbility : IAbility
    {
        public bool CanCast()
        {
            //player can always use a potion //TODO change to use Action Points
            return true;
        }

        public void CastAbility(Character target)
        {
            throw new System.NotImplementedException();
        }
    }
}