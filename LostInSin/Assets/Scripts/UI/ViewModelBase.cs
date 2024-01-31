using System;
using UniRx;
using Zenject;

namespace LostInSin.UI
{
    public class ViewModelBase : IDisposable
    {
        [Inject] protected readonly SignalBus _signalBus;
        protected readonly CompositeDisposable _disposables = new();

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}