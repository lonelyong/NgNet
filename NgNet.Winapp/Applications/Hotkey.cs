using System.Windows.Forms;

namespace NgNet.Applications
{
    public struct Hotkey
    {
        /// <summary>
        /// 快捷键名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 快捷键值
        /// </summary>
        public string Value
        {
            get
            {
                return $"{getValue(Modifiers)} + { Key}";
            }
        }
        /// <summary>
        /// 是否注册
        /// </summary>
        public bool Registered { get; set; }
        /// <summary>
        /// 快捷键成员
        /// </summary>
        public int Modifiers { get; set; }
        /// <summary>
        /// 附加键
        /// </summary>
        public Keys Key { get; set; }
        /// <summary>
        /// 回调委托
        /// </summary>
        public HotkeyCallBackHanlder CallBack;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="modifiers"></param>
        /// <param name="key"></param>
        /// <param name="callBack"></param>
        public Hotkey(string name, int modifiers, Keys key, HotkeyCallBackHanlder callBack)
        {
            Name = name;
            Modifiers = modifiers;
            Key = key;
            Registered = false;
            CallBack = callBack;

         
        }

        private string getValue(int modifiers)
        {
            #region get Value
            if (modifiers == 0)
                return null;
            else if (modifiers == 1)
                return "Alt";
            else if (modifiers == 2)
                return "Control";
            else if (modifiers == 3)
                return "Control + Alt";
            else if (modifiers == 5)
                return "Alt + Shift";
            else if (modifiers == 6)
                return "Control + Shift";
            else if (modifiers == 7)
                return "Control + Alt + Shift";
            else if (modifiers == 8)
                return "Win";
            else if (modifiers == 9)
                return "Win + Alt";
            else if (modifiers == 10)
                return "Win + Control";
            else if (modifiers == 11)
                return "Win + Control + Alt";
            else if (modifiers == 12)
                return "Win + Shift";
            else if (modifiers == 13)
                return "Win + Alt + Shift";
            else if (modifiers == 14)
                return "Win + Control + Shift";
            else
                return null;
            #endregion
        }
    }
}

