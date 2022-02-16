// Decompiled with JetBrains decompiler
// Type: Sorter_20.LogFormat
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

namespace Sorter_20
{
  public class LogFormat
  {
    public string url;
    public string username;
    public string password;
    public string file;
    public string separator;

    public LogFormat(string url, string username, string password, string file)
    {
      this.url = url;
      this.username = username;
      this.password = password;
      this.file = file;
    }

    public LogFormat(string file, string separator)
    {
      this.file = file;
      this.separator = separator;
    }

    public LogFormat()
    {
    }
  }
}
