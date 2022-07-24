using ReLogic.Content;
using System;
using System.Threading;
using System.Threading.Tasks;
using Terraria;

namespace AllBeginningsMod.Utility
{
    public static class ThreadUtils
    {
        public static bool IsMainThread => AssetRepository.IsMainThread;

        public static void RunOnMainThread(Action action, CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();

            if (IsMainThread) {
                action();
                return;
            }

            using ManualResetEventSlim manualresetevent = new(false);
            Exception error = null;

            Main.QueueMainThreadAction(() => {
                try {
                    if (!cancellationToken.IsCancellationRequested) {
                        action();
                    }
                }
                catch (Exception exception) {
                    error = exception;
                }
                finally {
                    manualresetevent.Set();
                }
            });

            manualresetevent.Wait(cancellationToken);

            if (error != null) {
                throw new AggregateException(new Exception[] { 
                    error 
                });
            }
        }

        public static Task RunOnMainThreadAsync(Action action, CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();

            if (action == null) {
                throw new ArgumentNullException(nameof(action));
            }

            if (IsMainThread) {
                if (cancellationToken.IsCancellationRequested) {
                    return Task.FromCanceled(cancellationToken);
                }
                try {
                    action();
                }
                catch (Exception exception) {
                    return Task.FromException(exception);
                }
                return Task.CompletedTask;
            }

            TaskCompletionSource source = new();

            Main.QueueMainThreadAction(delegate {
                if (cancellationToken.IsCancellationRequested) {
                    source.SetCanceled(cancellationToken);
                }
                try {
                    action();
                }
                catch (Exception exception) {
                    source.SetException(exception);
                }
                finally {
                    source.SetResult();
                }
            });
            return source.Task;
        }
    }
}
