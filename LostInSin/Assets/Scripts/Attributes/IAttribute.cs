namespace LostInSin.Attributes
{
    public interface IAttribute
    {
        public float MaxValue { get; }
        public float CurrentValue { get; }

        public void SetValue(float value);
        public void SetMaxValue(float value);
        public void AddToValue(float change);
        public void AddToMaxValue(float change);
        public float GetValue();
        public float GetMaxValue();
    }
}