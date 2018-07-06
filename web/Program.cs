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
            Cloud cloud = new Cloud().SetHooks().UseLog();
            cloud.Start(args);
        }
    }
}
