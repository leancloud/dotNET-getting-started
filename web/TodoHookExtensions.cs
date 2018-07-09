using System;
using System.Threading.Tasks;
using LeanCloud;
using LeanCloud.Core.Internal;
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
            }).BeforeSave("Review", review =>
            {
                var comment = review.Get<string>("comment");
                if (comment.Length > 140) review["comment"] = comment.Substring(0, 137) + "...";
                return Task.FromResult(comment);
            }).AfterSave("Review", async review =>
            {
                var post = review.Get<AVObject>("post");
                await post.FetchAsync();
                post.Increment("comments");
                await post.SaveAsync();
            }).AfterSave("", async user =>
            {
                if (user is AVUser avUser)
                {
                    avUser.Set("from", "LeanCloud");
                    await avUser.SaveAsync();
                }
            }).BeforeUpdate("Todo", (EngineObjectHookContext context) =>
             {
                 return Task.FromResult("haha");
             });
            return cloud;
        }
    }
}
