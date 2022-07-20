using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chomp.Properties;

namespace Chomp.Forms;

public class SettingsMenu : Form
{
	private IContainer components;

	private Button btnSaveSettings;

	private Button btnCancel;

	private CheckBox checkbox_VerifyRowDeletion;

	private ToolTip toolTip1;

	private ToolTip toolTip2;

	private ToolTip toolTip3;

	private ToolTip toolTip4;

	private ToolTip toolTip5;

	private ToolTip toolTip6;

	private ToolTip toolTip7;

	private ToolTip toolTip8;

	private ToolTip toolTip9;

	private Label label1;

	private TextBox textbox_ProjectName;

	private Button button_SelectTextEditor;

	private TextBox textbox_TextEditor;

	private Label label4;

	private OpenFileDialog textEditorPath;

	private CheckBox checkbox_SuppressConfirmations;

	private CheckBox checkbox_UseTextEditor;

	private GroupBox groupbox_General;

	private GroupBox groupbox_Workflow;

	private CheckBox checkbox_SaveNoEncryption;

	private OpenFileDialog secondaryDataPath;

	public SettingsMenu()
	{
		InitializeComponent();
		base.DialogResult = DialogResult.Cancel;
		textbox_ProjectName.Text = Settings.Default.ProjectName;
		textbox_TextEditor.Text = Settings.Default.TextEditorPath.ToString();
		textEditorPath.FileName = Settings.Default.TextEditorPath;
		checkbox_VerifyRowDeletion.Checked = Settings.Default.VerifyRowDeletion;
		checkbox_SuppressConfirmations.Checked = Settings.Default.ShowConfirmationMessages;
		checkbox_UseTextEditor.Checked = Settings.Default.UseTextEditor;
		checkbox_SaveNoEncryption.Checked = Settings.Default.SaveWithoutEncryption;
	}

