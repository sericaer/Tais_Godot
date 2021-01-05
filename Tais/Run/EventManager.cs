using System.ComponentModel;
using Tais.API;

namespace Tais.Run
{
    interface IEventManager : INotifyPropertyChanged
    {
        IEvent currEvent { get; }
    }

    class EventManager : IEventManager
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public IEvent currEvent { get; }
    }
}