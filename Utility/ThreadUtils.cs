using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AllBeginningsMod.Utility
{
    public static class ThreadUtils
    {
        public static bool IsMainThread => ReLogic.Content.AssetRepository.IsMainThread;

        /// <summary>
        /// Queues the specified action to the main thread and blocks until its complete.<br />
        /// If the current thread is the main thread, the action is invoked directly.
        /// </summary>
        /// <param name="action">The action to invoke</param>
        /// <param name="cancellationToken">The CancellationToken to observe</param>
        /// <exception cref="AggregateException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public static void RunOnMainThread(Action action, CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            if (IsMainThread) {
                action();
                return;
            }

            using (var manualresetevent = new ManualResetEventSlim(false)) {
                Exception error = null;
                Main.QueueMainThreadAction(() => {
                    try {
                        if (!cancellationToken.IsCancellationRequested) {
                            action();
                        }
                    }
                    catch (Exception e) {
                        error = e;
                    }
                    finally {
                        manualresetevent.Set();
                    }
                });
                manualresetevent.Wait(cancellationToken);
                if (error != null)
                    throw new AggregateException(new Exception[] { error });
            }
        }

        /// <summary>
        /// Queues the specified action to the main thread and returns a task that waits until its complete
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task RunOnMainThreadAsync(Action action, CancellationToken cancellationToken = default) {
            cancellationToken.ThrowIfCancellationRequested();
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (IsMainThread) {
                if (cancellationToken.IsCancellationRequested) {
                    return Task.FromCanceled(cancellationToken);
                }
                try {
                    action();
                }
                catch (Exception e) {
                    return Task.FromException(e);
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
                catch (Exception e) {
                    source.SetException(e);
                }
                finally {
                    source.SetResult();
                }
            });

            return source.Task;
        }
    }
}
