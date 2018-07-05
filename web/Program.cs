using System;
using System.Collections.Generic;
using LeanCloud;
using LeanCloud.Core.Internal;
using LeanCloud.Engine;
using LeanCloud.Storage.Internal;

namespace web
{
    class Program
    {
        static void Main(string[] args)
        {
            AVClient.Configuration configuration = new AVClient.Configuration()
            {
                ApplicationId = "315XFAYyIGPbd98vHPCBnLre-9Nh9j0Va",
                ApplicationKey = "Y04sM6TzhMSBmCMkwfI3FpHc",
                MasterKey = "Ep3IHWFqi41DMm44T49lKy07"
            };

            AVClient.Initialize(configuration);

            Cloud cloud = new Cloud().SetHooks().UseLog();
            cloud.Start(args);
        }
    }
}
