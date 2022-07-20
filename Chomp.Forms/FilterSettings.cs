using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chomp.Properties;

namespace Chomp.Forms;

public class FilterSettings : Form
{
	private IContainer components;

	private GroupBox group_Filter;

	private TextBox textbox_Filter_SectionDelimiter;

	private Label label2;

	private TextBox textbox_Filter_CommandDelimiter;

	private Label label1;

	private Button btnSaveSettings;

	private Button btnCancel;

	public FilterSettings()
	{
		InitializeComponent();
		textbox_Filter_CommandDelimiter.Text = Settings.Default.Filter_CommandDelimiter;
		textbox_Filter_SectionDelimiter.Text = Settings.Default.Filter_SectionDelimiter;
	}

	private void btnSaveSettings_Click(object sender, EventArgs e)
	{
		string command_delimiter = textbox_Filter_CommandDelimiter.Text;
		string section_delimiter = textbox_Filter_SectionDelimiter.Text;
		if (command_delimiter == "")
		{
			command_delimiter = ":";
		}
		if (section_delimiter == "")
		{
			section_delimiter = "~";
		}
		Settings.Default.Filter_CommandDelimiter = command_delimiter;
		Settings.Default.Filter_SectionDelimiter = section_delimiter;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
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
		this.group_Filter = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.textbox_Filter_CommandDelimiter = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.textbox_Filter_SectionDelimiter = new System.Windows.Forms.TextBox();
		this.btnSaveSettings = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.group_Filter.SuspendLayout();
		base.SuspendLayout();
		this.group_Filter.Controls.Add(this.textbox_Filter_SectionDelimiter);
		this.group_Filter.Controls.Add(this.label2);
		this.group_Filter.Controls.Add(this.textbox_Filter_CommandDelimiter);
		this.group_Filter.Controls.Add(this.label1);
		this.group_Filter.Location = new System.Drawing.Point(12, 12);
		this.group_Filter.Name = "group_Filter";
		this.group_Filter.Size = new System.Drawing.Size(250, 112);
		this.group_Filter.TabIndex = 35;
		this.group_Filter.TabStop = false;
		this.group_Filter.Text = "Filter ";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(7, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(122, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Filter Command Delimiter";
		this.textbox_Filter_CommandDelimiter.Location = new System.Drawing.Point(9, 37);
		this.textbox_Filter_CommandDelimiter.Name = "textbox_Filter_CommandDelimiter";
		this.textbox_Filter_CommandDelimiter.Size = new System.Drawing.Size(235, 20);
		this.textbox_Filter_CommandDelimiter.TabIndex = 1;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(7, 64);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(111, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Filter Section Delimiter";
		this.textbox_Filter_SectionDelimiter.Location = new System.Drawing.Point(9, 80);
		this.textbox_Filter_SectionDelimiter.Name = "textbox_Filter_SectionDelimiter";
		this.textbox_Filter_SectionDelimiter.Size = new System.Drawing.Size(235, 20);
		this.textbox_Filter_SectionDelimiter.TabIndex = 3;
		this.btnSaveSettings.Location = new System.Drawing.Point(12, 130);
		this.btnSaveSettings.Name = "btnSaveSettings";
		this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
		this.btnSaveSettings.TabIndex = 36;
		this.btnSaveSettings.Text = "Save";
		this.btnSaveSettings.UseVisualStyleBackColor = true;
		this.btnSaveSettings.Click += new System.EventHandler(btnSaveSettings_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(187, 130);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 37;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(273, 167);
		base.Controls.Add(this.btnSaveSettings);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.group_Filter);
		base.Name = "FilterSettings";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Filter Settings";
		this.group_Filter.ResumeLayout(false);
		this.group_Filter.PerformLayout();
		base.ResumeLayout(false);
	}
}
