// Decompiled with JetBrains decompiler
// Type: SorterX.Stat
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

namespace SorterX
{
  public class Stat
  {
    public int total;
    public int empty;
    public int filled;
    public int services;
    public int wallets;
    public int txt;

    public static void Undo(ref Stat stat)
    {
      stat.total = 0;
      stat.empty = 0;
      stat.filled = 0;
      stat.services = 0;
      stat.wallets = 0;
      stat.txt = 0;
    }
  }
}
