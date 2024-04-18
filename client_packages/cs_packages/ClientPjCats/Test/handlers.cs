using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientPjCats.Test
{
    public class handlers
    {
        public static void BindKey()
        {
            Input.Bind(RAGE.Ui.VirtualKeys.F6, true, onPressF6);
        }
        private static void onPressF6()
        {
            Chat.Output("press F6");
        }
    }
}
