// Decompiled with JetBrains decompiler
// Type: Sorter_20.PrepareHelper
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

using System.Collections.Generic;
using System.IO;

namespace Sorter_20
{
  public static class PrepareHelper
  {
    public static List<string> GetUrls(string file)
    {
      List<string> urls = new List<string>();
      foreach (string readAllLine in File.ReadAllLines(file))
        urls.Add(readAllLine);
      return urls;
    }

    public static void CreateSortedList(ref List<Log> sorted, List<string> urlBase)
    {
      foreach (string url in urlBase)
        sorted.Add(new Log(url, new List<string>()));
    }

    public static List<string> CreateUnique(List<string> lines)
    {
      List<string> list = new List<string>();
      foreach (string line in lines)
      {
        string[] strArray = line.Split(Settings.SEPARATOR[0]);
        if (!PrepareHelper.Equal(list, strArray[2] + Settings.SEPARATOR + strArray[3]))
          list.Add(line);
      }
      return list;
    }

    private static bool Equal(List<string> list, string line)
    {
      foreach (string str in list)
      {
        char[] chArray = new char[1]
        {
          Settings.SEPARATOR[0]
        };
        string[] strArray = str.Split(chArray);
        if (strArray[2] + Settings.SEPARATOR + strArray[3] == line)
          return true;
      }
      return false;
    }
  }
}