	private void btnCreate_Click(object sender, EventArgs e)
	{
		if (textbox_ProjectName.Text == "")
		{
			textbox_ProjectName.Text = "ExampleMod";
			MessageBox.Show("Project Name cannot be blank. It has been reset to ExampleMod", "Settings", MessageBoxButtons.OK);
		}
		Settings.Default.VerifyRowDeletion = checkbox_VerifyRowDeletion.Checked;
		Settings.Default.ProjectName = textbox_ProjectName.Text;
		Settings.Default.TextEditorPath = textEditorPath.FileName;
		Settings.Default.ShowConfirmationMessages = checkbox_SuppressConfirmations.Checked;
		Settings.Default.UseTextEditor = checkbox_UseTextEditor.Checked;
		Settings.Default.SaveWithoutEncryption = checkbox_SaveNoEncryption.Checked;
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void label1_Click(object sender, EventArgs e)
	{
	}

	private void FormSettings_Load(object sender, EventArgs e)
	{
	}

	private void button_SelectTextEditor_Click(object sender, EventArgs e)
	{
		textEditorPath.FileName = "";
		if (textEditorPath.ShowDialog() == DialogResult.OK)
		{
			Settings.Default.TextEditorPath = textEditorPath.FileName;
		}
		textbox_TextEditor.Text = Settings.Default.TextEditorPath.ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chomp.Forms.SettingsMenu));
		this.btnSaveSettings = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.checkbox_VerifyRowDeletion = new System.Windows.Forms.CheckBox();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip5 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip6 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip7 = new System.Windows.Forms.ToolTip(this.components);
		this.label1 = new System.Windows.Forms.Label();
		this.toolTip8 = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip9 = new System.Windows.Forms.ToolTip(this.components);
		this.checkbox_SaveNoEncryption = new System.Windows.Forms.CheckBox();
		this.label4 = new System.Windows.Forms.Label();
		this.textbox_ProjectName = new System.Windows.Forms.TextBox();
		this.button_SelectTextEditor = new System.Windows.Forms.Button();
		this.textbox_TextEditor = new System.Windows.Forms.TextBox();
		this.textEditorPath = new System.Windows.Forms.OpenFileDialog();
		this.checkbox_SuppressConfirmations = new System.Windows.Forms.CheckBox();
		this.checkbox_UseTextEditor = new System.Windows.Forms.CheckBox();
		this.groupbox_General = new System.Windows.Forms.GroupBox();
		this.groupbox_Workflow = new System.Windows.Forms.GroupBox();
		this.secondaryDataPath = new System.Windows.Forms.OpenFileDialog();
		this.groupbox_General.SuspendLayout();
		this.groupbox_Workflow.SuspendLayout();
		base.SuspendLayout();
		this.btnSaveSettings.Location = new System.Drawing.Point(13, 253);
		this.btnSaveSettings.Name = "btnSaveSettings";
		this.btnSaveSettings.Size = new System.Drawing.Size(75, 23);
		this.btnSaveSettings.TabIndex = 2;
		this.btnSaveSettings.Text = "Save";
		this.btnSaveSettings.UseVisualStyleBackColor = true;
		this.btnSaveSettings.Click += new System.EventHandler(btnCreate_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(188, 253);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.checkbox_VerifyRowDeletion.AutoSize = true;
		this.checkbox_VerifyRowDeletion.Location = new System.Drawing.Point(6, 84);
		this.checkbox_VerifyRowDeletion.Name = "checkbox_VerifyRowDeletion";
		this.checkbox_VerifyRowDeletion.Size = new System.Drawing.Size(119, 17);
		this.checkbox_VerifyRowDeletion.TabIndex = 10;
		this.checkbox_VerifyRowDeletion.Text = "Verify Row Deletion";
		this.toolTip4.SetToolTip(this.checkbox_VerifyRowDeletion, "Toggle warning before row deletion.");
		this.checkbox_VerifyRowDeletion.UseVisualStyleBackColor = true;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(71, 13);
		this.label1.TabIndex = 17;
		this.label1.Text = "Project Name";
		this.toolTip7.SetToolTip(this.label1, "Define a project name. This is used to isolate mod-specific names so the original Name files are not overwritten.");
		this.label1.Click += new System.EventHandler(label1_Click);
		this.checkbox_SaveNoEncryption.AutoSize = true;
		this.checkbox_SaveNoEncryption.Location = new System.Drawing.Point(6, 61);
		this.checkbox_SaveNoEncryption.Name = "checkbox_SaveNoEncryption";
		this.checkbox_SaveNoEncryption.Size = new System.Drawing.Size(141, 17);
		this.checkbox_SaveNoEncryption.TabIndex = 26;
		this.checkbox_SaveNoEncryption.Text = "Save without Encryption";
		this.checkbox_SaveNoEncryption.UseVisualStyleBackColor = true;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 19);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(95, 13);
		this.label4.TabIndex = 24;
		this.label4.Text = "Current Text Editor";
		this.textbox_ProjectName.Location = new System.Drawing.Point(6, 35);
		this.textbox_ProjectName.Name = "textbox_ProjectName";
		this.textbox_ProjectName.Size = new System.Drawing.Size(238, 20);
		this.textbox_ProjectName.TabIndex = 18;
		this.button_SelectTextEditor.Location = new System.Drawing.Point(140, 33);
		this.button_SelectTextEditor.Name = "button_SelectTextEditor";
		this.button_SelectTextEditor.Size = new System.Drawing.Size(104, 23);
		this.button_SelectTextEditor.TabIndex = 22;
		this.button_SelectTextEditor.Text = "Select Text Editor";
		this.button_SelectTextEditor.UseVisualStyleBackColor = true;
		this.button_SelectTextEditor.Click += new System.EventHandler(button_SelectTextEditor_Click);
		this.textbox_TextEditor.Location = new System.Drawing.Point(6, 35);
		this.textbox_TextEditor.Name = "textbox_TextEditor";
		this.textbox_TextEditor.Size = new System.Drawing.Size(128, 20);
		this.textbox_TextEditor.TabIndex = 23;
		this.textEditorPath.Filter = ".exe|*";
		this.checkbox_SuppressConfirmations.AutoSize = true;
		this.checkbox_SuppressConfirmations.Location = new System.Drawing.Point(6, 61);
		this.checkbox_SuppressConfirmations.Name = "checkbox_SuppressConfirmations";
		this.checkbox_SuppressConfirmations.Size = new System.Drawing.Size(182, 17);
		this.checkbox_SuppressConfirmations.TabIndex = 25;
		this.checkbox_SuppressConfirmations.Text = "Suppress Confirmation Messages";
		this.checkbox_SuppressConfirmations.UseVisualStyleBackColor = true;
		this.checkbox_UseTextEditor.AutoSize = true;
		this.checkbox_UseTextEditor.Location = new System.Drawing.Point(6, 84);
		this.checkbox_UseTextEditor.Name = "checkbox_UseTextEditor";
		this.checkbox_UseTextEditor.Size = new System.Drawing.Size(169, 17);
		this.checkbox_UseTextEditor.TabIndex = 26;
		this.checkbox_UseTextEditor.Text = "Automatically open output files";
		this.checkbox_UseTextEditor.UseVisualStyleBackColor = true;
		this.groupbox_General.Controls.Add(this.checkbox_SaveNoEncryption);
		this.groupbox_General.Controls.Add(this.checkbox_VerifyRowDeletion);
		this.groupbox_General.Controls.Add(this.textbox_ProjectName);
		this.groupbox_General.Controls.Add(this.label1);
		this.groupbox_General.Location = new System.Drawing.Point(12, 12);
		this.groupbox_General.Name = "groupbox_General";
		this.groupbox_General.Size = new System.Drawing.Size(250, 115);
		this.groupbox_General.TabIndex = 30;
		this.groupbox_General.TabStop = false;
		this.groupbox_General.Text = "General";
		this.groupbox_Workflow.Controls.Add(this.checkbox_SuppressConfirmations);
		this.groupbox_Workflow.Controls.Add(this.checkbox_UseTextEditor);
		this.groupbox_Workflow.Controls.Add(this.button_SelectTextEditor);
		this.groupbox_Workflow.Controls.Add(this.label4);
		this.groupbox_Workflow.Controls.Add(this.textbox_TextEditor);
		this.groupbox_Workflow.Location = new System.Drawing.Point(13, 133);
		this.groupbox_Workflow.Name = "groupbox_Workflow";
		this.groupbox_Workflow.Size = new System.Drawing.Size(250, 114);
		this.groupbox_Workflow.TabIndex = 32;
		this.groupbox_Workflow.TabStop = false;
		this.groupbox_Workflow.Text = "Workflow";
		this.secondaryDataPath.Filter = ".bdt|.bin|All files|*";
		base.AcceptButton = this.btnSaveSettings;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(275, 289);
		base.Controls.Add(this.groupbox_Workflow);
		base.Controls.Add(this.groupbox_General);
		base.Controls.Add(this.btnSaveSettings);
		base.Controls.Add(this.btnCancel);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "SettingsMenu";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Settings";
		base.Load += new System.EventHandler(FormSettings_Load);
		this.groupbox_General.ResumeLayout(false);
		this.groupbox_General.PerformLayout();
		this.groupbox_Workflow.ResumeLayout(false);
		this.groupbox_Workflow.PerformLayout();
		base.ResumeLayout(false);
	}
}
