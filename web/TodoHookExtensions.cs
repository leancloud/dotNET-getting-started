using System;
using System.Threading.Tasks;
using LeanCloud.Engine;

namespace web
{
    public static class TodoHookExtensions
    {
        public static Cloud SetHooks(this Cloud cloud)
        {
            cloud.BeforeSave("Todo", todo =>
            {
                var title = todo.Get<string>("title");
                // reset value for title
                if (!string.IsNullOrEmpty(title))
                    if (title.Length > 20) todo["title"] = title.Substring(0, 20);
                // returning any value will be ok.
                return Task.FromResult(true);
            });
            return cloud;
        }
    }
}
