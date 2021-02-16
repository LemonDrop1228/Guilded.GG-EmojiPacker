using EmojiPacker.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmojiPacker.TestHelpers
{
    public static class CreateTestPack
    {
        public static EmojiPackWrapper GetNewPackWrapper()
        {
            return new EmojiPackWrapper(GetNewPack());
        }

        public static EmojiPack GetNewPack()
        {
            return new EmojiPack() {
                Name = "Test Pack 001",
                Emotes = new ObservableCollection<EmojiDefinition>() {
                    { new EmojiDefinition(){ Name = "EscapefromTarkov", Url = "https://emoji.gg/assets/emoji/7695_EscapefromTarkov.png"} },
                    { new EmojiDefinition(){ Name = "Samurai", Url = "https://emoji.gg/assets/emoji/2458_Samurai.png"} },
                    { new EmojiDefinition(){ Name = "FigureJackboxGames", Url = "https://emoji.gg/assets/emoji/1317_FigureJackboxGames.png"} },
                }
            };
        }
    }
}
