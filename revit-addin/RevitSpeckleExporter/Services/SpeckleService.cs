#region System Namespaces
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#endregion

#region Speckle Namespaces
using Speckle.Core.Api;
using Speckle.Core.Api.GraphQL.Models;
using Speckle.Core.Credentials;
using Speckle.Core.Models;
using Speckle.Core.Transports;
#endregion

namespace RevitSpeckleExporter.Services
{
    internal class SpeckleService
    {
        private readonly string serverUrl;
        private readonly string streamId;
        private readonly string token;

        internal SpeckleService(string serverUrl, string streamId, string token)
        {
            this.serverUrl = serverUrl;
            this.streamId = streamId;
            this.token = token;
        }

        [System.Obsolete]
        internal async Task<string> SendAsync(Base root, string message = "Sent from Revit")
        {
            Account account = new Account
            {
                token = token,
                serverInfo = new ServerInfo { url = serverUrl }
            };

            ServerTransport transport = new ServerTransport(account, streamId);

            string objectId = await Operations.Send(
                root,
                CancellationToken.None,
                new List<ITransport> { transport },
                disposeTransports: true);

            Client client = new Client(account);

            string commitId = await client.CommitCreate(new CommitCreateInput
            {
                streamId = streamId,
                branchName = "main",
                objectId = objectId,
                message = message,
                sourceApplication = "Revit"
            });

            return commitId;
        }
    }
}
