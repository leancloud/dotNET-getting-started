using System;
using System.Collections.Generic;
using LeanCloud;
using LeanCloud.Core.Internal;
using LeanCloud.Engine;
using LeanCloud.Storage.Internal;
using Microsoft.AspNetCore.Hosting;

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
            var netEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var leanEnv = Environment.GetEnvironmentVariable("LEANCLOUD_APP_PROD");
            var isLeanProd = "1".Equals(leanEnv);
            var isNetProd = EnvironmentName.Production.Equals(netEnv);

            Console.WriteLine($"leanEnv:{leanEnv};isLeanProd:{isLeanProd};netEnv:{netEnv};isNetProd:{isNetProd};isProd:{isLeanProd || isNetProd}");


            AVClient.Initialize(configuration);

            Cloud cloud = new Cloud().SetHooks().UseLog();
            cloud.Start(args);
        }
    }
}
