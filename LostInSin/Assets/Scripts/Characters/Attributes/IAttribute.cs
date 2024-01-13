namespace LostInSin.Characters.Attributes
{
    public interface IAttribute
    {
        public float MaxValue { get; }
        public float CurrentValue { get; }

        public void SetValue(float value);
        public void AddToValue(float change);
        public float GetValue();
    }
}