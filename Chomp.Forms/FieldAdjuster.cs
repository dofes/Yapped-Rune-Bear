using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chomp.Properties;

namespace Chomp.Forms;

public class FieldAdjuster : Form
{
	private IContainer components;

	private Button btnSaveSettings;

	private Button btnCancel;

	private Label label1;

	private Label label2;

	private TextBox textbox_FieldMatch;

	private TextBox textbox_Formula;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private ToolTip toolTip1;

	private ToolTip toolTip2;

	private ToolTip toolTip3;

	private ToolTip toolTip4;

	private ToolTip toolTip5;

	private ToolTip toolTip6;

	private ToolTip toolTip7;

	private ToolTip toolTip8;

	private ToolTip toolTip9;

	private ToolTip toolTip10;

	private ToolTip toolTip11;

	private Label label7;

	private Label label8;

	private Label label9;

	private TextBox textbox_RowRange;

	private TextBox textbox_RowPartialMatch;

	private TextBox textbox_FieldMinimum;

	private TextBox textbox_FieldMaximum;

	private Label label10;

	private Label label11;

	private TextBox textbox_FieldExclusion;

	private TextBox textbox_FieldInclusion;

	private Label label12;

	private TextBox textbox_ValueMin;

	private TextBox textbox_ValueMax;

	private CheckBox checkbox_RetainFieldText;

	public FieldAdjuster()
	{
		InitializeComponent();
		base.DialogResult = DialogResult.Cancel;
		if (Settings.Default.FieldAdjuster_RetainFieldText)
		{
			textbox_FieldMatch.Text = Settings.Default.FieldAdjuster_FieldMatch;
			textbox_RowRange.Text = Settings.Default.FieldAdjuster_RowRange;
			textbox_RowPartialMatch.Text = Settings.Default.FieldAdjuster_RowPartialMatch;
			textbox_FieldMinimum.Text = Settings.Default.FieldAdjuster_FieldMinimum;
			textbox_FieldMaximum.Text = Settings.Default.FieldAdjuster_FieldMaximum;
			textbox_FieldExclusion.Text = Settings.Default.FieldAdjuster_FieldExclusion;
			textbox_FieldInclusion.Text = Settings.Default.FieldAdjuster_FieldInclusion;
			textbox_Formula.Text = Settings.Default.FieldAdjuster_Formula;
			textbox_ValueMin.Text = Settings.Default.FieldAdjuster_ValueMin;
			textbox_ValueMax.Text = Settings.Default.FieldAdjuster_ValueMax;
			checkbox_RetainFieldText.Checked = Settings.Default.FieldAdjuster_RetainFieldText;
		}
		else
		{
			textbox_FieldMatch.Text = "";
			textbox_RowRange.Text = "";
			textbox_RowPartialMatch.Text = "";
			textbox_FieldMinimum.Text = "";
			textbox_FieldMaximum.Text = "";
			textbox_FieldExclusion.Text = "";
			textbox_FieldInclusion.Text = "";
			textbox_Formula.Text = "";
			textbox_ValueMin.Text = "";
			textbox_ValueMax.Text = "";
		}
	}

