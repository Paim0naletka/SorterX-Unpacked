// Decompiled with JetBrains decompiler
// Type: Sorter_20.Log
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

using System;
using System.Collections.Generic;

namespace Sorter_20
{
  public class Log
  {
    public string url;
    public List<string> lines;

    public Log(string url, List<string> lines)
    {
      this.url = url;
      this.lines = lines;
    }

    public static string GetStandart(string line)
    {
      string[] strArray = line.Split(Settings.SEPARATOR[0]);
      return "FOLDER\t: " + strArray[0] + Environment.NewLine + "SITE\t: " + strArray[1] + Environment.NewLine + "USER\t: " + strArray[2] + Environment.NewLine + "PASS\t: " + strArray[3] + Environment.NewLine;
    }

    public static string GetBase(string line)
    {
      string[] strArray = line.Split(Settings.SEPARATOR[0]);
      return strArray[2] + ":" + strArray[3];
    }
  }
}
