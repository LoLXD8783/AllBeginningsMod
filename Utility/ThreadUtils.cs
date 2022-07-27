using System;
using System.Threading;
using ReLogic.Content;
using Terraria;

namespace AllBeginningsMod.Utility;

public static class ThreadUtils
{
    public static bool IsMainThread => AssetRepository.IsMainThread;

    public static void RunOnMainThread(Action action, CancellationToken cancellationToken = default) {
        cancellationToken.ThrowIfCancellationRequested();

        if (IsMainThread) {
            action();
            return;
        }

        ManualResetEventSlim manualResetEvent = new(false);
        Exception error = null;

        Main.QueueMainThreadAction(() => {
            try {
                if (!cancellationToken.IsCancellationRequested)
                    action();
            }
            catch (Exception exception) { error = exception; }
            finally { manualResetEvent.Set(); }
        });

        manualResetEvent.Wait(cancellationToken);

        if (error != null)
            throw new AggregateException(error);
    }
}