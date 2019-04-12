// Decompiled with JetBrains decompiler
// Type: CMD.Properties.Resources
// Assembly: CⅯD, Version=10.0.16299.15, Culture=neutral, PublicKeyToken=null
// MVID: A4F1CAA0-B7FE-4C8D-83A4-D166C09344D1
// Assembly location: C:\Python34\pycmd.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace CMD.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (CMD.Properties.Resources.resourceMan == null)
          CMD.Properties.Resources.resourceMan = new ResourceManager("CMD.Properties.Resources", typeof (CMD.Properties.Resources).Assembly);
        return CMD.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return CMD.Properties.Resources.resourceCulture;
      }
      set
      {
        CMD.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Icon cmd
    {
      get
      {
        return (Icon) CMD.Properties.Resources.ResourceManager.GetObject(nameof (cmd), CMD.Properties.Resources.resourceCulture);
      }
    }
  }
}
