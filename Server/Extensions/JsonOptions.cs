using Newtonsoft.Json;
using System.Text.Json;

namespace ProgramPro.Server.Extensions
{
    public static class JsonOptions
    {
        public static JsonSerializerSettings jsonSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                };
            }
        }

        public static JsonSerializerOptions jsonOptions
        {
            get
            {
                return new JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                };
            }
        }
    }
}
