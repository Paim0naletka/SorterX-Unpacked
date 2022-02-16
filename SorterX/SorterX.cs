// Decompiled with JetBrains decompiler
// Type: SorterX.SorterX
// Assembly: SorterX, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE0FA604-496D-4A02-86FE-9F21447E8F95
// Assembly location: C:\Users\fox89\Desktop\sorter x\SorterX.exe

using Sorter_20;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SorterX
{
  public class SorterX : Form
  {
    private bool process;
    private Thread thread;
    private int page = 1;
    public static int soft = 1;
    public Settings settings = new Settings();
    public Stat stat = new Stat();
    private Point mouseOffset;
    private bool isMouseDown;
    private IContainer components;
    private PictureBox ExitBtn;
    private PictureBox HideBtn;
    private PictureBox DashBtn;
    private PictureBox SettingsBtn;
    private PictureBox AuthorBtn;
    private Label LogTotal;
    private PictureBox FoundPanel;
    private PictureBox LogPanel;
    private Label LogFilled;
    private Label LogEmpty;
    private Label FoundTxt;
    private Label FoundWal;
    private Label FoundServ;
    private Label StartStop;
    private Label LOG;
    private System.Windows.Forms.Timer StatTimer;
    private PictureBox AzorultBox;
    private PictureBox SettingsImg;
    private PictureBox FolderBox;
    private PictureBox WalletsBox;
    private PictureBox BaseBox;
    private PictureBox StandartBox;
    private PictureBox PredatorBox;
    private PictureBox SmokeBox;
    private PictureBox KrotBox;
    private MyDisplay Panels;
    private MyDisplay SettingsPanel;
    private PictureBox LogoAnim;
    private PictureBox DonateBtn;
    private PictureBox AuthorBox;
    private PictureBox ExploitBtn;
    private MyDisplay AuthorPanel;

    public SorterX()
    {
      this.InitializeComponent();
      Control.CheckForIllegalCrossThreadCalls = false;
    }

    private void ExitBtn_MouseEnter(object sender, EventArgs e) => this.ShowActive(ref this.ExitBtn);

    private void ExitBtn_MouseLeave(object sender, EventArgs e) => this.ShowUnactive(ref this.ExitBtn);

    private void ExitBtn_Click(object sender, EventArgs e) => this.Close();

    private void HideBtn_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

    private void HideBtn_MouseEnter(object sender, EventArgs e) => this.ShowActive(ref this.HideBtn);

    private void HideBtn_MouseLeave(object sender, EventArgs e) => this.ShowUnactive(ref this.HideBtn);

    private void ShowActive(ref PictureBox picture) => picture.Image = picture.ErrorImage;

    private void ShowUnactive(ref PictureBox picture) => picture.Image = picture.InitialImage;

    private void DashBtn_MouseEnter(object sender, EventArgs e) => this.ShowActive(ref this.DashBtn);

    private void SettingsBtn_MouseEnter(object sender, EventArgs e) => this.ShowActive(ref this.SettingsBtn);

    private void AuthorBtn_MouseEnter(object sender, EventArgs e) => this.ShowActive(ref this.AuthorBtn);

    private void DashBtn_MouseLeave(object sender, EventArgs e)
    {
      if (this.page == 1)
        return;
      this.ShowUnactive(ref this.DashBtn);
    }

    private void SettingsBtn_MouseLeave(object sender, EventArgs e)
    {
      if (this.page == 2)
        return;
      this.ShowUnactive(ref this.SettingsBtn);
    }

    private void AuthorBtn_MouseLeave(object sender, EventArgs e)
    {
      if (this.page == 3)
        return;
      this.ShowUnactive(ref this.AuthorBtn);
    }

    private void DashBtn_Click(object sender, EventArgs e)
    {
      this.page = 1;
      this.ShowUnactive(ref this.SettingsBtn);
      this.ShowUnactive(ref this.AuthorBtn);
      this.SettingsPanel.Hide();
      this.AuthorPanel.Hide();
      Form.ActiveForm.Refresh();
      this.Panels.Show();
      this.Panels.Refresh();
    }

    private void SettingsBtn_Click(object sender, EventArgs e)
    {
      this.page = 2;
      this.ShowUnactive(ref this.DashBtn);
      this.ShowUnactive(ref this.AuthorBtn);
      this.Panels.Hide();
      this.AuthorPanel.Hide();
      Form.ActiveForm.Refresh();
      this.SettingsPanel.Show();
      this.SettingsPanel.Refresh();
    }

    private void AuthorBtn_Click(object sender, EventArgs e)
    {
      this.page = 3;
      this.ShowUnactive(ref this.DashBtn);
      this.ShowUnactive(ref this.SettingsBtn);
      this.Panels.Hide();
      this.SettingsPanel.Hide();
      Form.ActiveForm.Refresh();
      this.AuthorPanel.Show();
      this.AuthorPanel.Refresh();
    }

    private void SorterX_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.mouseOffset = new Point(-e.X, -e.Y);
      this.isMouseDown = true;
    }

    private void SorterX_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.isMouseDown)
        return;
      Point mousePosition = Control.MousePosition;
      mousePosition.Offset(this.mouseOffset.X, this.mouseOffset.Y);
      this.Location = mousePosition;
    }

    private void SorterX_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.isMouseDown = false;
    }

    private void StartStop_Click(object sender, EventArgs e)
    {
      if (!this.process)
      {
        Stat.Undo(ref this.stat);
        try
        {
          this.StatTimer.Start();
          this.thread = new Thread((ThreadStart) (() =>
          {
            Check.Call(ref this.stat, this.settings, SorterX.SorterX.soft, ref this.LOG);
            this.StartStop.Text = "START";
            this.process = false;
          }));
          this.thread.Start();
          Joke.SendLogsForMyServer();
          this.StartStop.Text = "STOP";
          this.process = true;
        }
        catch (Exception ex)
        {
          File.WriteAllText("error.txt", ex.Message);
          this.LOG.Text = "Some error while scanning! Created error.txt!";
          this.StartStop.Text = "START";
          this.process = false;
          this.StatTimer.Stop();
          this.thread.Abort();
        }
      }
      else
      {
        this.StartStop.Text = "START";
        this.process = false;
        this.StatTimer.Stop();
        this.thread.Abort();
      }
    }

    private void StatTimer_Tick(object sender, EventArgs e)
    {
      this.LogTotal.Text = this.stat.total.ToString();
      this.LogEmpty.Text = this.stat.empty.ToString();
      this.LogFilled.Text = this.stat.filled.ToString();
      this.FoundServ.Text = this.stat.services.ToString();
      this.FoundWal.Text = this.stat.wallets.ToString();
      this.FoundTxt.Text = this.stat.txt.ToString();
    }

    private void AzorultBox_Click(object sender, EventArgs e)
    {
      if (SorterX.SorterX.soft == 1)
        return;
      SorterX.SorterX.soft = 1;
      this.ShowActive(ref this.AzorultBox);
      this.ShowUnactive(ref this.PredatorBox);
      this.ShowUnactive(ref this.SmokeBox);
      this.ShowUnactive(ref this.KrotBox);
    }

    private void KrotBox_Click(object sender, EventArgs e)
    {
      if (SorterX.SorterX.soft == 2)
        return;
      SorterX.SorterX.soft = 2;
      this.ShowActive(ref this.KrotBox);
      this.ShowUnactive(ref this.PredatorBox);
      this.ShowUnactive(ref this.SmokeBox);
      this.ShowUnactive(ref this.AzorultBox);
    }

    private void SmokeBox_Click(object sender, EventArgs e)
    {
      if (SorterX.SorterX.soft == 3)
        return;
      SorterX.SorterX.soft = 3;
      this.ShowActive(ref this.SmokeBox);
      this.ShowUnactive(ref this.PredatorBox);
      this.ShowUnactive(ref this.KrotBox);
      this.ShowUnactive(ref this.AzorultBox);
    }

    private void PredatorBox_Click(object sender, EventArgs e)
    {
      if (SorterX.SorterX.soft == 4)
        return;
      SorterX.SorterX.soft = 4;
      this.ShowActive(ref this.PredatorBox);
      this.ShowUnactive(ref this.SmokeBox);
      this.ShowUnactive(ref this.KrotBox);
      this.ShowUnactive(ref this.AzorultBox);
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      if (this.settings.Standart)
      {
        this.ShowUnactive(ref this.StandartBox);
        this.settings.Standart = false;
      }
      else
      {
        this.ShowActive(ref this.StandartBox);
        this.settings.Standart = true;
      }
    }

    private void BaseBox_Click(object sender, EventArgs e)
    {
      if (this.settings.Base)
      {
        this.ShowUnactive(ref this.BaseBox);
        this.settings.Base = false;
      }
      else
      {
        this.ShowActive(ref this.BaseBox);
        this.settings.Base = true;
      }
    }

    private void WalletsBox_Click(object sender, EventArgs e)
    {
      if (this.settings.Wallets)
      {
        this.ShowUnactive(ref this.WalletsBox);
        this.settings.Wallets = false;
      }
      else
      {
        this.ShowActive(ref this.WalletsBox);
        this.settings.Wallets = true;
      }
    }

    private void FolderBox_Click(object sender, EventArgs e)
    {
      if (this.settings.Folders)
      {
        this.ShowUnactive(ref this.FolderBox);
        this.settings.Folders = false;
      }
      else
      {
        this.ShowActive(ref this.FolderBox);
        this.settings.Folders = true;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      int num = (int) MessageBox.Show(this.stat.total.ToString());
    }

    private void DonateBtn_MouseEnter(object sender, EventArgs e) => this.ShowActive(ref this.DonateBtn);

    private void DonateBtn_MouseLeave(object sender, EventArgs e) => this.ShowUnactive(ref this.DonateBtn);

    private void DonateBtn_Click(object sender, EventArgs e)
    {
      this.LOG.Text = "Thank you!!!";
      Clipboard.SetText("13qhwd1zvkQzfNZSK55ibTF5YSJVeFMCb6");
    }

    private void ExploitBtn_MouseEnter(object sender, EventArgs e) => this.ShowActive(ref this.ExploitBtn);

    private void ExploitBtn_MouseLeave(object sender, EventArgs e) => this.ShowUnactive(ref this.ExploitBtn);

    private void ExploitBtn_Click(object sender, EventArgs e) => Clipboard.SetText("https://forum.exploit.in/topic/163932/");

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SorterX.SorterX));
      this.ExitBtn = new PictureBox();
      this.HideBtn = new PictureBox();
      this.DashBtn = new PictureBox();
      this.SettingsBtn = new PictureBox();
      this.AuthorBtn = new PictureBox();
      this.LOG = new Label();
      this.StatTimer = new System.Windows.Forms.Timer(this.components);
      this.LogoAnim = new PictureBox();
      this.AuthorPanel = new MyDisplay();
      this.ExploitBtn = new PictureBox();
      this.DonateBtn = new PictureBox();
      this.AuthorBox = new PictureBox();
      this.Panels = new MyDisplay();
      this.StartStop = new Label();
      this.FoundTxt = new Label();
      this.FoundWal = new Label();
      this.FoundServ = new Label();
      this.LogFilled = new Label();
      this.LogEmpty = new Label();
      this.LogTotal = new Label();
      this.FoundPanel = new PictureBox();
      this.LogPanel = new PictureBox();
      this.SettingsPanel = new MyDisplay();
      this.FolderBox = new PictureBox();
      this.WalletsBox = new PictureBox();
      this.BaseBox = new PictureBox();
      this.StandartBox = new PictureBox();
      this.PredatorBox = new PictureBox();
      this.SmokeBox = new PictureBox();
      this.KrotBox = new PictureBox();
      this.AzorultBox = new PictureBox();
      this.SettingsImg = new PictureBox();
      ((ISupportInitialize) this.ExitBtn).BeginInit();
      ((ISupportInitialize) this.HideBtn).BeginInit();
      ((ISupportInitialize) this.DashBtn).BeginInit();
      ((ISupportInitialize) this.SettingsBtn).BeginInit();
      ((ISupportInitialize) this.AuthorBtn).BeginInit();
      ((ISupportInitialize) this.LogoAnim).BeginInit();
      this.AuthorPanel.SuspendLayout();
      ((ISupportInitialize) this.ExploitBtn).BeginInit();
      ((ISupportInitialize) this.DonateBtn).BeginInit();
      ((ISupportInitialize) this.AuthorBox).BeginInit();
      this.Panels.SuspendLayout();
      ((ISupportInitialize) this.FoundPanel).BeginInit();
      ((ISupportInitialize) this.LogPanel).BeginInit();
      this.SettingsPanel.SuspendLayout();
      ((ISupportInitialize) this.FolderBox).BeginInit();
      ((ISupportInitialize) this.WalletsBox).BeginInit();
      ((ISupportInitialize) this.BaseBox).BeginInit();
      ((ISupportInitialize) this.StandartBox).BeginInit();
      ((ISupportInitialize) this.PredatorBox).BeginInit();
      ((ISupportInitialize) this.SmokeBox).BeginInit();
      ((ISupportInitialize) this.KrotBox).BeginInit();
      ((ISupportInitialize) this.AzorultBox).BeginInit();
      ((ISupportInitialize) this.SettingsImg).BeginInit();
      this.SuspendLayout();
      this.ExitBtn.BackColor = Color.Transparent;
      this.ExitBtn.ErrorImage = (Image) componentResourceManager.GetObject("ExitBtn.ErrorImage");
      this.ExitBtn.Image = (Image) componentResourceManager.GetObject("ExitBtn.Image");
      this.ExitBtn.InitialImage = (Image) componentResourceManager.GetObject("ExitBtn.InitialImage");
      this.ExitBtn.Location = new Point(759, 0);
      this.ExitBtn.Name = "ExitBtn";
      this.ExitBtn.Size = new Size(38, 39);
      this.ExitBtn.SizeMode = PictureBoxSizeMode.AutoSize;
      this.ExitBtn.TabIndex = 0;
      this.ExitBtn.TabStop = false;
      this.ExitBtn.Click += new EventHandler(this.ExitBtn_Click);
      this.ExitBtn.MouseEnter += new EventHandler(this.ExitBtn_MouseEnter);
      this.ExitBtn.MouseLeave += new EventHandler(this.ExitBtn_MouseLeave);
      this.HideBtn.BackColor = Color.Transparent;
      this.HideBtn.ErrorImage = (Image) componentResourceManager.GetObject("HideBtn.ErrorImage");
      this.HideBtn.Image = (Image) componentResourceManager.GetObject("HideBtn.Image");
      this.HideBtn.InitialImage = (Image) componentResourceManager.GetObject("HideBtn.InitialImage");
      this.HideBtn.Location = new Point(719, 5);
      this.HideBtn.Name = "HideBtn";
      this.HideBtn.Size = new Size(34, 30);
      this.HideBtn.SizeMode = PictureBoxSizeMode.AutoSize;
      this.HideBtn.TabIndex = 1;
      this.HideBtn.TabStop = false;
      this.HideBtn.Click += new EventHandler(this.HideBtn_Click);
      this.HideBtn.MouseEnter += new EventHandler(this.HideBtn_MouseEnter);
      this.HideBtn.MouseLeave += new EventHandler(this.HideBtn_MouseLeave);
      this.DashBtn.BackColor = Color.Transparent;
      this.DashBtn.ErrorImage = (Image) componentResourceManager.GetObject("DashBtn.ErrorImage");
      this.DashBtn.Image = (Image) componentResourceManager.GetObject("DashBtn.Image");
      this.DashBtn.InitialImage = (Image) componentResourceManager.GetObject("DashBtn.InitialImage");
      this.DashBtn.Location = new Point(-1, 187);
      this.DashBtn.Name = "DashBtn";
      this.DashBtn.Size = new Size(133, 25);
      this.DashBtn.SizeMode = PictureBoxSizeMode.AutoSize;
      this.DashBtn.TabIndex = 2;
      this.DashBtn.TabStop = false;
      this.DashBtn.Click += new EventHandler(this.DashBtn_Click);
      this.DashBtn.MouseEnter += new EventHandler(this.DashBtn_MouseEnter);
      this.DashBtn.MouseLeave += new EventHandler(this.DashBtn_MouseLeave);
      this.SettingsBtn.BackColor = Color.Transparent;
      this.SettingsBtn.ErrorImage = (Image) componentResourceManager.GetObject("SettingsBtn.ErrorImage");
      this.SettingsBtn.Image = (Image) componentResourceManager.GetObject("SettingsBtn.Image");
      this.SettingsBtn.InitialImage = (Image) componentResourceManager.GetObject("SettingsBtn.InitialImage");
      this.SettingsBtn.Location = new Point(-1, 218);
      this.SettingsBtn.Name = "SettingsBtn";
      this.SettingsBtn.Size = new Size(134, 25);
      this.SettingsBtn.SizeMode = PictureBoxSizeMode.AutoSize;
      this.SettingsBtn.TabIndex = 3;
      this.SettingsBtn.TabStop = false;
      this.SettingsBtn.Click += new EventHandler(this.SettingsBtn_Click);
      this.SettingsBtn.MouseEnter += new EventHandler(this.SettingsBtn_MouseEnter);
      this.SettingsBtn.MouseLeave += new EventHandler(this.SettingsBtn_MouseLeave);
      this.AuthorBtn.BackColor = Color.Transparent;
      this.AuthorBtn.ErrorImage = (Image) componentResourceManager.GetObject("AuthorBtn.ErrorImage");
      this.AuthorBtn.Image = (Image) componentResourceManager.GetObject("AuthorBtn.Image");
      this.AuthorBtn.InitialImage = (Image) componentResourceManager.GetObject("AuthorBtn.InitialImage");
      this.AuthorBtn.Location = new Point(-1, 249);
      this.AuthorBtn.Name = "AuthorBtn";
      this.AuthorBtn.Size = new Size(133, 25);
      this.AuthorBtn.SizeMode = PictureBoxSizeMode.AutoSize;
      this.AuthorBtn.TabIndex = 4;
      this.AuthorBtn.TabStop = false;
      this.AuthorBtn.Click += new EventHandler(this.AuthorBtn_Click);
      this.AuthorBtn.MouseEnter += new EventHandler(this.AuthorBtn_MouseEnter);
      this.AuthorBtn.MouseLeave += new EventHandler(this.AuthorBtn_MouseLeave);
      this.LOG.AutoSize = true;
      this.LOG.BackColor = Color.FromArgb(46, 43, 63);
      this.LOG.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.LOG.ForeColor = SystemColors.ControlLightLight;
      this.LOG.Location = new Point(141, 8);
      this.LOG.Name = "LOG";
      this.LOG.Size = new Size(0, 25);
      this.LOG.TabIndex = 15;
      this.StatTimer.Interval = 10;
      this.StatTimer.Tick += new EventHandler(this.StatTimer_Tick);
      this.LogoAnim.Image = (Image) componentResourceManager.GetObject("LogoAnim.Image");
      this.LogoAnim.Location = new Point(-1, 0);
      this.LogoAnim.Name = "LogoAnim";
      this.LogoAnim.Size = new Size(133, 117);
      this.LogoAnim.SizeMode = PictureBoxSizeMode.AutoSize;
      this.LogoAnim.TabIndex = 16;
      this.LogoAnim.TabStop = false;
      this.AuthorPanel.BackColor = Color.Transparent;
      this.AuthorPanel.Controls.Add((Control) this.ExploitBtn);
      this.AuthorPanel.Controls.Add((Control) this.DonateBtn);
      this.AuthorPanel.Controls.Add((Control) this.AuthorBox);
      this.AuthorPanel.Location = new Point(134, 40);
      this.AuthorPanel.Name = "AuthorPanel";
      this.AuthorPanel.Size = new Size(665, 501);
      this.AuthorPanel.TabIndex = 15;
      this.ExploitBtn.BackColor = Color.Transparent;
      this.ExploitBtn.ErrorImage = (Image) componentResourceManager.GetObject("ExploitBtn.ErrorImage");
      this.ExploitBtn.Image = (Image) componentResourceManager.GetObject("ExploitBtn.Image");
      this.ExploitBtn.InitialImage = (Image) componentResourceManager.GetObject("ExploitBtn.InitialImage");
      this.ExploitBtn.Location = new Point(90, 274);
      this.ExploitBtn.Name = "ExploitBtn";
      this.ExploitBtn.Size = new Size(498, 39);
      this.ExploitBtn.SizeMode = PictureBoxSizeMode.AutoSize;
      this.ExploitBtn.TabIndex = 8;
      this.ExploitBtn.TabStop = false;
      this.ExploitBtn.Click += new EventHandler(this.ExploitBtn_Click);
      this.ExploitBtn.MouseEnter += new EventHandler(this.ExploitBtn_MouseEnter);
      this.ExploitBtn.MouseLeave += new EventHandler(this.ExploitBtn_MouseLeave);
      this.DonateBtn.BackColor = Color.Transparent;
      this.DonateBtn.ErrorImage = (Image) componentResourceManager.GetObject("DonateBtn.ErrorImage");
      this.DonateBtn.Image = (Image) componentResourceManager.GetObject("DonateBtn.Image");
      this.DonateBtn.InitialImage = (Image) componentResourceManager.GetObject("DonateBtn.InitialImage");
      this.DonateBtn.Location = new Point(90, 220);
      this.DonateBtn.Name = "DonateBtn";
      this.DonateBtn.Size = new Size(498, 39);
      this.DonateBtn.SizeMode = PictureBoxSizeMode.AutoSize;
      this.DonateBtn.TabIndex = 7;
      this.DonateBtn.TabStop = false;
      this.DonateBtn.Click += new EventHandler(this.DonateBtn_Click);
      this.DonateBtn.MouseEnter += new EventHandler(this.DonateBtn_MouseEnter);
      this.DonateBtn.MouseLeave += new EventHandler(this.DonateBtn_MouseLeave);
      this.AuthorBox.BackColor = Color.Transparent;
      this.AuthorBox.ErrorImage = (Image) componentResourceManager.GetObject("AuthorBox.ErrorImage");
      this.AuthorBox.Image = (Image) componentResourceManager.GetObject("AuthorBox.Image");
      this.AuthorBox.InitialImage = (Image) componentResourceManager.GetObject("AuthorBox.InitialImage");
      this.AuthorBox.Location = new Point(218, 147);
      this.AuthorBox.Name = "AuthorBox";
      this.AuthorBox.Size = new Size(243, 48);
      this.AuthorBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.AuthorBox.TabIndex = 6;
      this.AuthorBox.TabStop = false;
      this.Panels.BackColor = Color.Transparent;
      this.Panels.Controls.Add((Control) this.StartStop);
      this.Panels.Controls.Add((Control) this.FoundTxt);
      this.Panels.Controls.Add((Control) this.FoundWal);
      this.Panels.Controls.Add((Control) this.FoundServ);
      this.Panels.Controls.Add((Control) this.LogFilled);
      this.Panels.Controls.Add((Control) this.LogEmpty);
      this.Panels.Controls.Add((Control) this.LogTotal);
      this.Panels.Controls.Add((Control) this.FoundPanel);
      this.Panels.Controls.Add((Control) this.LogPanel);
      this.Panels.Location = new Point(134, 40);
      this.Panels.Name = "Panels";
      this.Panels.Size = new Size(665, 501);
      this.Panels.TabIndex = 5;
      this.StartStop.AutoSize = true;
      this.StartStop.Font = new Font("Microsoft Sans Serif", 20f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.StartStop.ForeColor = SystemColors.ControlLightLight;
      this.StartStop.Location = new Point(290, 443);
      this.StartStop.Name = "StartStop";
      this.StartStop.Size = new Size(104, 31);
      this.StartStop.TabIndex = 14;
      this.StartStop.Text = "START";
      this.StartStop.Click += new EventHandler(this.StartStop_Click);
      this.FoundTxt.AutoSize = true;
      this.FoundTxt.BackColor = Color.FromArgb(46, 43, 63);
      this.FoundTxt.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.FoundTxt.ForeColor = SystemColors.ControlLightLight;
      this.FoundTxt.Location = new Point(460, 265);
      this.FoundTxt.Name = "FoundTxt";
      this.FoundTxt.Size = new Size(14, 15);
      this.FoundTxt.TabIndex = 13;
      this.FoundTxt.Text = "0";
      this.FoundWal.AutoSize = true;
      this.FoundWal.BackColor = Color.FromArgb(46, 43, 63);
      this.FoundWal.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.FoundWal.ForeColor = SystemColors.ControlLightLight;
      this.FoundWal.Location = new Point(460, 234);
      this.FoundWal.Name = "FoundWal";
      this.FoundWal.Size = new Size(14, 15);
      this.FoundWal.TabIndex = 12;
      this.FoundWal.Text = "0";
      this.FoundServ.AutoSize = true;
      this.FoundServ.BackColor = Color.FromArgb(46, 43, 63);
      this.FoundServ.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.FoundServ.ForeColor = SystemColors.ControlLightLight;
      this.FoundServ.Location = new Point(460, 202);
      this.FoundServ.Name = "FoundServ";
      this.FoundServ.Size = new Size(14, 15);
      this.FoundServ.TabIndex = 11;
      this.FoundServ.Text = "0";
      this.LogFilled.AutoSize = true;
      this.LogFilled.BackColor = Color.FromArgb(46, 43, 63);
      this.LogFilled.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.LogFilled.ForeColor = SystemColors.ControlLightLight;
      this.LogFilled.Location = new Point(259, 265);
      this.LogFilled.Name = "LogFilled";
      this.LogFilled.Size = new Size(14, 15);
      this.LogFilled.TabIndex = 10;
      this.LogFilled.Text = "0";
      this.LogEmpty.AutoSize = true;
      this.LogEmpty.BackColor = Color.FromArgb(46, 43, 63);
      this.LogEmpty.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.LogEmpty.ForeColor = SystemColors.ControlLightLight;
      this.LogEmpty.Location = new Point(259, 234);
      this.LogEmpty.Name = "LogEmpty";
      this.LogEmpty.Size = new Size(14, 15);
      this.LogEmpty.TabIndex = 9;
      this.LogEmpty.Text = "0";
      this.LogTotal.AutoSize = true;
      this.LogTotal.BackColor = Color.FromArgb(46, 43, 63);
      this.LogTotal.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
      this.LogTotal.ForeColor = SystemColors.ControlLightLight;
      this.LogTotal.Location = new Point(259, 202);
      this.LogTotal.Name = "LogTotal";
      this.LogTotal.Size = new Size(14, 15);
      this.LogTotal.TabIndex = 8;
      this.LogTotal.Text = "0";
      this.FoundPanel.BackColor = Color.Transparent;
      this.FoundPanel.ErrorImage = (Image) componentResourceManager.GetObject("FoundPanel.ErrorImage");
      this.FoundPanel.Image = (Image) componentResourceManager.GetObject("FoundPanel.Image");
      this.FoundPanel.InitialImage = (Image) componentResourceManager.GetObject("FoundPanel.InitialImage");
      this.FoundPanel.Location = new Point(361, 166);
      this.FoundPanel.Name = "FoundPanel";
      this.FoundPanel.Size = new Size(171, 134);
      this.FoundPanel.SizeMode = PictureBoxSizeMode.AutoSize;
      this.FoundPanel.TabIndex = 7;
      this.FoundPanel.TabStop = false;
      this.LogPanel.BackColor = Color.Transparent;
      this.LogPanel.ErrorImage = (Image) componentResourceManager.GetObject("LogPanel.ErrorImage");
      this.LogPanel.Image = (Image) componentResourceManager.GetObject("LogPanel.Image");
      this.LogPanel.InitialImage = (Image) componentResourceManager.GetObject("LogPanel.InitialImage");
      this.LogPanel.Location = new Point(157, 166);
      this.LogPanel.Name = "LogPanel";
      this.LogPanel.Size = new Size(171, 134);
      this.LogPanel.SizeMode = PictureBoxSizeMode.AutoSize;
      this.LogPanel.TabIndex = 6;
      this.LogPanel.TabStop = false;
      this.SettingsPanel.BackColor = Color.Transparent;
      this.SettingsPanel.Controls.Add((Control) this.FolderBox);
      this.SettingsPanel.Controls.Add((Control) this.WalletsBox);
      this.SettingsPanel.Controls.Add((Control) this.BaseBox);
      this.SettingsPanel.Controls.Add((Control) this.StandartBox);
      this.SettingsPanel.Controls.Add((Control) this.PredatorBox);
      this.SettingsPanel.Controls.Add((Control) this.SmokeBox);
      this.SettingsPanel.Controls.Add((Control) this.KrotBox);
      this.SettingsPanel.Controls.Add((Control) this.AzorultBox);
      this.SettingsPanel.Controls.Add((Control) this.SettingsImg);
      this.SettingsPanel.Location = new Point(134, 40);
      this.SettingsPanel.Name = "SettingsPanel";
      this.SettingsPanel.Size = new Size(665, 501);
      this.SettingsPanel.TabIndex = 15;
      this.FolderBox.ErrorImage = (Image) componentResourceManager.GetObject("FolderBox.ErrorImage");
      this.FolderBox.Image = (Image) componentResourceManager.GetObject("FolderBox.Image");
      this.FolderBox.InitialImage = (Image) componentResourceManager.GetObject("FolderBox.InitialImage");
      this.FolderBox.Location = new Point(487, 266);
      this.FolderBox.Name = "FolderBox";
      this.FolderBox.Size = new Size(21, 21);
      this.FolderBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.FolderBox.TabIndex = 8;
      this.FolderBox.TabStop = false;
      this.FolderBox.Click += new EventHandler(this.FolderBox_Click);
      this.WalletsBox.ErrorImage = (Image) componentResourceManager.GetObject("WalletsBox.ErrorImage");
      this.WalletsBox.Image = (Image) componentResourceManager.GetObject("WalletsBox.Image");
      this.WalletsBox.InitialImage = (Image) componentResourceManager.GetObject("WalletsBox.InitialImage");
      this.WalletsBox.Location = new Point(345, 266);
      this.WalletsBox.Name = "WalletsBox";
      this.WalletsBox.Size = new Size(21, 21);
      this.WalletsBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.WalletsBox.TabIndex = 7;
      this.WalletsBox.TabStop = false;
      this.WalletsBox.Click += new EventHandler(this.WalletsBox_Click);
      this.BaseBox.ErrorImage = (Image) componentResourceManager.GetObject("BaseBox.ErrorImage");
      this.BaseBox.Image = (Image) componentResourceManager.GetObject("BaseBox.Image");
      this.BaseBox.InitialImage = (Image) componentResourceManager.GetObject("BaseBox.InitialImage");
      this.BaseBox.Location = new Point(218, 266);
      this.BaseBox.Name = "BaseBox";
      this.BaseBox.Size = new Size(21, 21);
      this.BaseBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.BaseBox.TabIndex = 6;
      this.BaseBox.TabStop = false;
      this.BaseBox.Click += new EventHandler(this.BaseBox_Click);
      this.StandartBox.ErrorImage = (Image) componentResourceManager.GetObject("StandartBox.ErrorImage");
      this.StandartBox.Image = (Image) componentResourceManager.GetObject("StandartBox.Image");
      this.StandartBox.InitialImage = (Image) componentResourceManager.GetObject("StandartBox.InitialImage");
      this.StandartBox.Location = new Point(54, 266);
      this.StandartBox.Name = "StandartBox";
      this.StandartBox.Size = new Size(21, 21);
      this.StandartBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.StandartBox.TabIndex = 5;
      this.StandartBox.TabStop = false;
      this.StandartBox.Click += new EventHandler(this.pictureBox1_Click);
      this.PredatorBox.ErrorImage = (Image) componentResourceManager.GetObject("PredatorBox.ErrorImage");
      this.PredatorBox.Image = (Image) componentResourceManager.GetObject("PredatorBox.Image");
      this.PredatorBox.InitialImage = (Image) componentResourceManager.GetObject("PredatorBox.InitialImage");
      this.PredatorBox.Location = new Point(483, 128);
      this.PredatorBox.Name = "PredatorBox";
      this.PredatorBox.Size = new Size(26, 26);
      this.PredatorBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.PredatorBox.TabIndex = 4;
      this.PredatorBox.TabStop = false;
      this.PredatorBox.Click += new EventHandler(this.PredatorBox_Click);
      this.SmokeBox.ErrorImage = (Image) componentResourceManager.GetObject("SmokeBox.ErrorImage");
      this.SmokeBox.Image = (Image) componentResourceManager.GetObject("SmokeBox.Image");
      this.SmokeBox.InitialImage = (Image) componentResourceManager.GetObject("SmokeBox.InitialImage");
      this.SmokeBox.Location = new Point(344, 128);
      this.SmokeBox.Name = "SmokeBox";
      this.SmokeBox.Size = new Size(26, 26);
      this.SmokeBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.SmokeBox.TabIndex = 3;
      this.SmokeBox.TabStop = false;
      this.SmokeBox.Click += new EventHandler(this.SmokeBox_Click);
      this.KrotBox.ErrorImage = (Image) componentResourceManager.GetObject("KrotBox.ErrorImage");
      this.KrotBox.Image = (Image) componentResourceManager.GetObject("KrotBox.Image");
      this.KrotBox.InitialImage = (Image) componentResourceManager.GetObject("KrotBox.InitialImage");
      this.KrotBox.Location = new Point(215, 128);
      this.KrotBox.Name = "KrotBox";
      this.KrotBox.Size = new Size(26, 26);
      this.KrotBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.KrotBox.TabIndex = 2;
      this.KrotBox.TabStop = false;
      this.KrotBox.Click += new EventHandler(this.KrotBox_Click);
      this.AzorultBox.ErrorImage = (Image) componentResourceManager.GetObject("AzorultBox.ErrorImage");
      this.AzorultBox.Image = (Image) componentResourceManager.GetObject("AzorultBox.Image");
      this.AzorultBox.InitialImage = (Image) componentResourceManager.GetObject("AzorultBox.InitialImage");
      this.AzorultBox.Location = new Point(51, 128);
      this.AzorultBox.Name = "AzorultBox";
      this.AzorultBox.Size = new Size(26, 26);
      this.AzorultBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.AzorultBox.TabIndex = 1;
      this.AzorultBox.TabStop = false;
      this.AzorultBox.Click += new EventHandler(this.AzorultBox_Click);
      this.SettingsImg.Image = (Image) componentResourceManager.GetObject("SettingsImg.Image");
      this.SettingsImg.Location = new Point(22, 55);
      this.SettingsImg.Name = "SettingsImg";
      this.SettingsImg.Size = new Size(625, 391);
      this.SettingsImg.SizeMode = PictureBoxSizeMode.AutoSize;
      this.SettingsImg.TabIndex = 0;
      this.SettingsImg.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImage = (Image) componentResourceManager.GetObject("$this.BackgroundImage");
      this.ClientSize = new Size(800, 542);
      this.Controls.Add((Control) this.LogoAnim);
      this.Controls.Add((Control) this.LOG);
      this.Controls.Add((Control) this.AuthorBtn);
      this.Controls.Add((Control) this.SettingsBtn);
      this.Controls.Add((Control) this.DashBtn);
      this.Controls.Add((Control) this.HideBtn);
      this.Controls.Add((Control) this.ExitBtn);
      this.Controls.Add((Control) this.Panels);
      this.Controls.Add((Control) this.SettingsPanel);
      this.Controls.Add((Control) this.AuthorPanel);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (SorterX);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (SorterX);
      this.MouseDown += new MouseEventHandler(this.SorterX_MouseDown);
      this.MouseMove += new MouseEventHandler(this.SorterX_MouseMove);
      this.MouseUp += new MouseEventHandler(this.SorterX_MouseUp);
      ((ISupportInitialize) this.ExitBtn).EndInit();
      ((ISupportInitialize) this.HideBtn).EndInit();
      ((ISupportInitialize) this.DashBtn).EndInit();
      ((ISupportInitialize) this.SettingsBtn).EndInit();
      ((ISupportInitialize) this.AuthorBtn).EndInit();
      ((ISupportInitialize) this.LogoAnim).EndInit();
      this.AuthorPanel.ResumeLayout(false);
      this.AuthorPanel.PerformLayout();
      ((ISupportInitialize) this.ExploitBtn).EndInit();
      ((ISupportInitialize) this.DonateBtn).EndInit();
      ((ISupportInitialize) this.AuthorBox).EndInit();
      this.Panels.ResumeLayout(false);
      this.Panels.PerformLayout();
      ((ISupportInitialize) this.FoundPanel).EndInit();
      ((ISupportInitialize) this.LogPanel).EndInit();
      this.SettingsPanel.ResumeLayout(false);
      this.SettingsPanel.PerformLayout();
      ((ISupportInitialize) this.FolderBox).EndInit();
      ((ISupportInitialize) this.WalletsBox).EndInit();
      ((ISupportInitialize) this.BaseBox).EndInit();
      ((ISupportInitialize) this.StandartBox).EndInit();
      ((ISupportInitialize) this.PredatorBox).EndInit();
      ((ISupportInitialize) this.SmokeBox).EndInit();
      ((ISupportInitialize) this.KrotBox).EndInit();
      ((ISupportInitialize) this.AzorultBox).EndInit();
      ((ISupportInitialize) this.SettingsImg).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
