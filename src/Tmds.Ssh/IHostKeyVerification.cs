// This file is part of Tmds.Ssh which is released under MIT.
// See file LICENSE for full license details.

using System.Threading;
using System.Threading.Tasks;

namespace Tmds.Ssh;

interface IHostKeyVerification
{
    ValueTask VerifyAsync(SshConnectionInfo connectionInfo, CancellationToken ct);
}
