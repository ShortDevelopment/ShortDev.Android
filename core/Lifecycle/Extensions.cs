using AndroidX.Lifecycle;
using AndroidLifecycle = AndroidX.Lifecycle.Lifecycle;
using LifecycleState = AndroidX.Lifecycle.Lifecycle.State;

namespace ShortDev.Android.Lifecycle;

public static class Extensions
{
    extension(AndroidLifecycle lifecycle)
    {
        /// <summary>
        /// Indicates whether the <see cref="AndroidLifecycle" /> has reached at least the <see cref="LifecycleState.Started" /> state.
        /// </summary>
        /// <seealso cref="LifecycleState.IsAtLeast(LifecycleState)"/>
        public bool IsAtLeastStarted => lifecycle.CurrentState.IsAtLeast(LifecycleState.Started!);

        /// <summary>
        /// Indicates whether the <see cref="AndroidLifecycle" /> has reached at least the <see cref="LifecycleState.Resumed" /> state.
        /// </summary>
        /// <seealso cref="LifecycleState.IsAtLeast(LifecycleState)"/>
        public bool IsAtLeastResumed => lifecycle.CurrentState.IsAtLeast(LifecycleState.Resumed!);

        /// <summary>
        /// Creates an awaiter that completes when the <see cref="AndroidLifecycle" /> reaches the specified target <see cref="LifecycleState" />.
        /// </summary>
        /// <param name="targetState">The lifecycle state to wait for.</param>
        /// <returns>A <see cref="LifecycleAwaiter"/> that completes when the lifecycle reaches the specified state.</returns>
        public LifecycleAwaiter WaitUntil(LifecycleState targetState)
            => new(lifecycle, targetState);

        /// <summary>
        /// Creates an awaiter that completes when the <see cref="AndroidLifecycle" /> reaches the <see cref="LifecycleState.Started" /> state.
        /// </summary>
        /// <returns>A <see cref="LifecycleAwaiter"/> that completes when the lifecycle reaches the started state.</returns>
        public LifecycleAwaiter WaitUntilStarted()
            => new(lifecycle, LifecycleState.Started!);

        /// <summary>
        /// Creates an awaiter that completes when the <see cref="AndroidLifecycle" /> reaches the <see cref="LifecycleState.Resumed" /> state.
        /// </summary>
        /// <returns>A <see cref="LifecycleAwaiter" /> that completes when the lifecycle reaches the resumed state.</returns>
        public LifecycleAwaiter WaitUntilResumed()
            => new(lifecycle, LifecycleState.Resumed!);
    }

    extension(ILifecycleOwner owner)
    {
        /// <inheritdoc cref="get_IsAtLeastStarted(AndroidLifecycle)" />
        public bool IsAtLeastStarted => owner.Lifecycle.IsAtLeastStarted;

        /// <inheritdoc cref="get_IsAtLeastResumed(AndroidLifecycle)" />
        public bool IsAtLeastResumed => owner.Lifecycle.IsAtLeastResumed;
    }
}
