using System;
using System.Threading;
using ReLogic.Content;
using Terraria;

namespace AllBeginningsMod.Utility;

public static class ThreadUtils
{
    public static bool IsMainThread => AssetRepository.IsMainThread;

    /// <summary>
    /// Queues the specified action to the main thread and blocks until its complete. If the current thread is the main thread, the action is invoked directly.
    /// </summary>
    /// <param name="action">The action to invoke.</param>
    /// <param name="cancellationToken">The cancellation token to observe.</param>
    /// <exception cref="AggregateException"></exception>
    public static void RunOnMainThread(Action action, CancellationToken cancellationToken = default) {
        cancellationToken.ThrowIfCancellationRequested();

        if (IsMainThread) {
            action();
            return;
        }

        ManualResetEventSlim manualResetEvent = new(false);
        Exception error = null;

        Main.QueueMainThreadAction(
            () => {
                try {
                    if (!cancellationToken.IsCancellationRequested)
                        action();
                }
                catch (Exception exception) {
                    error = exception;
                }
                finally {
                    manualResetEvent.Set();
                }
            }
        );

        manualResetEvent.Wait(cancellationToken);

        if (error != null)
            throw new AggregateException(error);
    }
}