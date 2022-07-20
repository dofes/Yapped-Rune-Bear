using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chomp.Properties;

namespace Chomp.Forms;

public class InterfaceSettings : Form
{
	private IContainer components;

	private GroupBox groupbox_Field;

	private Button btnSaveSettings;

	private Button btnCancel;

	private ColorDialog colorDialog_FieldValue_Int;

	private ColorDialog colorDialog_FieldValue_Float;

	private ColorDialog colorDialog_FieldValue_Bool;

	private ColorDialog colorDialog_FieldValue_String;

	private TextBox textBox_FieldColor_String;

	private Button button_FieldColor_String;

	private TextBox textBox_FieldColor_Bool;

	private Button button_FieldColor_Bool;

	private TextBox textBox_FieldColor_Float;

	private Button button_FieldColor_Float;

	private TextBox textBox_FieldColor_Int;

	private Button button_FieldColor_int;

	private GroupBox group_FieldElements;

	private CheckBox checkbox_DisplayEnums;

	private CheckBox checkbox_EnumValueInName;

	private CheckBox checkbox_FieldDescriptions;

	private CheckBox checkbox_BooleanEnumToggle;

	private CheckBox checkbox_customizableEnumToggle;

	public InterfaceSettings()
	{
		InitializeComponent();
		Color int_color = Color.FromArgb(Settings.Default.FieldColor_Int_R, Settings.Default.FieldColor_Int_G, Settings.Default.FieldColor_Int_B);
		Color float_color = Color.FromArgb(Settings.Default.FieldColor_Float_R, Settings.Default.FieldColor_Float_G, Settings.Default.FieldColor_Float_B);
		Color bool_color = Color.FromArgb(Settings.Default.FieldColor_Bool_R, Settings.Default.FieldColor_Bool_G, Settings.Default.FieldColor_Bool_B);
		Color string_color = Color.FromArgb(Settings.Default.FieldColor_String_R, Settings.Default.FieldColor_String_G, Settings.Default.FieldColor_String_B);
		colorDialog_FieldValue_Int.Color = int_color;
		colorDialog_FieldValue_Float.Color = float_color;
		colorDialog_FieldValue_Bool.Color = bool_color;
		colorDialog_FieldValue_String.Color = string_color;
		textBox_FieldColor_Int.BackColor = colorDialog_FieldValue_Int.Color;
		textBox_FieldColor_Float.BackColor = colorDialog_FieldValue_Float.Color;
		textBox_FieldColor_Bool.BackColor = colorDialog_FieldValue_Bool.Color;
		textBox_FieldColor_String.BackColor = colorDialog_FieldValue_String.Color;
		checkbox_DisplayEnums.Checked = Settings.Default.ShowEnums;
		checkbox_EnumValueInName.Checked = Settings.Default.ShowEnumValueInName;
		checkbox_FieldDescriptions.Checked = Settings.Default.ShowFieldDescriptions;
		checkbox_BooleanEnumToggle.Checked = Settings.Default.DisplayBooleanEnumAsCheckbox;
		checkbox_customizableEnumToggle.Checked = Settings.Default.DisableEnumForCustomTypes;
	}

	private void button_FieldColor_int_Click(object sender, EventArgs e)
	{
		colorDialog_FieldValue_Int.AllowFullOpen = true;
		colorDialog_FieldValue_Int.ShowHelp = true;
		colorDialog_FieldValue_Int.Color = textBox_FieldColor_Int.BackColor;
		if (colorDialog_FieldValue_Int.ShowDialog() == DialogResult.OK)
		{
			textBox_FieldColor_Int.BackColor = colorDialog_FieldValue_Int.Color;
		}
	}

	private void button_FieldColor_Float_Click(object sender, EventArgs e)
	{
		colorDialog_FieldValue_Float.AllowFullOpen = true;
		colorDialog_FieldValue_Float.ShowHelp = true;
		colorDialog_FieldValue_Float.Color = textBox_FieldColor_Float.BackColor;
		if (colorDialog_FieldValue_Float.ShowDialog() == DialogResult.OK)
		{
			textBox_FieldColor_Float.BackColor = colorDialog_FieldValue_Float.Color;
		}
	}

	private void button_FieldColor_Bool_Click(object sender, EventArgs e)
	{
		colorDialog_FieldValue_Bool.AllowFullOpen = true;
		colorDialog_FieldValue_Bool.ShowHelp = true;
		colorDialog_FieldValue_Bool.Color = textBox_FieldColor_Bool.BackColor;
		if (colorDialog_FieldValue_Bool.ShowDialog() == DialogResult.OK)
		{
			textBox_FieldColor_Bool.BackColor = colorDialog_FieldValue_Bool.Color;
		}
	}

