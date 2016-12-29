﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricDownload.util
{
    public class Constant
    {
        //--- GetWindowLong
        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
        //--- Window Style
        public const int WS_EX_CONTEXTHELP = 0x00400;
        public const int WS_MAXIMIZEBOX = 0x10000;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_SYSMENU = 0x80000;
        //--- Window Message
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSCOMMAND = 0x0112;
        //--- System Command
        public const int SC_CONTEXTHELP = 0xF180;
        public const int SC_CLOSE = 0xF060;
        //--- Virtual Keyboard
        public const int VK_F4 = 0x73;
        //--- Constructor
        private Constant() { }
    }
}
