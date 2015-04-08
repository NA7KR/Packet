using System;
using System.Threading;

namespace Renci.SshNet
{
    public partial class KeyboardInteractiveAuthenticationMethod : AuthenticationMethod
    {
        /// <exception cref="ArgumentNullException"><paramref name="action" /> is null.</exception>
        partial void ExecuteThread(Action action)
        {
            ThreadPool.QueueUserWorkItem(o => { action(); });
        }
    }
}