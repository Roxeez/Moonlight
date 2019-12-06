using System;

namespace NtCore.API.Scheduler
{
    public interface IScheduler
    {
        void Schedule(TimeSpan delay, Action action);
    }
}