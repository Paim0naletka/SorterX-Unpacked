// Decompiled with JetBrains decompiler
// Type: Sorter_20.Settings
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

namespace Sorter_20
{
  public class Settings
  {
    public bool Standart;
    public bool Base;
    public bool Folders;
    public bool Wallets;
    public static string MAIN_FOLDER = "log";
    public static string STANDART_FOLDER = Settings.MAIN_FOLDER + "/Standart";
    public static string BASE = Settings.MAIN_FOLDER + "/Base";
    public static string FOLDERS = Settings.MAIN_FOLDER + "/Foldes";
    public static string WALLETS = Settings.MAIN_FOLDER + "/Wallets";
    public static string SEPARATOR = ";";
    public static string SERVICES_FOLDER = "services.txt";
    public static string BASE_FOLDER = "base";
    public static string FILE_FORMAT = ".txt";
  }
}
