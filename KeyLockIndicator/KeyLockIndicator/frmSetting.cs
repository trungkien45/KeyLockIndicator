using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace KeyLockIndicator
{
	public class frmSetting : Form
	{
		private Label lbNum;

		private Label lbCaps;

		private Label lbScroll;

		private RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);

		private IContainer components;

		private CheckBox checkBox1;

		private Button btScroll;

		private Button btNum;

		private Button btCaps;

		private Button button1;

		private CheckBox cbNum;

		private CheckBox cbScroll;

		private CheckBox cbCaps;

		public frmSetting(Label lbNum, Label lbCaps, Label lbScroll)
		{
			InitializeComponent();
			this.lbCaps = lbCaps;
			btCaps.BackColor = lbCaps.ForeColor;
			cbCaps.Checked = (btCaps.Visible = lbCaps.Visible);
			this.lbNum = lbNum;
			btNum.BackColor = lbNum.ForeColor;
			cbNum.Checked = (btNum.Visible = lbNum.Visible);
			this.lbScroll = lbScroll;
			btScroll.BackColor = lbScroll.ForeColor;
			cbScroll.Checked = (btScroll.Visible = lbScroll.Visible);
		}

		private void frmSetting_Load(object sender, EventArgs e)
		{
			checkBox1.Checked = rkApp.GetValue("MyApp") != null;
		}

		private void btNum_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				btNum.BackColor = colorDialog.Color;
				lbNum.ForeColor = colorDialog.Color;
			}
		}

		private void btCaps_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				btCaps.BackColor = colorDialog.Color;
				lbCaps.ForeColor = colorDialog.Color;
			}
		}

		private void btScroll_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				btScroll.BackColor = colorDialog.Color;
				lbScroll.ForeColor = colorDialog.Color;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.Combine("KeyLockIndicator", "settings.setting"));
			string[] contents = new string[6]
			{
				ColorTranslator.ToHtml(btNum.BackColor),
				ColorTranslator.ToHtml(btCaps.BackColor),
				ColorTranslator.ToHtml(btScroll.BackColor),
				cbNum.Checked.ToString(),
				cbCaps.Checked.ToString(),
				cbScroll.Checked.ToString()
			};
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			File.WriteAllLines(path, contents);
			if (checkBox1.Checked)
			{
				rkApp.SetValue("MyApp", Application.ExecutablePath);
			}
			else
			{
				rkApp.DeleteValue("MyApp", throwOnMissingValue: false);
			}
			Close();
		}

		private void cbNum_CheckedChanged(object sender, EventArgs e)
		{
			Label label = lbNum;
			bool visible = (btNum.Visible = cbNum.Checked);
			label.Visible = visible;
		}

		private void cbCaps_CheckedChanged(object sender, EventArgs e)
		{
			Label label = lbCaps;
			bool visible = (btCaps.Visible = cbCaps.Checked);
			label.Visible = visible;
		}

		private void cbScroll_CheckedChanged(object sender, EventArgs e)
		{
			Label label = lbScroll;
			bool visible = (btScroll.Visible = cbScroll.Checked);
			label.Visible = visible;
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
			checkBox1 = new System.Windows.Forms.CheckBox();
			btScroll = new System.Windows.Forms.Button();
			btNum = new System.Windows.Forms.Button();
			btCaps = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			cbNum = new System.Windows.Forms.CheckBox();
			cbScroll = new System.Windows.Forms.CheckBox();
			cbCaps = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			checkBox1.AutoSize = true;
			checkBox1.Location = new System.Drawing.Point(13, 11);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new System.Drawing.Size(117, 17);
			checkBox1.TabIndex = 0;
			checkBox1.Text = "Start with Windows";
			checkBox1.UseVisualStyleBackColor = true;
			btScroll.Location = new System.Drawing.Point(12, 93);
			btScroll.Name = "btScroll";
			btScroll.Size = new System.Drawing.Size(96, 23);
			btScroll.TabIndex = 1;
			btScroll.Text = "Scroll lock Color";
			btScroll.UseVisualStyleBackColor = true;
			btScroll.Click += new System.EventHandler(btScroll_Click);
			btNum.Location = new System.Drawing.Point(13, 34);
			btNum.Name = "btNum";
			btNum.Size = new System.Drawing.Size(96, 23);
			btNum.TabIndex = 1;
			btNum.Text = "Num lock Color";
			btNum.UseVisualStyleBackColor = true;
			btNum.Click += new System.EventHandler(btNum_Click);
			btCaps.Location = new System.Drawing.Point(12, 64);
			btCaps.Name = "btCaps";
			btCaps.Size = new System.Drawing.Size(96, 23);
			btCaps.TabIndex = 1;
			btCaps.Text = "Caps lock Color";
			btCaps.UseVisualStyleBackColor = true;
			btCaps.Click += new System.EventHandler(btCaps_Click);
			button1.Location = new System.Drawing.Point(139, 7);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 2;
			button1.Text = "OK";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			cbNum.AutoSize = true;
			cbNum.Checked = true;
			cbNum.CheckState = System.Windows.Forms.CheckState.Checked;
			cbNum.Location = new System.Drawing.Point(111, 38);
			cbNum.Name = "cbNum";
			cbNum.Size = new System.Drawing.Size(101, 17);
			cbNum.TabIndex = 3;
			cbNum.Text = "Show Num lock";
			cbNum.UseVisualStyleBackColor = true;
			cbNum.CheckedChanged += new System.EventHandler(cbNum_CheckedChanged);
			cbScroll.AutoSize = true;
			cbScroll.Checked = true;
			cbScroll.CheckState = System.Windows.Forms.CheckState.Checked;
			cbScroll.Location = new System.Drawing.Point(111, 97);
			cbScroll.Name = "cbScroll";
			cbScroll.Size = new System.Drawing.Size(105, 17);
			cbScroll.TabIndex = 4;
			cbScroll.Text = "Show Scroll lock";
			cbScroll.UseVisualStyleBackColor = true;
			cbScroll.CheckedChanged += new System.EventHandler(cbScroll_CheckedChanged);
			cbCaps.AutoSize = true;
			cbCaps.Checked = true;
			cbCaps.CheckState = System.Windows.Forms.CheckState.Checked;
			cbCaps.Location = new System.Drawing.Point(111, 68);
			cbCaps.Name = "cbCaps";
			cbCaps.Size = new System.Drawing.Size(103, 17);
			cbCaps.TabIndex = 5;
			cbCaps.Text = "Show Caps lock";
			cbCaps.UseVisualStyleBackColor = true;
			cbCaps.CheckedChanged += new System.EventHandler(cbCaps_CheckedChanged);
			base.AcceptButton = button1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(235, 118);
			base.Controls.Add(cbCaps);
			base.Controls.Add(cbScroll);
			base.Controls.Add(cbNum);
			base.Controls.Add(button1);
			base.Controls.Add(btCaps);
			base.Controls.Add(btNum);
			base.Controls.Add(btScroll);
			base.Controls.Add(checkBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmSetting";
			base.ShowInTaskbar = false;
			Text = "Setting";
			base.Load += new System.EventHandler(frmSetting_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
