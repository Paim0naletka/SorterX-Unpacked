// Decompiled with JetBrains decompiler
// Type: SorterX.Properties.Resources
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SorterX.Properties
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
        if (SorterX.Properties.Resources.resourceMan == null)
          SorterX.Properties.Resources.resourceMan = new ResourceManager("SorterX.Properties.Resources", typeof (SorterX.Properties.Resources).Assembly);
        return SorterX.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => SorterX.Properties.Resources.resourceCulture;
      set => SorterX.Properties.Resources.resourceCulture = value;
    }
  }
}
