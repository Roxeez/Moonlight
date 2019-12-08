using System;

namespace NtCore.API.Scheduler
{
    /// <summary>
    ///     Represent a scheduler used for delaying action
    /// </summary>
    public interface IScheduler
    {
        /// <summary>
        ///     Execute an action after defined time
        /// </summary>
        /// <param name="delay">Time before executing action</param>
        /// <param name="action">Action to execute</param>
        void Schedule(TimeSpan delay, Action action);
    }
}