using System;
using System.Linq;
using System.Threading.Tasks;
using LeanCloud;
using LeanCloud.Core.Internal;
using LeanCloud.Engine;
using System.Collections.Generic;

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
            }).AfterSave("_User", async user =>
            {
                if (user is AVUser avUser)
                {
                    avUser.Set("from", "LeanCloud");
                    await avUser.SaveAsync();
                }
            }).BeforeUpdate("Review", (EngineObjectHookContext context) =>
            {
                if (context.UpdatedKeys.Contains("comment"))
                {
                    var comment = context.TheObject.Get<string>("comment");
                    if (comment.Length > 140) throw new EngineException(400, "comment 长度不得超过 140 字符");
                }
                return Task.FromResult(true);
            }).AfterSave("Article", article =>
            {
                Console.WriteLine(article.ObjectId);
                return Task.FromResult(true);
            }).BeforeDelete("Album", async album =>
            {
                AVQuery<AVObject> query = new AVQuery<AVObject>("Photo");
                query.WhereEqualTo("album", album);
                int count = await query.CountAsync();
                if (count > 0)
                {
                    throw new Exception("无法删除非空相簿");
                }
                else
                {
                    Console.WriteLine("deleted.");
                }
            }).AfterDelete("Album", async album =>
            {
                AVQuery<AVObject> query = new AVQuery<AVObject>("Photo");
                query.WhereEqualTo("album", album);
                var result = await query.FindAsync();
                if (result != null && result.Count() != 0)
                {
                    await AVObject.DeleteAllAsync(result);
                }
            }).OnVerifiedSMS((AVUser user) =>
            {
                Console.WriteLine("user verified by sms");
                return Task.FromResult(true);
            }).OnLogIn(user =>
            {
                Console.WriteLine("user logged in.");
                return Task.FromResult(true);
            });
            return cloud;
        }
    }
}
