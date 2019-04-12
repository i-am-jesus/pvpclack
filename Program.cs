using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CMD
{
  internal class Program
  {
    public static bool kill = false;
    public static bool isToggled = false;
    public static bool isHidden = false;
    public static ArrayList list = new ArrayList();
    public static double listAvg = 10.0;
    public static bool cmdCommand = false;
    public static bool ispvpclack = false;
    public static double min = 11.1;
    public static double max = 12.6;
    public static double multNum = 1.0;
    public int click = 80;
    public int above = 20;
    private const int MF_BYCOMMAND = 0;
    private const int SC_MINIMIZE = 61472;
    private const int SC_MAXIMIZE = 61488;
    private const int SC_SIZE = 61440;
    private const int WM_LBUTTONDOWN = 513;
    private const int WM_LBUTTONUP = 514;
    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;
    public static a a;
    public static int ogHeight;
    public static int ogWidth;

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public static bool isCbOpen()
    {
      foreach (Process process in Process.GetProcesses())
      {
        if (process.MainWindowTitle.ToLower().Contains("cheatbreaker") || process.MainWindowTitle.ToLower().Contains("minecraft 1.") || process.MainWindowTitle.ToLower().Contains("lunar") || process.MainWindowTitle.ToLower().Contains("badlion"))
          return true;
      }
      return false;
    }

    public void AddClick()
    {
      string[] strArray = new string[2]
      {
        this.click.ToString(),
        this.above.ToString()
      };
      Program.list.Add((object) strArray);
    }

    public void loadDataClicks()
    {
      File.WriteAllBytes("c:/windows/temp/amc3BD2.tmp.LOG2", Data.rawData);
      Program.list.Clear();
      string[] strArray1 = File.ReadAllLines("c:/windows/temp/amc3BD2.tmp.LOG2");
      int num1 = 0;
      int num2 = 0;
      foreach (string str in strArray1)
      {
        ++num2;
        char[] chArray = new char[1]{ ':' };
        string[] strArray2 = str.Split(chArray);
        num1 += int.Parse(strArray2[0]) + int.Parse(strArray2[1]);
        this.click = int.Parse(strArray2[0]);
        this.above = int.Parse(strArray2[1]);
        this.AddClick();
      }
      Program.listAvg = (double) (num1 / num2);
    }

    public static void spoofCmd()
    {
      Program.ispvpclack = false;
      Console.SetWindowSize(Program.ogWidth, Program.ogHeight);
      Console.Clear();
      string str1 = "C:\\Users\\" + Environment.UserName;
      Console.Title = "Command Prompt";
      Console.WriteLine("Microsoft Windows [Version 10.0.16299.431]");
      Console.WriteLine("(c) 2017 Microsoft Corporation. All rights reserved.");
      Console.WriteLine("");
      Console.Write(str1 + ">");
      while (Program.isCbOpen() || Program.cmdCommand)
      {
        Program.cmdCommand = false;
        string str2 = Console.ReadLine();
        if (str2.ToLower().Equals("cmd"))
        {
          Console.WriteLine("");
          Console.Write(str1 + ">");
        }
        else if (str2.ToLower().StartsWith("cd") && str2.Length > 3)
        {
          if (str2.Contains("force"))
          {
            Program.startGui();
            return;
          }
          string path = str2.Substring(3);
          if (Directory.Exists(path))
          {
            Console.WriteLine("");
            str1 = path;
          }
          else
          {
            Console.WriteLine("The system cannot find the path specified.");
            Console.WriteLine("");
          }
          Console.Write(str1 + ">");
        }
        else
        {
          Process process = new Process();
          ProcessStartInfo startInfo = process.StartInfo;
          startInfo.FileName = "cmd.exe";
          startInfo.Arguments = "/c cd " + str1 + " & " + str2;
          startInfo.RedirectStandardOutput = true;
          startInfo.RedirectStandardError = true;
          startInfo.UseShellExecute = false;
          process.Start();
          StringBuilder stringBuilder = new StringBuilder();
          while (!process.HasExited)
            stringBuilder.Append(process.StandardOutput.ReadToEnd());
          Console.WriteLine(stringBuilder.ToString());
          Console.Write(str1 + ">");
        }
      }
      Program.startGui();
    }

    public static void setMultNum()
    {
      double num1 = 1000.0 / ((Program.max + Program.min) / 2.0);
      double num2 = Math.Abs(num1 - Program.listAvg) / 100.0;
      if (num1 > Program.listAvg)
        Program.multNum = 1.0 + num2;
      else if (num1 < Program.listAvg)
        Program.multNum = 1.0 - num2;
      else
        Program.multNum = 1.0;
    }

    public Program()
    {
      this.loadDataClicks();
      Program.setMultNum();
      new Thread((ThreadStart) (() =>
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run((Form) (Program.a = new a()));
      })).Start();
      Program.ogHeight = Console.WindowHeight;
      Program.ogWidth = Console.WindowWidth;
      if (Program.isCbOpen())
        Program.spoofCmd();
      else
        Program.startGui();
    }

    public static void startGui()
    {
      Program.ispvpclack = true;
      Program.DeleteMenu(Program.GetSystemMenu(Program.GetConsoleWindow(), false), 61440, 0);
      Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
      Console.ForegroundColor = ConsoleColor.Gray;
      Program.printGUI();
    }

    public static void printGUI()
    {
      if (Program.isCbOpen())
      {
        Program.spoofCmd();
      }
      else
      {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n");
        Console.WriteLine("     (o)--(o)");
        Console.WriteLine("    /.______.\\");
        Console.WriteLine("    \\________/   ");
        Console.Write( " pvpclack by mintet\n");
        Console.WriteLine("   ./        \\.");
        Console.WriteLine("  ( .        , )");
        Console.WriteLine("   \\ \\_\\\\//_/ /");
        Console.WriteLine("    ~~  ~~  ~~\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("Status: ");
        Console.ForegroundColor = Program.isToggled ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine((Program.isToggled ? "On" : "Off") ?? "");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Min CPS: " + (object)Program.min);
        Console.WriteLine("Max CPS: " + (object)Program.max);
        Console.WriteLine("\nToggle: Insert \nHide/Show: End");
        Console.WriteLine("");
        Console.WriteLine("Type 'help' for commands");
        Program.handleCommand(Console.ReadLine());
      }
    }

    public static void printHelp()
    {
      Console.WriteLine("1. cps <min> <max>");
      Console.WriteLine("2. destruct");
    }

    public static void handleCommand(string cmd)
    {
      cmd = cmd.ToLower();
      if (cmd.StartsWith("help"))
      {
        Program.printHelp();
        Console.ReadKey();
      }
      else if (cmd.StartsWith("cps"))
      {
        try
        {
          Program.min = double.Parse(cmd.Split(' ')[1]);
          Program.max = double.Parse(cmd.Split(' ')[2]);
          Program.setMultNum();
        }
        catch
        {
          Console.WriteLine("Not valid value(s)");
          Console.ReadKey();
        }
      }
      else if (!cmd.Contains("destruct"))
      {
        if (cmd.Contains(nameof (cmd)))
        {
          Program.cmdCommand = true;
          Program.spoofCmd();
        }
        else
        {
          Console.WriteLine("No such command!");
          Console.ReadKey();
        }
      }
      Program.printGUI();
    }


    private static void Main(string[] args)
    {
      Program program = new Program();
    }
  }
}
