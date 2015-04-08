﻿using System;
using System.Threading;

namespace Renci.SshNet.Common
{
    /// <summary>
    ///     Light implementation of SemaphoreSlim.
    /// </summary>
    public class SemaphoreLight
    {
        private readonly object _lock = new object();

        /// <summary>
        ///     Initializes a new instance of the <see cref="SemaphoreLight" /> class, specifying
        ///     the initial number of requests that can be granted concurrently.
        /// </summary>
        /// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initialCount" /> is a negative number.</exception>
        public SemaphoreLight(int initialCount)
        {
            if (initialCount < 0)
                throw new ArgumentOutOfRangeException("The initial argument is negative");

            CurrentCount = initialCount;
        }

        /// <summary>
        ///     Gets the current count of the <see cref="SemaphoreLight" />.
        /// </summary>
        public int CurrentCount { get; private set; }

        /// <summary>
        ///     Exits the <see cref="SemaphoreLight" /> once.
        /// </summary>
        /// <returns>The previous count of the <see cref="SemaphoreLight" />.</returns>
        public int Release()
        {
            return Release(1);
        }

        /// <summary>
        ///     Exits the <see cref="SemaphoreLight" /> a specified number of times.
        /// </summary>
        /// <param name="releaseCount">The number of times to exit the semaphore.</param>
        /// <returns>The previous count of the <see cref="SemaphoreLight" />.</returns>
        public int Release(int releaseCount)
        {
            var oldCount = CurrentCount;

            lock (_lock)
            {
                CurrentCount += releaseCount;

                Monitor.Pulse(_lock);
            }

            return oldCount;
        }

        /// <summary>
        ///     Blocks the current thread until it can enter the <see cref="SemaphoreLight" />.
        /// </summary>
        public void Wait()
        {
            lock (_lock)
            {
                while (CurrentCount < 1)
                {
                    Monitor.Wait(_lock);
                }

                CurrentCount--;

                Monitor.Pulse(_lock);
            }
        }
    }
}