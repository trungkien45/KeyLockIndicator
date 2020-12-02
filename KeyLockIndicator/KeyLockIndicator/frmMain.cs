using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyLockIndicator
{
	public class frmMain : Form
	{
		private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

		private const uint SWP_NOSIZE = 1u;

		private const uint SWP_NOMOVE = 2u;

		private const uint TOPMOST_FLAGS = 3u;

		private frmSetting frmSetting;

		private UserActivityHook actHook;

		private const int WM_NCHITTEST = 132;

		private const int HTTRANSPARENT = -1;

		private IContainer components;

		private Label lbNum;

		private Label lbCaps;

		private Label lbScroll;

		private NotifyIcon notifyIcon1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem exitToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem1;

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		public frmMain()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			SetWindowPos(base.Handle, HWND_TOPMOST, 0, 0, 0, 0, 3u);
			Rectangle workingArea = Screen.GetWorkingArea(this);
			base.Location = new Point(workingArea.Right - base.Size.Width, workingArea.Bottom - base.Size.Height);
			actHook = new UserActivityHook(InstallMouseHook: true, InstallKeyboardHook: true);
			actHook.KeyDown += KeyDowned;
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.Combine("KeyLockIndicator", "settings.setting"));
			if (!File.Exists(path))
			{
				string[] contents = new string[3]
				{
					ColorTranslator.ToHtml(lbNum.ForeColor),
					ColorTranslator.ToHtml(lbCaps.ForeColor),
					ColorTranslator.ToHtml(lbScroll.ForeColor)
				};
				Directory.CreateDirectory(Path.GetDirectoryName(path));
				File.WriteAllLines(path, contents);
			}
			string[] array = File.ReadAllLines(path);
			if (array.Length == 6)
			{
				Color foreColor = ColorTranslator.FromHtml(array[0]);
				Color foreColor2 = ColorTranslator.FromHtml(array[1]);
				Color foreColor3 = ColorTranslator.FromHtml(array[2]);
				lbNum.ForeColor = foreColor;
				lbNum.Visible = bool.Parse(array[3]);
				lbCaps.ForeColor = foreColor2;
				lbCaps.Visible = bool.Parse(array[4]);
				lbScroll.ForeColor = foreColor3;
				lbScroll.Visible = bool.Parse(array[5]);
			}
			if (Control.IsKeyLocked(Keys.NumLock))
			{
				lbNum.Font = new Font(lbNum.Font, FontStyle.Bold);
			}
			if (Control.IsKeyLocked(Keys.Capital))
			{
				lbCaps.Font = new Font(lbCaps.Font, FontStyle.Bold);
			}
			if (Control.IsKeyLocked(Keys.Scroll))
			{
				lbScroll.Font = new Font(lbScroll.Font, FontStyle.Bold);
			}
		}

		private void KeyDowned(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.NumLock)
			{
				if (!Control.IsKeyLocked(Keys.NumLock))
				{
					lbNum.Font = new Font(lbNum.Font, FontStyle.Bold);
				}
				else
				{
					lbNum.Font = new Font(lbNum.Font, FontStyle.Regular);
				}
			}
			if (e.KeyCode == Keys.Capital)
			{
				if (!Control.IsKeyLocked(Keys.Capital))
				{
					lbCaps.Font = new Font(lbCaps.Font, FontStyle.Bold);
				}
				else
				{
					lbCaps.Font = new Font(lbCaps.Font, FontStyle.Regular);
				}
			}
			if (e.KeyCode == Keys.Scroll)
			{
				if (!Control.IsKeyLocked(Keys.Scroll))
				{
					lbScroll.Font = new Font(lbScroll.Font, FontStyle.Bold);
				}
				else
				{
					lbScroll.Font = new Font(lbScroll.Font, FontStyle.Regular);
				}
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (frmSetting == null || frmSetting.IsDisposed)
			{
				frmSetting = new frmSetting(lbNum, lbCaps, lbScroll);
			}
			if (!frmSetting.Visible)
			{
				frmSetting.ShowDialog();
			}
		}

		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
			if (frmSetting == null || frmSetting.IsDisposed)
			{
				frmSetting = new frmSetting(lbNum, lbCaps, lbScroll);
			}
			if (!frmSetting.Visible)
			{
				frmSetting.ShowDialog();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyLockIndicator.frmMain));
			lbNum = new System.Windows.Forms.Label();
			lbCaps = new System.Windows.Forms.Label();
			lbScroll = new System.Windows.Forms.Label();
			notifyIcon1 = new System.Windows.Forms.NotifyIcon(components);
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			lbNum.AutoSize = true;
			lbNum.ForeColor = System.Drawing.Color.Blue;
			lbNum.Location = new System.Drawing.Point(15, 14);
			lbNum.Name = "lbNum";
			lbNum.Size = new System.Drawing.Size(56, 13);
			lbNum.TabIndex = 0;
			lbNum.Text = "Num Lock";
			lbCaps.AutoSize = true;
			lbCaps.ForeColor = System.Drawing.Color.Red;
			lbCaps.Location = new System.Drawing.Point(15, 40);
			lbCaps.Name = "lbCaps";
			lbCaps.Size = new System.Drawing.Size(58, 13);
			lbCaps.TabIndex = 1;
			lbCaps.Text = "Caps Lock";
			lbScroll.AutoSize = true;
			lbScroll.ForeColor = System.Drawing.Color.Green;
			lbScroll.Location = new System.Drawing.Point(15, 65);
			lbScroll.Name = "lbScroll";
			lbScroll.Size = new System.Drawing.Size(60, 13);
			lbScroll.TabIndex = 2;
			lbScroll.Text = "Scroll Lock";
			notifyIcon1.BalloonTipText = "Key Lock Indicator";
			notifyIcon1.BalloonTipTitle = "Key Lock Indicator";
			notifyIcon1.ContextMenuStrip = contextMenuStrip1;
			notifyIcon1.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon1.Icon");
			notifyIcon1.Text = "Key Lock Indicator";
			notifyIcon1.Visible = true;
			notifyIcon1.DoubleClick += new System.EventHandler(notifyIcon1_DoubleClick);
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				toolStripMenuItem1,
				exitToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
			toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
			toolStripMenuItem1.Text = "Setting";
			toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			exitToolStripMenuItem.Text = "Exit";
			exitToolStripMenuItem.Click += new System.EventHandler(exitToolStripMenuItem_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(91, 100);
			base.Controls.Add(lbScroll);
			base.Controls.Add(lbCaps);
			base.Controls.Add(lbNum);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmMain";
			base.ShowInTaskbar = false;
			base.TransparencyKey = System.Drawing.SystemColors.Control;
			base.Load += new System.EventHandler(Form1_Load);
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
