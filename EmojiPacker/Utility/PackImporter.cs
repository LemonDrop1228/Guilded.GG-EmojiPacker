using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmojiPacker.Utility;
using Newtonsoft.Json;

namespace EmojiPacker.Utility
{
    public static class PackImporter
    {
        public static EmojiPackWrapper Import(string jsonData)
        {
            var pack = JsonConvert.DeserializeObject<EmojiPack>(jsonData);
            var wrapper = new EmojiPackWrapper(pack);
            return wrapper;
        }
    }
}
