using EmojiPacker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EmojiPacker.Views
{
    public class BaseView : UserControl
    {
        public EmojiPackWrapper CurrentPack { get; set; }

        public virtual void CloseView()
        {

        }
    }
}
