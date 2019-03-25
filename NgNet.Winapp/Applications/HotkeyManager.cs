using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NgNet.Applications
{
    public delegate void HotkeyCallBackHanlder();
    public class HotkeyManager
    {
        #region private fields
        // 区分不同的快捷键
        private int hotkeyId = 0;
        // 每一个key对于一个处理函数
        private Dictionary<int, HotkeyCallBackHanlder> hotkeysMap = new Dictionary<int, HotkeyCallBackHanlder>();
        // 快捷键注册到的句柄
        private IntPtr _hWnd;
        #endregion

        #region proptected fields

        #endregion

        #region public properties

        #endregion

        #region constructor
        public HotkeyManager(IntPtr hWnd)
        {
            _hWnd = hWnd;
        }
        #endregion

        #region private methods

        #endregion

        #region public methods
        // 注册快捷键
        public bool Register(int modifiers, Keys vk, HotkeyCallBackHanlder callBack)
        {
            int id = hotkeyId++;
            if (!Windows.Apis.User32.RegisterHotKey(_hWnd, id, modifiers, vk))
                return false;
            hotkeysMap[id] = callBack;
            return true;
        }

        // 注册快捷键
        public bool Register(ref Hotkey hotkey)
        {
            int id = hotkeyId++;
            if (!Windows.Apis.User32.RegisterHotKey(_hWnd, id, hotkey.Modifiers, hotkey.Key))
                return hotkey.Registered = false;
            hotkeysMap[id] = hotkey.CallBack;
            return hotkey.Registered = true;
        }

        // 注销快捷键
        public bool UnRegister(HotkeyCallBackHanlder callBack)
        {
            foreach (KeyValuePair<int, HotkeyCallBackHanlder> var in hotkeysMap)
            {
                if (var.Value == callBack)
                {
                    Windows.Apis.User32.UnregisterHotKey(_hWnd, var.Key);
                    hotkeysMap.Remove(var.Key);
                    return true;
                }
            }
            return false;
        }

        // 注销快捷键
        public bool UnRegister(ref Hotkey hotkey)
        {
            foreach (KeyValuePair<int, HotkeyCallBackHanlder> var in hotkeysMap)
            {
                if (var.Value == hotkey.CallBack)
                {
                    Windows.Apis.User32.UnregisterHotKey(_hWnd, var.Key);
                    hotkey.Registered = false;
                    hotkeysMap.Remove(var.Key);
                    return true;
                }
            }
            return false;
        }

        // 快捷键消息处理
        public void ProcessHotkey(Message m)
        {
            if (m.Msg == 0x312)
            {
                int id = m.WParam.ToInt32();
                HotkeyCallBackHanlder callback;
                if (hotkeysMap.TryGetValue(id, out callback))
                    callback();
            }
        }
        #endregion
    }
}
