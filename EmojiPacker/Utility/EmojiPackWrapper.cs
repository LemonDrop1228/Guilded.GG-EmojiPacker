using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmojiPacker.Utility
{
    public class EmojiPackWrapper
    {
        private EmojiPack emojiPack;

        public string Name
        {
            get
            {
                return emojiPack.ToString();
            }
            set
            {
                emojiPack.Name = value;
            }
        }

        public string Count
        {
            get
            {
                return emojiPack?.Emotes?.Count.ToString() ?? 0.ToString();
            }
        }

        public ObservableCollection<EmojiDefinition> Emojis => emojiPack.Emotes;

        public EmojiPackWrapper(EmojiPack pack)
        {
            emojiPack = pack;
        }

        public EmojiPackWrapper()
        {
            emojiPack = new EmojiPack();
        }

        public string GetPackJson()
        {
            return JsonConvert.SerializeObject(emojiPack);
        }
    }

    public class EmojiPack : INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<EmojiDefinition> emotes;

        [JsonProperty("name")]
        public string Name { get => name; set => SetField(ref name, value, "Name"); }
        [JsonProperty("author")]
        public string Author => "EmojiPacker";
        [JsonProperty("emotes")]
        public ObservableCollection<EmojiDefinition> Emotes { get => emotes ?? (emotes = new ObservableCollection<EmojiDefinition>()); set => SetField(ref emotes, value, "Emotes"); }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? $"Unamed Pack {DateTime.Now.ToString("MM.dd.yyyy")}" : Name;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion


    }

    public class EmojiDefinition : INotifyPropertyChanged
    {
        private string name;
        private string url;

        [JsonProperty("name")]
        public string Name { get => name; set => SetField(ref name, value, "Name"); }

        [JsonProperty("url")]
        public string Url { get => url; set => SetField(ref url, value, "Url"); }


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            if (propertyName == "Url")
                Name = ProposeName();
            OnPropertyChanged(propertyName);
            return true;
        }

        private string ProposeName()
        {
            var res = string.Empty;

            if (string.IsNullOrEmpty(Name))
            {
                if (Url.StartsWith("https://emoji.gg/assets/emoji/"))
                {
                    try
                    {
                        var emojiMeta = Url.Replace("https://emoji.gg/assets/emoji/", string.Empty);
                        var cleanEmojiMeta = emojiMeta.Remove(0, emojiMeta.Split('_')[0].Length + 1);
                        var name = Path.GetFileNameWithoutExtension(cleanEmojiMeta);
                        res = name;
                    }
                    catch (Exception) { }
                }
            }
            else
                return Name;

            return res;

        }
        #endregion

    }
}
