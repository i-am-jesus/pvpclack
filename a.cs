// Decompiled with JetBrains decompiler
// Type: CMD.a
// Assembly: CⅯD, Version=10.0.16299.15, Culture=neutral, PublicKeyToken=null
// MVID: A4F1CAA0-B7FE-4C8D-83A4-D166C09344D1
// Assembly location: C:\Python34\pycmd.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CMD
{
  public class a : Form
  {
    public static IntPtr minecraft = IntPtr.Zero;
    public Random rand = new Random();
    public int current = 2;
    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;
    private const int WM_MOUSEWHEEL = 522;
    private const int WM_MOUSEMOVE = 512;
    private const int WM_LBUTTONDOWN = 513;
    private const int WM_LBUTTONUP = 514;
    private const int WM_RBUTTONDOWN = 516;
    private const int WM_RBUTTONUP = 517;
    public bool wasDown;
    private IContainer components;
    private System.Windows.Forms.Timer timer;

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr windowHandle, int hotkeyId, uint modifierKeys, uint virtualKey);

    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr windowHandle, int hotkeyId);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public static IntPtr GetMinecraftHandle()
    {
      foreach (Process process in Process.GetProcessesByName("javaw"))
      {
        if (process.MainWindowTitle.Contains("Minecraft"))
          return process.MainWindowHandle;
      }
      return IntPtr.Zero;
    }

    public a()
    {
      this.InitializeComponent();
      int hotkeyId = 0;
      a.RegisterHotKey(this.Handle, hotkeyId, 0U, 45U);
      a.RegisterHotKey(this.Handle, hotkeyId + 1, 0U, 35U);
      this.timer.Start();
      MouseHook.Start();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      a.minecraft = a.GetMinecraftHandle();
      if (a.minecraft == IntPtr.Zero)
        return;
      if (Program.isToggled && MouseHook.isLeftDown)
      {
        if (this.current >= Program.list.Count - 2)
          this.current = 0;
        else
          ++this.current;
        string[] strArray = (string[]) Program.list[this.current];
        int down = Convert.ToInt32((double) int.Parse(strArray[0]) * Program.multNum);
        this.timer.Interval = Convert.ToInt32((double) int.Parse(strArray[1]) * Program.multNum) + down;
        new Thread((ThreadStart) (() => this.sendClick(down))).Start();
        this.wasDown = true;
      }
      else
      {
        if (!this.wasDown)
          return;
        this.timer.Interval = 40;
        this.wasDown = false;
        a.PostMessage(a.minecraft, 514U, 0, 0);
      }
    }

    public void sendClick(int down)
    {
      a.PostMessage(a.minecraft, 514U, 0, 0);
      Thread.Sleep(down);
      if (!MouseHook.isLeftDown)
        return;
      a.PostMessage(a.minecraft, 513U, 0, 0);
    }

    private void a_Paint(object sender, PaintEventArgs e)
    {
      this.Hide();
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 786)
        return;
      int num1 = (int) m.LParam >> 16 & (int) ushort.MaxValue;
      int num2 = (int) m.LParam & (int) ushort.MaxValue;
      m.WParam.ToInt32();
      int num3 = 45;
      if (num1 == num3)
      {
        Program.isToggled = !Program.isToggled;
        if (!Program.ispvpclack)
          return;
        new Thread((ThreadStart) (() => Program.printpvpclack())).Start();
      }
      else
      {
        IntPtr consoleWindow = a.GetConsoleWindow();
        if (Program.isHidden)
          a.ShowWindow(consoleWindow, 5);
        else
          a.ShowWindow(consoleWindow, 0);
        Program.isHidden = !Program.isHidden;
      }
    }

    private void a_Load(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.timer = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      this.timer.Enabled = true;
      this.timer.Interval = 40;
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(120, 0);
      this.Name = nameof (a);
      this.ShowIcon = false;
      this.Text = nameof (a);
      this.Load += new EventHandler(this.a_Load);
      this.Paint += new PaintEventHandler(this.a_Paint);
      this.ResumeLayout(false);
    }

    private enum KeyModifier
    {
      None = 0,
      Alt = 1,
      Control = 2,
      Shift = 4,
      WinKey = 8,
    }
  }
}