	private void btnCreate_Click(object sender, EventArgs e)
	{
		Settings.Default.FieldAdjuster_FieldMatch = textbox_FieldMatch.Text;
		Settings.Default.FieldAdjuster_RowRange = textbox_RowRange.Text;
		Settings.Default.FieldAdjuster_RowPartialMatch = textbox_RowPartialMatch.Text;
		Settings.Default.FieldAdjuster_FieldMinimum = textbox_FieldMinimum.Text;
		Settings.Default.FieldAdjuster_FieldMaximum = textbox_FieldMaximum.Text;
		Settings.Default.FieldAdjuster_FieldExclusion = textbox_FieldExclusion.Text;
		Settings.Default.FieldAdjuster_FieldInclusion = textbox_FieldInclusion.Text;
		Settings.Default.FieldAdjuster_Formula = textbox_Formula.Text;
		Settings.Default.FieldAdjuster_ValueMin = textbox_ValueMin.Text;
		Settings.Default.FieldAdjuster_ValueMax = textbox_ValueMax.Text;
		Settings.Default.FieldAdjuster_RetainFieldText = checkbox_RetainFieldText.Checked;
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
		this.btnSaveSettings = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.textbox_FieldMatch = new System.Windows.Forms.TextBox();
		this.textbox_Formula = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.textbox_RowRange = new System.Windows.Forms.TextBox();
		this.textbox_RowPartialMatch = new System.Windows.Forms.TextBox();
		this.textbox_FieldMinimum = new System.Windows.Forms.TextBox();
		this.textbox_FieldMaximum = new System.Windows.Forms.TextBox();
		this.textbox_FieldExclusion = new System.Windows.Forms.TextBox();
		this.textbox_FieldInclusion = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.textbox_ValueMin = new System.Windows.Forms.TextBox();
		this.textbox_ValueMax = new System.Windows.Forms.TextBox();
		this.checkbox_RetainFieldText = new System.Windows.Forms.CheckBox();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip6 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip7 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip8 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip9 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip10 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip11 = new System.Windows.Forms.ToolTip(this.components);
		base.SuspendLayout();
		this.btnSaveSettings.Location = new System.Drawing.Point(17, 380);
		this.btnSaveSettings.Name = "btnSaveSettings";
		this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
		this.btnSaveSettings.TabIndex = 2;
		this.btnSaveSettings.Text = "Apply";
		this.btnSaveSettings.UseVisualStyleBackColor = true;
		this.btnSaveSettings.Click += new System.EventHandler(btnCreate_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(147, 380);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 279);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(44, 13);
		this.label1.TabIndex = 4;
		this.label1.Text = "Formula";
		this.toolTip1.SetToolTip(this.label1, "Enter the mathematical formula to apply - Use x for the field value.\nExample: x*3");
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(14, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(73, 13);
		this.label2.TabIndex = 5;
		this.label2.Text = "Field to Adjust";
		this.toolTip2.SetToolTip(this.label2, "Field to apply the adjustment to.\nExample: Scaling: STR");
		this.textbox_FieldMatch.Location = new System.Drawing.Point(17, 25);
		this.textbox_FieldMatch.Name = "textbox_FieldMatch";
		this.textbox_FieldMatch.Size = new System.Drawing.Size(205, 20);
		this.textbox_FieldMatch.TabIndex = 6;
		this.textbox_Formula.Location = new System.Drawing.Point(18, 295);
		this.textbox_Formula.Name = "textbox_Formula";
		this.textbox_Formula.Size = new System.Drawing.Size(205, 20);
		this.textbox_Formula.TabIndex = 7;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(14, 103);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(64, 13);
		this.label3.TabIndex = 10;
		this.label3.Text = "Row Range";
		this.toolTip3.SetToolTip(this.label3, "Specify a row range, use the CSV delimiter to indicate start and end values. \nExample: 1000,9000");
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(120, 103);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(94, 13);
		this.label4.TabIndex = 11;
		this.label4.Text = "Substring End Match";
		this.toolTip4.SetToolTip(this.label4, "Define a substring to match with the end of a row ID. \nExample: 700");
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(122, 328);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(86, 13);
		this.label5.TabIndex = 13;
		this.label5.Text = "Output Maximum";
		this.toolTip5.SetToolTip(this.label5, "Cap final value for field adjustment at this value.");
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(18, 328);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(83, 13);
		this.label6.TabIndex = 14;
		this.label6.Text = "Output Minimum";
		this.toolTip6.SetToolTip(this.label6, "Floor final value for field adjustment at this value.");
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(14, 151);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(73, 13);
		this.label8.TabIndex = 17;
		this.label8.Text = "Field Minimum";
		this.toolTip7.SetToolTip(this.label8, "Define a minimum existing value to apply the formula to.");
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(120, 151);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(76, 13);
		this.label9.TabIndex = 18;
		this.label9.Text = "Field Maximum";
		this.toolTip8.SetToolTip(this.label9, "Define a maximum existing value to apply the formula to.");
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(15, 199);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(77, 13);
		this.label10.TabIndex = 23;
		this.label10.Text = "Field Exclusion";
		this.toolTip9.SetToolTip(this.label10, "Define an existing value to ignore when applying the formula.\nMultiple can be included by using the CSV delimiter to split them.");
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(122, 199);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(74, 13);
		this.label11.TabIndex = 24;
		this.label11.Text = "Field Inclusion";
		this.toolTip10.SetToolTip(this.label11, "Define an existing value to ignore when applying the formula.\nMultiple can be included by using the CSV delimiter to split them.");
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(13, 74);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(75, 20);
		this.label7.TabIndex = 16;
		this.label7.Text = "Selection";
		this.textbox_RowRange.Location = new System.Drawing.Point(17, 119);
		this.textbox_RowRange.Name = "textbox_RowRange";
		this.textbox_RowRange.Size = new System.Drawing.Size(100, 20);
		this.textbox_RowRange.TabIndex = 19;
		this.textbox_RowPartialMatch.Location = new System.Drawing.Point(123, 119);
		this.textbox_RowPartialMatch.Name = "textbox_RowPartialMatch";
		this.textbox_RowPartialMatch.Size = new System.Drawing.Size(100, 20);
		this.textbox_RowPartialMatch.TabIndex = 20;
		this.textbox_FieldMinimum.Location = new System.Drawing.Point(17, 167);
		this.textbox_FieldMinimum.Name = "textbox_FieldMinimum";
		this.textbox_FieldMinimum.Size = new System.Drawing.Size(100, 20);
		this.textbox_FieldMinimum.TabIndex = 21;
		this.textbox_FieldMaximum.Location = new System.Drawing.Point(123, 167);
		this.textbox_FieldMaximum.Name = "textbox_FieldMaximum";
		this.textbox_FieldMaximum.Size = new System.Drawing.Size(100, 20);
		this.textbox_FieldMaximum.TabIndex = 22;
		this.textbox_FieldExclusion.Location = new System.Drawing.Point(17, 215);
		this.textbox_FieldExclusion.Name = "textbox_FieldExclusion";
		this.textbox_FieldExclusion.Size = new System.Drawing.Size(100, 20);
		this.textbox_FieldExclusion.TabIndex = 25;
		this.textbox_FieldInclusion.Location = new System.Drawing.Point(123, 215);
		this.textbox_FieldInclusion.Name = "textbox_FieldInclusion";
		this.textbox_FieldInclusion.Size = new System.Drawing.Size(100, 20);
		this.textbox_FieldInclusion.TabIndex = 26;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(14, 249);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(58, 20);
		this.label12.TabIndex = 27;
		this.label12.Text = "Output";
		this.textbox_ValueMin.Location = new System.Drawing.Point(17, 344);
		this.textbox_ValueMin.Name = "textbox_ValueMin";
		this.textbox_ValueMin.Size = new System.Drawing.Size(100, 20);
		this.textbox_ValueMin.TabIndex = 28;
		this.textbox_ValueMax.Location = new System.Drawing.Point(123, 344);
		this.textbox_ValueMax.Name = "textbox_ValueMax";
		this.textbox_ValueMax.Size = new System.Drawing.Size(100, 20);
		this.textbox_ValueMax.TabIndex = 29;
		this.checkbox_RetainFieldText.AutoSize = true;
		this.checkbox_RetainFieldText.Location = new System.Drawing.Point(17, 51);
		this.checkbox_RetainFieldText.Name = "checkbox_RetainFieldText";
		this.checkbox_RetainFieldText.Size = new System.Drawing.Size(106, 17);
		this.checkbox_RetainFieldText.TabIndex = 30;
		this.checkbox_RetainFieldText.Text = "Retain Field Text";
		this.checkbox_RetainFieldText.UseVisualStyleBackColor = true;
		this.toolTip11.SetToolTip(this.checkbox_RetainFieldText, "Retain the field text between uses.");
		base.AcceptButton = this.btnSaveSettings;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(243, 415);
		base.Controls.Add(this.checkbox_RetainFieldText);
		base.Controls.Add(this.textbox_ValueMax);
		base.Controls.Add(this.textbox_ValueMin);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.textbox_FieldInclusion);
		base.Controls.Add(this.textbox_FieldExclusion);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.textbox_FieldMaximum);
		base.Controls.Add(this.textbox_FieldMinimum);
		base.Controls.Add(this.textbox_RowPartialMatch);
		base.Controls.Add(this.textbox_RowRange);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.textbox_Formula);
		base.Controls.Add(this.textbox_FieldMatch);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnSaveSettings);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "FormFieldAdjuster";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Field Adjuster";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
