// Decompiled with JetBrains decompiler
// Type: Sorter_20.DirHelper
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

using System;
using System.IO;

namespace Sorter_20
{
  public static class DirHelper
  {
    public static void DirectoryCopy(string sourceDirName, string destDirName)
    {
      try
      {
        DirectoryInfo directoryInfo1 = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] directoryInfoArray = directoryInfo1.Exists ? directoryInfo1.GetDirectories() : throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
        if (!Directory.Exists(destDirName))
          Directory.CreateDirectory(destDirName);
        foreach (FileInfo file in directoryInfo1.GetFiles())
        {
          string destFileName = Path.Combine(destDirName, file.Name);
          file.CopyTo(destFileName, false);
        }
        foreach (DirectoryInfo directoryInfo2 in directoryInfoArray)
        {
          string destDirName1 = Path.Combine(destDirName, directoryInfo2.Name);
          DirHelper.DirectoryCopy(directoryInfo2.FullName, destDirName1);
        }
      }
      catch (Exception ex)
      {
      }
    }

    public static void ClearDir(Settings settings)
    {
      if (Directory.Exists(Settings.MAIN_FOLDER))
      {
        try
        {
          Directory.Delete(Settings.MAIN_FOLDER, true);
        }
        catch (Exception ex)
        {
        }
      }
      Directory.CreateDirectory(Settings.MAIN_FOLDER);
      if (settings.Standart)
        Directory.CreateDirectory(Settings.STANDART_FOLDER);
      if (!settings.Base)
        return;
      Directory.CreateDirectory(Settings.BASE);
    }
  }
}
