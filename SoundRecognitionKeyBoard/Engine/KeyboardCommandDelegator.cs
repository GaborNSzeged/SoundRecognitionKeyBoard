using System.Runtime.InteropServices;

namespace SoundRecognitionKeyBoard.Engine
{
    internal static class KeyboardCommandDelegator
    {
        // https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes

        // [in] bVk A virtual-key code. The code must be a value in the range 1 to 254.
        // [in] bScan A hardware scan code for the key.
        // [in] dwFlags Controls various aspects of function operation. This parameter can be one or more of the following values.
        // [in] dwExtraInfo Type: ULONG_PTR An additional value associated with the key stroke.
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int KEYEVENTF_EXTENDEDKEY = 0;

        // 0x0001 If specified, the scan code was preceded by a prefix byte having the value 0xE0 (224).
        //private const int KEYEVENTF_EXTENDEDKEY = 1;

        // 0x0002 If specified, the key is being released. If not specified, the key is being depressed.
        private const int KEYEVENTF_KEYUP = 2;

        public static void KeyDown(byte vKey)
        {
            // keybd_event(vKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(vKey, 0, 0, 0);
        }

        public static void KeyUp(byte vKey)
        {
            // keybd_event(vKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            keybd_event(vKey, 0, KEYEVENTF_KEYUP, 0);
        }
    }
}
