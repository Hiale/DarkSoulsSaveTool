using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hiale.DarkSoulsSaveTool
{
    //http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx
    public class KeyboardHook
    {
        // ReSharper disable InconsistentNaming
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        // ReSharper restore InconsistentNaming
        private static readonly Win32.LowLevelKeyboardProc Proc = HookCallback;
        private static IntPtr _hookId = IntPtr.Zero;

        private static readonly Dictionary<Keys, Action<Keys>> Dict = new Dictionary<Keys, Action<Keys>>();

        public static void Hook()
        {
            _hookId = SetHook(Proc);
        }

        public static void Unhook()
        {
            Win32.UnhookWindowsHookEx(_hookId);
            _hookId = IntPtr.Zero;
        }

        public static void RegisterKey(Keys key, Action<Keys> action)
        {
            if (_hookId == IntPtr.Zero)
                Hook();
            if (!Dict.ContainsKey(key))
                Dict.Add(key, action);
        }

        public static void UnregisterKey(Keys key)
        {
            Dict.Remove(key);
        }

        private static IntPtr SetHook(Win32.LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return Win32.SetWindowsHookEx(WH_KEYBOARD_LL, proc, Win32.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                var vkCode = Marshal.ReadInt32(lParam);
                var key = (Keys) vkCode;
                if (Control.ModifierKeys == Keys.Control)
                {
                    Debug.WriteLine("Control pressed " + key);
                    
                }
                if (Dict.ContainsKey(key))
                    Dict[key].Invoke(key);
            }
            return Win32.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}
