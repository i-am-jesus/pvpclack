// Decompiled with JetBrains decompiler
// Type: CMD.MouseHook
// Assembly: CⅯD, Version=10.0.16299.15, Culture=neutral, PublicKeyToken=null
// MVID: A4F1CAA0-B7FE-4C8D-83A4-D166C09344D1
// Assembly location: C:\Python34\pycmd.exe

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CMD
{
  public static class MouseHook
  {
    private static MouseHook.LowLevelMouseProc _proc = new MouseHook.LowLevelMouseProc(MouseHook.HookCallbackMouse);
    private static IntPtr _hookID = IntPtr.Zero;
    public static bool isLeftDown = false;
    public static ArrayList nigger = new ArrayList();
    private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private const int WH_MOUSE_LL = 14;

    public static void Start()
    {
      MouseHook._hookID = MouseHook.SetMouseHook(MouseHook._proc);
    }

    public static void stop()
    {
      MouseHook.UnhookWindowsHookEx(MouseHook._hookID);
    }

    private static IntPtr SetMouseHook(MouseHook.LowLevelMouseProc proc)
    {
      using (Process currentProcess = Process.GetCurrentProcess())
      {
        using (ProcessModule mainModule = currentProcess.MainModule)
          return MouseHook.SetWindowsHookEx(14, proc, MouseHook.GetModuleHandle(mainModule.ModuleName), 0U);
      }
    }

    private static IntPtr HookCallbackMouse(int nCode, IntPtr wParam, IntPtr lParam)
    {
      if (!MouseHook.HandleMouseInput((MouseHook.MSLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof (MouseHook.MSLLHOOKSTRUCT)), (MouseHook.MouseMessages) (int) wParam))
        return new IntPtr(1);
      return MouseHook.CallNextHookEx(MouseHook._hookID, nCode, wParam, lParam);
    }

    public static IntPtr MakeLParam(int LoWord, int HiWord)
    {
      return (IntPtr) (HiWord << 16 | LoWord & (int) ushort.MaxValue);
    }

    public static IntPtr GetMinecraftHandle()
    {
      foreach (Process process in Process.GetProcessesByName("javaw"))
      {
        if (process.MainWindowTitle.Contains("Minecraft"))
          return process.MainWindowHandle;
      }
      return IntPtr.Zero;
    }

    private static bool HandleMouseInput(MouseHook.MSLLHOOKSTRUCT strct, MouseHook.MouseMessages mm)
    {
      if (strct.flags != 0U || !Program.isToggled || !(a.minecraft != IntPtr.Zero))
        return true;
      switch (mm)
      {
        case MouseHook.MouseMessages.WM_LBUTTONDOWN:
          MouseHook.isLeftDown = true;
          MouseHook.PostMessage(a.minecraft, 513U, 0, 0);
          break;
        case MouseHook.MouseMessages.WM_LBUTTONUP:
          MouseHook.isLeftDown = false;
          MouseHook.PostMessage(a.minecraft, 514U, 0, 0);
          break;
        case MouseHook.MouseMessages.WM_RBUTTONDOWN:
          MouseHook.PostMessage(a.minecraft, 516U, 0, 0);
          break;
        case MouseHook.MouseMessages.WM_RBUTTONUP:
          MouseHook.PostMessage(a.minecraft, 517U, 0, 0);
          break;
        default:
          return true;
      }
      return false;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, MouseHook.LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, MouseHook.LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    public static long CurrentTimeMillis()
    {
      return (long) (DateTime.UtcNow - MouseHook.Jan1st1970).TotalMilliseconds;
    }

    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private struct POINT
    {
      public int x;
      public int y;
    }

    private struct MSLLHOOKSTRUCT
    {
      public MouseHook.POINT pt;
      public uint mouseData;
      public uint flags;
      public uint time;
      public IntPtr dwExtraInfo;
    }

    private enum MouseMessages
    {
      WM_MOUSEMOVE = 512, // 0x00000200
      WM_LBUTTONDOWN = 513, // 0x00000201
      WM_LBUTTONUP = 514, // 0x00000202
      WM_RBUTTONDOWN = 516, // 0x00000204
      WM_RBUTTONUP = 517, // 0x00000205
      WM_MOUSEWHEEL = 522, // 0x0000020A
    }

    private enum GetWindow_Cmd : uint
    {
      GW_HWNDFIRST,
      GW_HWNDLAST,
      GW_HWNDNEXT,
      GW_HWNDPREV,
      GW_OWNER,
      GW_CHILD,
      GW_ENABLEDPOPUP,
    }

    private enum FakeMessage
    {
      WM_MOUSEMOVE = 512, // 0x00000200
      WM_LBUTTONDOWN = 513, // 0x00000201
      WM_LBUTTONUP = 514, // 0x00000202
      WM_RBUTTONDOWN = 516, // 0x00000204
      WM_RBUTTONUP = 517, // 0x00000205
      WM_MOUSEWHEEL = 522, // 0x0000020A
    }
  }
}
