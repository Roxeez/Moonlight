using System;
using System.Reactive.Linq;

namespace NtCore.Scheduler
{
    public class ObservableScheduler : IScheduler
    {
        public void Schedule(TimeSpan delay, Action action)
        {
            Observable.Timer(delay).Subscribe(x => { action(); });
        }
    }
}