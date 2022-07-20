using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chomp.Properties;

namespace Chomp.Forms;

public class DataSettings : Form
{
	private IContainer components;

	private GroupBox groupbox_Data;

	private CheckBox checkbox_UnfurledCSVExport;

	private CheckBox checkbox_ExportUniqueOnly;

	private CheckBox checkbox_IncludeRowNames;

	private CheckBox checkbox_IncludeHeader;

	private Label label_CSV_Delimiter;

	private TextBox textbox_CSV_Delimiter;

	private Button btnSaveSettings;

	private Button btnCancel;

	private GroupBox groupBox1;

	private CheckBox checkbox_EnableFieldValidation;

	public DataSettings()
	{
		InitializeComponent();
		checkbox_IncludeHeader.Checked = Settings.Default.IncludeHeaderInCSV;
		checkbox_IncludeRowNames.Checked = Settings.Default.IncludeRowNameInCSV;
		checkbox_ExportUniqueOnly.Checked = Settings.Default.ExportUniqueOnly;
		checkbox_UnfurledCSVExport.Checked = Settings.Default.VerboseCSVExport;
		textbox_CSV_Delimiter.Text = Settings.Default.ExportDelimiter;
		checkbox_EnableFieldValidation.Checked = Settings.Default.EnableFieldValidation;
	}