	private void button_FieldColor_String_Click(object sender, EventArgs e)
	{
		colorDialog_FieldValue_String.AllowFullOpen = true;
		colorDialog_FieldValue_String.ShowHelp = true;
		colorDialog_FieldValue_String.Color = textBox_FieldColor_String.BackColor;
		if (colorDialog_FieldValue_String.ShowDialog() == DialogResult.OK)
		{
			textBox_FieldColor_String.BackColor = colorDialog_FieldValue_String.Color;
		}
	}

	private void btnSaveSettings_Click(object sender, EventArgs e)
	{
		Settings.Default.FieldColor_Int_R = colorDialog_FieldValue_Int.Color.R;
		Settings.Default.FieldColor_Int_G = colorDialog_FieldValue_Int.Color.G;
		Settings.Default.FieldColor_Int_B = colorDialog_FieldValue_Int.Color.B;
		Settings.Default.FieldColor_Float_R = colorDialog_FieldValue_Float.Color.R;
		Settings.Default.FieldColor_Float_G = colorDialog_FieldValue_Float.Color.G;
		Settings.Default.FieldColor_Float_B = colorDialog_FieldValue_Float.Color.B;
		Settings.Default.FieldColor_Bool_R = colorDialog_FieldValue_Bool.Color.R;
		Settings.Default.FieldColor_Bool_G = colorDialog_FieldValue_Bool.Color.G;
		Settings.Default.FieldColor_Bool_B = colorDialog_FieldValue_Bool.Color.B;
		Settings.Default.FieldColor_String_R = colorDialog_FieldValue_String.Color.R;
		Settings.Default.FieldColor_String_G = colorDialog_FieldValue_String.Color.G;
		Settings.Default.FieldColor_String_B = colorDialog_FieldValue_String.Color.B;
		Settings.Default.ShowEnums = checkbox_DisplayEnums.Checked;
		Settings.Default.ShowEnumValueInName = checkbox_EnumValueInName.Checked;
		Settings.Default.ShowFieldDescriptions = checkbox_FieldDescriptions.Checked;
		Settings.Default.DisplayBooleanEnumAsCheckbox = checkbox_BooleanEnumToggle.Checked;
		Settings.Default.DisableEnumForCustomTypes = checkbox_customizableEnumToggle.Checked;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chomp.Forms.InterfaceSettings));
		this.groupbox_Field = new System.Windows.Forms.GroupBox();
		this.textBox_FieldColor_String = new System.Windows.Forms.TextBox();
		this.button_FieldColor_String = new System.Windows.Forms.Button();
		this.textBox_FieldColor_Bool = new System.Windows.Forms.TextBox();
		this.button_FieldColor_Bool = new System.Windows.Forms.Button();
		this.textBox_FieldColor_Float = new System.Windows.Forms.TextBox();
		this.button_FieldColor_Float = new System.Windows.Forms.Button();
		this.textBox_FieldColor_Int = new System.Windows.Forms.TextBox();
		this.button_FieldColor_int = new System.Windows.Forms.Button();
		this.btnSaveSettings = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.colorDialog_FieldValue_Int = new System.Windows.Forms.ColorDialog();
		this.colorDialog_FieldValue_Float = new System.Windows.Forms.ColorDialog();
		this.colorDialog_FieldValue_Bool = new System.Windows.Forms.ColorDialog();
		this.colorDialog_FieldValue_String = new System.Windows.Forms.ColorDialog();
		this.group_FieldElements = new System.Windows.Forms.GroupBox();
		this.checkbox_BooleanEnumToggle = new System.Windows.Forms.CheckBox();
		this.checkbox_FieldDescriptions = new System.Windows.Forms.CheckBox();
		this.checkbox_EnumValueInName = new System.Windows.Forms.CheckBox();
		this.checkbox_DisplayEnums = new System.Windows.Forms.CheckBox();
		this.checkbox_customizableEnumToggle = new System.Windows.Forms.CheckBox();
		this.groupbox_Field.SuspendLayout();
		this.group_FieldElements.SuspendLayout();
		base.SuspendLayout();
		this.groupbox_Field.Controls.Add(this.textBox_FieldColor_String);
		this.groupbox_Field.Controls.Add(this.button_FieldColor_String);
		this.groupbox_Field.Controls.Add(this.textBox_FieldColor_Bool);
		this.groupbox_Field.Controls.Add(this.button_FieldColor_Bool);
		this.groupbox_Field.Controls.Add(this.textBox_FieldColor_Float);
		this.groupbox_Field.Controls.Add(this.button_FieldColor_Float);
		this.groupbox_Field.Controls.Add(this.textBox_FieldColor_Int);
		this.groupbox_Field.Controls.Add(this.button_FieldColor_int);
		this.groupbox_Field.Location = new System.Drawing.Point(12, 12);
		this.groupbox_Field.Name = "groupbox_Field";
		this.groupbox_Field.Size = new System.Drawing.Size(250, 139);
		this.groupbox_Field.TabIndex = 31;
		this.groupbox_Field.TabStop = false;
		this.groupbox_Field.Text = "Field Coloring";
		this.textBox_FieldColor_String.Location = new System.Drawing.Point(87, 108);
		this.textBox_FieldColor_String.Name = "textBox_FieldColor_String";
		this.textBox_FieldColor_String.ReadOnly = true;
		this.textBox_FieldColor_String.Size = new System.Drawing.Size(153, 20);
		this.textBox_FieldColor_String.TabIndex = 7;
		this.textBox_FieldColor_String.Text = "Example";
		this.button_FieldColor_String.Location = new System.Drawing.Point(6, 106);
		this.button_FieldColor_String.Name = "button_FieldColor_String";
		this.button_FieldColor_String.Size = new System.Drawing.Size(75, 23);
		this.button_FieldColor_String.TabIndex = 6;
		this.button_FieldColor_String.Text = "String";
		this.button_FieldColor_String.UseVisualStyleBackColor = true;
		this.button_FieldColor_String.Click += new System.EventHandler(button_FieldColor_String_Click);
		this.textBox_FieldColor_Bool.Location = new System.Drawing.Point(87, 79);
		this.textBox_FieldColor_Bool.Name = "textBox_FieldColor_Bool";
		this.textBox_FieldColor_Bool.ReadOnly = true;
		this.textBox_FieldColor_Bool.Size = new System.Drawing.Size(153, 20);
		this.textBox_FieldColor_Bool.TabIndex = 5;
		this.textBox_FieldColor_Bool.Text = "0";
		this.button_FieldColor_Bool.Location = new System.Drawing.Point(6, 77);
		this.button_FieldColor_Bool.Name = "button_FieldColor_Bool";
		this.button_FieldColor_Bool.Size = new System.Drawing.Size(75, 23);
		this.button_FieldColor_Bool.TabIndex = 4;
		this.button_FieldColor_Bool.Text = "Boolean";
		this.button_FieldColor_Bool.UseVisualStyleBackColor = true;
		this.button_FieldColor_Bool.Click += new System.EventHandler(button_FieldColor_Bool_Click);
		this.textBox_FieldColor_Float.Location = new System.Drawing.Point(87, 50);
		this.textBox_FieldColor_Float.Name = "textBox_FieldColor_Float";
		this.textBox_FieldColor_Float.ReadOnly = true;
		this.textBox_FieldColor_Float.Size = new System.Drawing.Size(153, 20);
		this.textBox_FieldColor_Float.TabIndex = 3;
		this.textBox_FieldColor_Float.Text = "1.05";
		this.button_FieldColor_Float.Location = new System.Drawing.Point(6, 48);
		this.button_FieldColor_Float.Name = "button_FieldColor_Float";
		this.button_FieldColor_Float.Size = new System.Drawing.Size(75, 23);
		this.button_FieldColor_Float.TabIndex = 2;
		this.button_FieldColor_Float.Text = "Float";
		this.button_FieldColor_Float.UseVisualStyleBackColor = true;
		this.button_FieldColor_Float.Click += new System.EventHandler(button_FieldColor_Float_Click);
		this.textBox_FieldColor_Int.Location = new System.Drawing.Point(87, 21);
		this.textBox_FieldColor_Int.Name = "textBox_FieldColor_Int";
		this.textBox_FieldColor_Int.ReadOnly = true;
		this.textBox_FieldColor_Int.Size = new System.Drawing.Size(153, 20);
		this.textBox_FieldColor_Int.TabIndex = 1;
		this.textBox_FieldColor_Int.Text = "1";
		this.button_FieldColor_int.Location = new System.Drawing.Point(6, 19);
		this.button_FieldColor_int.Name = "button_FieldColor_int";
		this.button_FieldColor_int.Size = new System.Drawing.Size(75, 23);
		this.button_FieldColor_int.TabIndex = 0;
		this.button_FieldColor_int.Text = "Integer";
		this.button_FieldColor_int.UseVisualStyleBackColor = true;
		this.button_FieldColor_int.Click += new System.EventHandler(button_FieldColor_int_Click);
		this.btnSaveSettings.Location = new System.Drawing.Point(12, 308);
		this.btnSaveSettings.Name = "btnSaveSettings";
		this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
		this.btnSaveSettings.TabIndex = 32;
		this.btnSaveSettings.Text = "Save";
		this.btnSaveSettings.UseVisualStyleBackColor = true;
		this.btnSaveSettings.Click += new System.EventHandler(btnSaveSettings_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(187, 308);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 33;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.colorDialog_FieldValue_Int.Color = System.Drawing.Color.White;
		this.colorDialog_FieldValue_Float.Color = System.Drawing.Color.White;
		this.colorDialog_FieldValue_Bool.Color = System.Drawing.Color.White;
		this.colorDialog_FieldValue_String.Color = System.Drawing.Color.White;
		this.group_FieldElements.Controls.Add(this.checkbox_customizableEnumToggle);
		this.group_FieldElements.Controls.Add(this.checkbox_BooleanEnumToggle);
		this.group_FieldElements.Controls.Add(this.checkbox_FieldDescriptions);
		this.group_FieldElements.Controls.Add(this.checkbox_EnumValueInName);
		this.group_FieldElements.Controls.Add(this.checkbox_DisplayEnums);
		this.group_FieldElements.Location = new System.Drawing.Point(12, 157);
		this.group_FieldElements.Name = "group_FieldElements";
		this.group_FieldElements.Size = new System.Drawing.Size(250, 145);
		this.group_FieldElements.TabIndex = 34;
		this.group_FieldElements.TabStop = false;
		this.group_FieldElements.Text = "Field Interface";
		this.checkbox_BooleanEnumToggle.AutoSize = true;
		this.checkbox_BooleanEnumToggle.Location = new System.Drawing.Point(7, 89);
		this.checkbox_BooleanEnumToggle.Name = "checkbox_BooleanEnumToggle";
		this.checkbox_BooleanEnumToggle.Size = new System.Drawing.Size(197, 17);
		this.checkbox_BooleanEnumToggle.TabIndex = 21;
		this.checkbox_BooleanEnumToggle.Text = "Display Boolean Enum as Checkbox";
		this.checkbox_BooleanEnumToggle.UseVisualStyleBackColor = true;
		this.checkbox_FieldDescriptions.AutoSize = true;
		this.checkbox_FieldDescriptions.Location = new System.Drawing.Point(7, 112);
		this.checkbox_FieldDescriptions.Name = "checkbox_FieldDescriptions";
		this.checkbox_FieldDescriptions.Size = new System.Drawing.Size(187, 17);
		this.checkbox_FieldDescriptions.TabIndex = 20;
		this.checkbox_FieldDescriptions.Text = "Display Field Description in Tooltip";
		this.checkbox_FieldDescriptions.UseVisualStyleBackColor = true;
		this.checkbox_EnumValueInName.AutoSize = true;
		this.checkbox_EnumValueInName.Location = new System.Drawing.Point(7, 43);
		this.checkbox_EnumValueInName.Name = "checkbox_EnumValueInName";
		this.checkbox_EnumValueInName.Size = new System.Drawing.Size(202, 17);
		this.checkbox_EnumValueInName.TabIndex = 1;
		this.checkbox_EnumValueInName.Text = "Show Enum Value in Selection Name";
		this.checkbox_EnumValueInName.UseVisualStyleBackColor = true;
		this.checkbox_DisplayEnums.AutoSize = true;
		this.checkbox_DisplayEnums.Location = new System.Drawing.Point(7, 20);
		this.checkbox_DisplayEnums.Name = "checkbox_DisplayEnums";
		this.checkbox_DisplayEnums.Size = new System.Drawing.Size(137, 17);
		this.checkbox_DisplayEnums.TabIndex = 0;
		this.checkbox_DisplayEnums.Text = "Display Enum Selection";
		this.checkbox_DisplayEnums.UseVisualStyleBackColor = true;
		this.checkbox_customizableEnumToggle.AutoSize = true;
		this.checkbox_customizableEnumToggle.Location = new System.Drawing.Point(7, 66);
		this.checkbox_customizableEnumToggle.Name = "checkbox_customizableEnumToggle";
		this.checkbox_customizableEnumToggle.Size = new System.Drawing.Size(223, 17);
		this.checkbox_customizableEnumToggle.TabIndex = 22;
		this.checkbox_customizableEnumToggle.Text = "Show Customizable Enum as Normal Field";
		this.checkbox_customizableEnumToggle.UseVisualStyleBackColor = true;
		base.AcceptButton = this.btnSaveSettings;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(272, 348);
		base.Controls.Add(this.group_FieldElements);
		base.Controls.Add(this.btnSaveSettings);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.groupbox_Field);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "InterfaceSettings";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Interface Settings";
		this.groupbox_Field.ResumeLayout(false);
		this.groupbox_Field.PerformLayout();
		this.group_FieldElements.ResumeLayout(false);
		this.group_FieldElements.PerformLayout();
		base.ResumeLayout(false);
	}
}
