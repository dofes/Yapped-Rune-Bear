using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chomp.Properties;

namespace Chomp.Forms;

public class FieldExporter : Form
{
	private IContainer components;

	private Button btnCreate;

	private Button btnCancel;

	private TextBox textbox_FieldMatch;

	private TextBox textbox_FieldMinimum;

	private TextBox textbox_FieldMaximum;

	private TextBox textbox_FieldExclusions;

	private TextBox textbox_FieldInclusions;

	private ToolTip toolTip_fieldlist;

	private ToolTip toolTip_field_minimums;

	private ToolTip toolTip_field_maximums;

	private ToolTip toolTip_strictMatching;

	private ToolTip toolTip_FieldExclusions;

	private ToolTip toolTip_FieldInclusions;

	private ToolTip toolTip_RetainFieldText;

	private CheckBox checkbox_RetainFieldText;

	public FieldExporter()
	{
		InitializeComponent();
		base.DialogResult = DialogResult.Cancel;
		checkbox_RetainFieldText.Checked = Settings.Default.FieldExporter_RetainFieldText;
		if (checkbox_RetainFieldText.Checked)
		{
			textbox_FieldMatch.Text = Settings.Default.FieldExporter_FieldMatch;
			textbox_FieldMinimum.Text = Settings.Default.FieldExporter_FieldMinimum;
			textbox_FieldMaximum.Text = Settings.Default.FieldExporter_FieldMaximum;
			textbox_FieldExclusions.Text = Settings.Default.FieldExporter_FieldExclusion;
			textbox_FieldInclusions.Text = Settings.Default.FieldExporter_FieldInclusion;
		}
		else
		{
			textbox_FieldMatch.Text = "";
			textbox_FieldMinimum.Text = "";
			textbox_FieldMaximum.Text = "";
			textbox_FieldExclusions.Text = "";
			textbox_FieldInclusions.Text = "";
		}
	}

