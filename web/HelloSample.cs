using System;
using LeanCloud.Engine;

namespace web
{
    public class HelloSample
    {
        [EngineFunction("Hello")]
        public static string Hello([EngineFunctionParameter("text")]string text)
        {
            return $"Hello, {text}";
        }
    }
}
