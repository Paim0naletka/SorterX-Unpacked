// Decompiled with JetBrains decompiler
// Type: Sorter_20.Check
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

using SorterX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Sorter_20
{
  public static class Check
  {
    public static LogFormat Azorult = new LogFormat("HOST:\t\t", "USER:\t\t", "PASS:\t\t", "PasswordsList.txt");
    public static LogFormat Smoke = new LogFormat("Host: ", "User: ", "Password: ", "Passwords.txt");
    public static LogFormat Predator = new LogFormat("Url: ", "Login: ", "Password: ", "passwords.txt");
    public static LogFormat Krot = new LogFormat("passwords.txt", "|");
    public static LogFormat Undefined1 = new LogFormat("URL: ", "Login: ", "Password: ", "Passwords.txt");
    public static List<string> walletBase = new List<string>();
    public static int maxThreadCount = 200;
    public static Stat localStat = new Stat();

    public static void Call(ref Stat stat, Settings settings, int soft, ref Label LOG)
    {
      DirHelper.ClearDir(settings);
      List<Log> sorted = new List<Log>();
      List<string> urlBase = PrepareHelper.GetUrls(Settings.SERVICES_FOLDER);
      string[] directories = Directory.GetDirectories(Settings.BASE_FOLDER);
      PrepareHelper.CreateSortedList(ref sorted, urlBase);
      int threadCount = 0;
      LogFormat format = new LogFormat();
      switch (soft)
      {
        case 1:
          format = Check.Azorult;
          break;
        case 2:
          format = Check.Krot;
          break;
        case 3:
          format = Check.Smoke;
          break;
        case 4:
          format = Check.Predator;
          break;
      }
      foreach (string str in directories)
      {
        string log = str;
        try
        {
          while (threadCount >= Check.maxThreadCount)
            Thread.Sleep(100);
          threadCount++;
          ++Check.localStat.total;
          stat = Check.localStat;
          new Thread((ThreadStart) (() =>
          {
            try
            {
              string[] files = Check.SearchFile(log, format.file);
              if (Check.SearchFile(log, "*.dat").Length != 0)
              {
                Check.walletBase.Add(log);
                ++Check.localStat.wallets;
              }
              if (files.Length != 0)
              {
                Check.CheckBase(log, files, urlBase.ToArray(), format, ref sorted);
                ++Check.localStat.filled;
                Check.localStat.txt += files.Length;
              }
              else
                ++Check.localStat.empty;
            }
            catch (Exception ex)
            {
            }
            --threadCount;
          })).Start();
        }
        catch (Exception ex)
        {
          File.WriteAllText("error.txt", ex.Message);
        }
      }
      if (threadCount != 0)
        Thread.Sleep(2000);
      foreach (Log log in sorted)
      {
        try
        {
          LOG.Text = "Write " + log.url + " to file...";
          string contents1 = "";
          string contents2 = "";
          foreach (string line in PrepareHelper.CreateUnique(log.lines))
          {
            if (settings.Standart)
              contents1 = contents1 + Log.GetStandart(line) + Environment.NewLine;
            if (settings.Base)
              contents2 = contents2 + Log.GetBase(line) + Environment.NewLine;
            if (settings.Folders)
            {
              try
              {
                string[] strArray1 = line.Split(';');
                string[] strArray2 = strArray1[0].Split('\\');
                DirHelper.DirectoryCopy(strArray1[0], Settings.FOLDERS + "/" + log.url + "/" + strArray2[strArray2.Length - 1]);
              }
              catch (Exception ex)
              {
              }
            }
          }
          if (contents1.Length > 1)
            File.WriteAllText(Settings.STANDART_FOLDER + "/" + log.url + Settings.FILE_FORMAT, contents1);
          if (contents2.Length > 1)
            File.WriteAllText(Settings.BASE + "/" + log.url + Settings.FILE_FORMAT, contents2);
        }
        catch (Exception ex)
        {
        }
      }
      LOG.Text = "Copy wallets ...";
      if (settings.Wallets)
      {
        foreach (string sourceDirName in Check.walletBase)
          DirHelper.DirectoryCopy(sourceDirName, Settings.WALLETS + "/" + sourceDirName.Split('\\')[1]);
      }
      LOG.Text = "Finished!";
    }

    public static void CheckBase(
      string log,
      string[] files,
      string[] request,
      LogFormat format,
      ref List<Log> sorted)
    {
      foreach (string file in files)
      {
        string[] strArray1 = File.ReadAllLines(file);
        foreach (string str1 in request)
        {
          string req = str1;
          List<string> collection = new List<string>();
          for (int index = 0; index < strArray1.Length; ++index)
          {
            if (!format.Equals((object) Check.Krot))
            {
              if (strArray1[index].Length > format.url.Length && strArray1[index].Substring(0, format.url.Length) == format.url)
              {
                if (Regex.IsMatch(strArray1[index].Substring(format.url.Length), req))
                {
                  try
                  {
                    string str2 = Environment.CurrentDirectory + "\\" + log + Settings.SEPARATOR + strArray1[index].Substring(format.url.Length) + Settings.SEPARATOR + strArray1[index + 1].Substring(format.username.Length) + Settings.SEPARATOR + strArray1[index + 2].Substring(format.password.Length);
                    if (!collection.Contains(str2))
                    {
                      collection.Add(str2);
                      ++Check.localStat.services;
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                }
              }
            }
            else if (strArray1[index].Split(format.separator[0]).Length != 0)
            {
              try
              {
                string[] strArray2 = strArray1[index].Split(format.separator[0]);
                if (Regex.IsMatch(strArray2[1], req))
                {
                  try
                  {
                    string str3 = Environment.CurrentDirectory + "\\" + log + Settings.SEPARATOR + strArray2[1].Substring(1, strArray2[1].Length - 2) + Settings.SEPARATOR + strArray2[2].Substring(1, strArray2[2].Length - 2) + Settings.SEPARATOR + strArray2[3].Substring(1, strArray2[3].Length - 2);
                    if (!collection.Contains(str3))
                    {
                      collection.Add(str3);
                      ++Check.localStat.services;
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
          }
          int index1 = sorted.FindIndex((Predicate<Log>) (x => x.url == req));
          sorted[index1].lines.AddRange((IEnumerable<string>) collection);
        }
      }
    }

    public static string[] SearchFile(string path, string file) => Directory.GetFiles(path, file, SearchOption.AllDirectories);
  }
}
