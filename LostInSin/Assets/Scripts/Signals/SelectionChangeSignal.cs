namespace LostInSin.Signals
{
    public class SelectionChangeSignal : ISelectionChangeSignal
    {
        private readonly bool _selected;
        public bool Selected => _selected;

        public SelectionChangeSignal(bool selected)
        {
            _selected = selected;
        }
    }
}