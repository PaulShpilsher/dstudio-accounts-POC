namespace Accounts.Repositories.Utils
{
    ///
    /// Helper lock class to use with async/await
    /// https://gist.githubusercontent.com/borakasmer/8f2b05e511f776bda98937db5156930d/raw/bc80a030f34e59b9123c2e5880a75143cf640ed6/AsyncLockObject.cs
    ///
    public sealed class AsyncLock
    {
        private readonly SemaphoreSlim m_semaphore = new SemaphoreSlim(1, 1);
        private readonly Task<IDisposable> m_releaser;

        public AsyncLock()
        {
            m_releaser = Task.FromResult((IDisposable)new Releaser(this));
        }

        public Task<IDisposable> LockAsync()
        {
            var wait = m_semaphore.WaitAsync();
            return wait.IsCompleted ?
                        m_releaser :
                        wait.ContinueWith((_, state) => (IDisposable)state,
                                          m_releaser.Result,
                                          CancellationToken.None,
                                          TaskContinuationOptions.ExecuteSynchronously,
                                          TaskScheduler.Default);
        }

        private sealed class Releaser : IDisposable
        {
            private readonly AsyncLock m_toRelease;
            internal Releaser(AsyncLock toRelease) { m_toRelease = toRelease; }
            public void Dispose() { m_toRelease.m_semaphore.Release(); }
        }
    }
}