	private void btnSaveSettings_Click(object sender, EventArgs e)
	{
		if (textbox_CSV_Delimiter.Text == "")
		{
			textbox_CSV_Delimiter.Text = ";";
			MessageBox.Show("CSV Delimiter cannot be blank. It has been reset to ;", "Settings", MessageBoxButtons.OK);
		}
		Settings.Default.ExportDelimiter = textbox_CSV_Delimiter.Text;
		Settings.Default.IncludeHeaderInCSV = checkbox_IncludeHeader.Checked;
		Settings.Default.IncludeRowNameInCSV = checkbox_IncludeRowNames.Checked;
		Settings.Default.ExportUniqueOnly = checkbox_ExportUniqueOnly.Checked;
		Settings.Default.VerboseCSVExport = checkbox_UnfurledCSVExport.Checked;
		Settings.Default.EnableFieldValidation = checkbox_EnableFieldValidation.Checked;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chomp.Forms.DataSettings));
		this.groupbox_Data = new System.Windows.Forms.GroupBox();
		this.checkbox_UnfurledCSVExport = new System.Windows.Forms.CheckBox();
		this.checkbox_ExportUniqueOnly = new System.Windows.Forms.CheckBox();
		this.checkbox_IncludeRowNames = new System.Windows.Forms.CheckBox();
		this.checkbox_IncludeHeader = new System.Windows.Forms.CheckBox();
		this.label_CSV_Delimiter = new System.Windows.Forms.Label();
		this.textbox_CSV_Delimiter = new System.Windows.Forms.TextBox();
		this.btnSaveSettings = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.checkbox_EnableFieldValidation = new System.Windows.Forms.CheckBox();
		this.groupbox_Data.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupbox_Data.Controls.Add(this.checkbox_UnfurledCSVExport);
		this.groupbox_Data.Controls.Add(this.checkbox_ExportUniqueOnly);
		this.groupbox_Data.Controls.Add(this.checkbox_IncludeRowNames);
		this.groupbox_Data.Controls.Add(this.checkbox_IncludeHeader);
		this.groupbox_Data.Controls.Add(this.label_CSV_Delimiter);
		this.groupbox_Data.Controls.Add(this.textbox_CSV_Delimiter);
		this.groupbox_Data.Location = new System.Drawing.Point(12, 53);
		this.groupbox_Data.Name = "groupbox_Data";
		this.groupbox_Data.Size = new System.Drawing.Size(255, 153);
		this.groupbox_Data.TabIndex = 32;
		this.groupbox_Data.TabStop = false;
		this.groupbox_Data.Text = "Data Export";
		this.checkbox_UnfurledCSVExport.AutoSize = true;
		this.checkbox_UnfurledCSVExport.Location = new System.Drawing.Point(6, 127);
		this.checkbox_UnfurledCSVExport.Name = "checkbox_UnfurledCSVExport";
		this.checkbox_UnfurledCSVExport.Size = new System.Drawing.Size(169, 17);
		this.checkbox_UnfurledCSVExport.TabIndex = 10;
		this.checkbox_UnfurledCSVExport.Text = "Unfurl Output in Exported CSV";
		this.checkbox_UnfurledCSVExport.UseVisualStyleBackColor = true;
		this.checkbox_ExportUniqueOnly.AutoSize = true;
		this.checkbox_ExportUniqueOnly.Location = new System.Drawing.Point(6, 104);
		this.checkbox_ExportUniqueOnly.Name = "checkbox_ExportUniqueOnly";
		this.checkbox_ExportUniqueOnly.Size = new System.Drawing.Size(154, 17);
		this.checkbox_ExportUniqueOnly.TabIndex = 9;
		this.checkbox_ExportUniqueOnly.Text = "Collate Unique Values Only";
		this.checkbox_ExportUniqueOnly.UseVisualStyleBackColor = true;
		this.checkbox_IncludeRowNames.AutoSize = true;
		this.checkbox_IncludeRowNames.Location = new System.Drawing.Point(6, 61);
		this.checkbox_IncludeRowNames.Name = "checkbox_IncludeRowNames";
		this.checkbox_IncludeRowNames.Size = new System.Drawing.Size(202, 17);
		this.checkbox_IncludeRowNames.TabIndex = 6;
		this.checkbox_IncludeRowNames.Text = "Include Row Names in Exported CSV";
		this.checkbox_IncludeRowNames.UseVisualStyleBackColor = true;
		this.checkbox_IncludeHeader.AutoSize = true;
		this.checkbox_IncludeHeader.Location = new System.Drawing.Point(6, 82);
		this.checkbox_IncludeHeader.Name = "checkbox_IncludeHeader";
		this.checkbox_IncludeHeader.Size = new System.Drawing.Size(204, 17);
		this.checkbox_IncludeHeader.TabIndex = 5;
		this.checkbox_IncludeHeader.Text = "Include Header Row in Exported CSV";
		this.checkbox_IncludeHeader.UseVisualStyleBackColor = true;
		this.label_CSV_Delimiter.AutoSize = true;
		this.label_CSV_Delimiter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label_CSV_Delimiter.Location = new System.Drawing.Point(6, 19);
		this.label_CSV_Delimiter.Name = "label_CSV_Delimiter";
		this.label_CSV_Delimiter.Size = new System.Drawing.Size(71, 13);
		this.label_CSV_Delimiter.TabIndex = 7;
		this.label_CSV_Delimiter.Text = "CSV Delimiter";
		this.textbox_CSV_Delimiter.Location = new System.Drawing.Point(6, 35);
		this.textbox_CSV_Delimiter.Name = "textbox_CSV_Delimiter";
		this.textbox_CSV_Delimiter.Size = new System.Drawing.Size(238, 20);
		this.textbox_CSV_Delimiter.TabIndex = 8;
		this.btnSaveSettings.Location = new System.Drawing.Point(12, 212);
		this.btnSaveSettings.Name = "btnSaveSettings";
		this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
		this.btnSaveSettings.TabIndex = 33;
		this.btnSaveSettings.Text = "Save";
		this.btnSaveSettings.UseVisualStyleBackColor = true;
		this.btnSaveSettings.Click += new System.EventHandler(btnSaveSettings_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(192, 212);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 34;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.groupBox1.Controls.Add(this.checkbox_EnableFieldValidation);
		this.groupBox1.Location = new System.Drawing.Point(12, 6);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(255, 41);
		this.groupBox1.TabIndex = 33;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Yapped";
		this.checkbox_EnableFieldValidation.AutoSize = true;
		this.checkbox_EnableFieldValidation.Location = new System.Drawing.Point(6, 19);
		this.checkbox_EnableFieldValidation.Name = "checkbox_EnableFieldValidation";
		this.checkbox_EnableFieldValidation.Size = new System.Drawing.Size(133, 17);
		this.checkbox_EnableFieldValidation.TabIndex = 6;
		this.checkbox_EnableFieldValidation.Text = "Enable Field Validation";
		this.checkbox_EnableFieldValidation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.checkbox_EnableFieldValidation.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(275, 244);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnSaveSettings);
		base.Controls.Add(this.groupbox_Data);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "DataSettings";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Data Settings";
		this.groupbox_Data.ResumeLayout(false);
		this.groupbox_Data.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