	private void btnCreate_Click(object sender, EventArgs e)
	{
		Settings.Default.FieldExporter_FieldMatch = textbox_FieldMatch.Text;
		Settings.Default.FieldExporter_FieldMinimum = textbox_FieldMinimum.Text;
		Settings.Default.FieldExporter_FieldMaximum = textbox_FieldMaximum.Text;
		Settings.Default.FieldExporter_FieldExclusion = textbox_FieldExclusions.Text;
		Settings.Default.FieldExporter_FieldInclusion = textbox_FieldInclusions.Text;
		Settings.Default.FieldExporter_RetainFieldText = checkbox_RetainFieldText.Checked;
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
		this.components = new System.ComponentModel.Container();
		this.btnCreate = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.textbox_FieldMatch = new System.Windows.Forms.TextBox();
		this.textbox_FieldMinimum = new System.Windows.Forms.TextBox();
		this.textbox_FieldMaximum = new System.Windows.Forms.TextBox();
		this.textbox_FieldExclusions = new System.Windows.Forms.TextBox();
		this.textbox_FieldInclusions = new System.Windows.Forms.TextBox();
		this.toolTip_fieldlist = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip_field_minimums = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip_field_maximums = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip_strictMatching = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip_FieldExclusions = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip_FieldInclusions = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip_RetainFieldText = new System.Windows.Forms.ToolTip(this.components);
		this.checkbox_RetainFieldText = new System.Windows.Forms.CheckBox();
		System.Windows.Forms.Label lblName = new System.Windows.Forms.Label();
		System.Windows.Forms.Label label_fieldMinimum = new System.Windows.Forms.Label();
		System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
		System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();
		System.Windows.Forms.Label label3 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		lblName.AutoSize = true;
		lblName.Location = new System.Drawing.Point(12, 9);
		lblName.Name = "lblName";
		lblName.Size = new System.Drawing.Size(74, 13);
		lblName.TabIndex = 5;
		lblName.Text = "Field to Export";
		this.toolTip_fieldlist.SetToolTip(lblName, "Field to export. Order of operations: Field Minimum, Field Maximum, Field Inclusion, Field Exclusion");
		label_fieldMinimum.AutoSize = true;
		label_fieldMinimum.Location = new System.Drawing.Point(9, 48);
		label_fieldMinimum.Name = "label_fieldMinimum";
		label_fieldMinimum.Size = new System.Drawing.Size(73, 13);
		label_fieldMinimum.TabIndex = 6;
		label_fieldMinimum.Text = "Field Minimum";
		this.toolTip_field_minimums.SetToolTip(label_fieldMinimum, "Lowest value allowed for inclusion in export. Ignored if blank.");
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(95, 48);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(76, 13);
		label1.TabIndex = 7;
		label1.Text = "Field Maximum";
		this.toolTip_field_maximums.SetToolTip(label1, "Highest value allowed for inclusion in export. Ignored if blank.");
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(270, 48);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(77, 13);
		label2.TabIndex = 12;
		label2.Text = "Field Exclusion";
		this.toolTip_FieldExclusions.SetToolTip(label2, "Ignore values that match this in export. Ignored if blank.");
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(184, 48);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(74, 13);
		label3.TabIndex = 14;
		label3.Text = "Field Inclusion";
		this.toolTip_FieldInclusions.SetToolTip(label3, "Only include values that match this in the export. Ignored if blank.");
		this.btnCreate.Location = new System.Drawing.Point(12, 99);
		this.btnCreate.Name = "btnCreate";
		this.btnCreate.Size = new System.Drawing.Size(75, 23);
		this.btnCreate.TabIndex = 2;
		this.btnCreate.Text = "OK";
		this.btnCreate.UseVisualStyleBackColor = true;
		this.btnCreate.Click += new System.EventHandler(btnCreate_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(281, 99);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.textbox_FieldMatch.Location = new System.Drawing.Point(12, 25);
		this.textbox_FieldMatch.Name = "textbox_FieldMatch";
		this.textbox_FieldMatch.Size = new System.Drawing.Size(341, 20);
		this.textbox_FieldMatch.TabIndex = 1;
		this.textbox_FieldMinimum.Location = new System.Drawing.Point(12, 64);
		this.textbox_FieldMinimum.Name = "textbox_FieldMinimum";
		this.textbox_FieldMinimum.Size = new System.Drawing.Size(80, 20);
		this.textbox_FieldMinimum.TabIndex = 8;
		this.textbox_FieldMaximum.Location = new System.Drawing.Point(98, 64);
		this.textbox_FieldMaximum.Name = "textbox_FieldMaximum";
		this.textbox_FieldMaximum.Size = new System.Drawing.Size(80, 20);
		this.textbox_FieldMaximum.TabIndex = 9;
		this.textbox_FieldExclusions.Location = new System.Drawing.Point(273, 64);
		this.textbox_FieldExclusions.Name = "textbox_FieldExclusions";
		this.textbox_FieldExclusions.Size = new System.Drawing.Size(80, 20);
		this.textbox_FieldExclusions.TabIndex = 11;
		this.textbox_FieldInclusions.Location = new System.Drawing.Point(187, 64);
		this.textbox_FieldInclusions.Name = "textbox_FieldInclusions";
		this.textbox_FieldInclusions.Size = new System.Drawing.Size(80, 20);
		this.textbox_FieldInclusions.TabIndex = 13;
		this.checkbox_RetainFieldText.AutoSize = true;
		this.checkbox_RetainFieldText.Location = new System.Drawing.Point(98, 103);
		this.checkbox_RetainFieldText.Name = "checkbox_RetainFieldText";
		this.checkbox_RetainFieldText.Size = new System.Drawing.Size(106, 17);
		this.checkbox_RetainFieldText.TabIndex = 15;
		this.checkbox_RetainFieldText.Text = "Retain Field Text";
		this.toolTip_RetainFieldText.SetToolTip(this.checkbox_RetainFieldText, "Tick to save the text in fields between uses.");
		this.checkbox_RetainFieldText.UseVisualStyleBackColor = true;
		base.AcceptButton = this.btnCreate;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(368, 133);
		base.Controls.Add(this.checkbox_RetainFieldText);
		base.Controls.Add(label3);
		base.Controls.Add(this.textbox_FieldInclusions);
		base.Controls.Add(label2);
		base.Controls.Add(this.textbox_FieldExclusions);
		base.Controls.Add(this.textbox_FieldMaximum);
		base.Controls.Add(this.textbox_FieldMinimum);
		base.Controls.Add(label1);
		base.Controls.Add(label_fieldMinimum);
		base.Controls.Add(lblName);
		base.Controls.Add(this.textbox_FieldMatch);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnCreate);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "FormFieldExporter";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Field Exporter";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
