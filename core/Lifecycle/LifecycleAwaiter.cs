using AndroidX.Lifecycle;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AndroidLifecycle = AndroidX.Lifecycle.Lifecycle;
using LifecycleEvent = AndroidX.Lifecycle.Lifecycle.Event;
using LifecycleState = AndroidX.Lifecycle.Lifecycle.State;

namespace ShortDev.Android.Lifecycle;

/// <summary>
/// Provides an awaiter that completes when the specified Android <see cref="AndroidLifecycle"/> reaches or exceeds a target <see cref="LifecycleState"/>.
/// </summary>
/// <remarks>
/// <para>
/// Use this struct to asynchronously wait until an Android component's lifecycle reaches a particular state, such as <see cref="LifecycleState.Started"/> or <see cref="LifecycleState.Resumed"/>. <br/>
/// This is typically used with the <see langword="await" /> keyword to pause execution until the lifecycle advances to the desired state.
/// </para>
/// The awaiter will never complete if the lifecycle is already in the <see cref="LifecycleState.Destroyed"/> state.
/// </remarks>
/// <param name="lifecycle">The Android lifecycle to observe for state changes.</param>
/// <param name="targetState">The lifecycle state at which the awaiter will complete.</param>
/// <seealso cref="Extensions.WaitUntil(AndroidLifecycle, LifecycleState)"/>
public readonly struct LifecycleAwaiter(AndroidLifecycle lifecycle, LifecycleState targetState) : ICriticalNotifyCompletion
{
    readonly AndroidLifecycle _lifecycle = lifecycle;
    readonly LifecycleState _targetState = targetState;

    public bool IsCompleted => _lifecycle.CurrentState.IsAtLeast(_targetState);

    [StackTraceHidden]
    public void GetResult() { }

    public void OnCompleted(Action continuation) => UnsafeOnCompleted(continuation); // ToDo: Capture ExecutionContext
    public void UnsafeOnCompleted(Action continuation)
    {
        if (_lifecycle.CurrentState.Equals(LifecycleState.Destroyed))
            return;

        if (IsCompleted)
        {
            continuation();
            return;
        }

        _lifecycle.AddObserver(new Observer(_targetState, continuation));
    }

    public LifecycleAwaiter GetAwaiter() => this;

    sealed class Observer(LifecycleState targetState, Action continuation) : Java.Lang.Object, ILifecycleEventObserver
    {
        private readonly LifecycleState _targetState = targetState;
        private readonly Action _continuation = continuation;

        public void OnStateChanged(ILifecycleOwner source, LifecycleEvent e)
        {
            if (!source.Lifecycle.CurrentState.IsAtLeast(_targetState))
                return;

            source.Lifecycle.RemoveObserver(this);
            _continuation();
        }
    }
}
