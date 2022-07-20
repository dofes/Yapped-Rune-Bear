using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Chomp.Properties;

namespace Chomp.Forms;

public class NewRow : Form
{
	public int ResultID;

	public string ResultName;

	private IContainer components;

	private NumericUpDown nudID;

	private Button btnCreate;

	private Button btnCancel;

	private TextBox txtName;

	private Label lblID;

	private Label lblName;

	private Label label1;

	private Label label2;

	private NumericUpDown textbox_RepeatCount;

	private NumericUpDown textbox_StepValue;

	public NewRow(string prompt)
	{
		InitializeComponent();
		Text = prompt;
		base.DialogResult = DialogResult.Cancel;
		textbox_RepeatCount.Text = Settings.Default.NewRow_RepeatCount.ToString();
		textbox_StepValue.Text = Settings.Default.NewRow_StepValue.ToString();
	}

	public NewRow(string prompt, int row_id, string row_name)
	{
		InitializeComponent();
		Text = prompt;
		nudID.Text = row_id.ToString();
		txtName.Text = row_name.ToString();
		base.DialogResult = DialogResult.Cancel;
	}

	private void btnCreate_Click(object sender, EventArgs e)
	{
		ResultID = (int)nudID.Value;
		ResultName = ((txtName.Text.Length > 0) ? txtName.Text : null);
		Settings.Default.NewRow_RepeatCount = Convert.ToInt32(textbox_RepeatCount.Text);
		Settings.Default.NewRow_StepValue = Convert.ToInt32(textbox_StepValue.Text);
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void nudID_Enter(object sender, EventArgs e)
	{
		nudID.Select(0, nudID.Text.Length);
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
		this.lblID = new System.Windows.Forms.Label();
		this.lblName = new System.Windows.Forms.Label();
		this.nudID = new System.Windows.Forms.NumericUpDown();
		this.btnCreate = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.txtName = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.textbox_RepeatCount = new System.Windows.Forms.NumericUpDown();
		this.textbox_StepValue = new System.Windows.Forms.NumericUpDown();
		((System.ComponentModel.ISupportInitialize)this.nudID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.textbox_RepeatCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.textbox_StepValue).BeginInit();
		base.SuspendLayout();
		this.lblID.AutoSize = true;
		this.lblID.Location = new System.Drawing.Point(12, 9);
		this.lblID.Name = "lblID";
		this.lblID.Size = new System.Drawing.Size(18, 13);
		this.lblID.TabIndex = 4;
		this.lblID.Text = "ID";
		this.lblName.AutoSize = true;
		this.lblName.Location = new System.Drawing.Point(12, 87);
		this.lblName.Name = "lblName";
		this.lblName.Size = new System.Drawing.Size(81, 13);
		this.lblName.TabIndex = 5;
		this.lblName.Text = "Name (optional)";
		this.nudID.Location = new System.Drawing.Point(12, 25);
		this.nudID.Maximum = new decimal(new int[4] { 1215752192, 23, 0, 0 });
		this.nudID.Name = "nudID";
		this.nudID.Size = new System.Drawing.Size(210, 20);
		this.nudID.TabIndex = 0;
		this.nudID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudID.Enter += new System.EventHandler(nudID_Enter);
		this.btnCreate.Location = new System.Drawing.Point(15, 129);
		this.btnCreate.Name = "btnCreate";
		this.btnCreate.Size = new System.Drawing.Size(75, 23);
		this.btnCreate.TabIndex = 2;
		this.btnCreate.Text = "Create";
		this.btnCreate.UseVisualStyleBackColor = true;
		this.btnCreate.Click += new System.EventHandler(btnCreate_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(147, 129);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.txtName.Location = new System.Drawing.Point(12, 103);
		this.txtName.Name = "txtName";
		this.txtName.Size = new System.Drawing.Size(210, 20);
		this.txtName.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 48);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(73, 13);
		this.label1.TabIndex = 6;
		this.label1.Text = "Repeat Count";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(119, 48);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(59, 13);
		this.label2.TabIndex = 7;
		this.label2.Text = "Step Value";
		this.textbox_RepeatCount.Location = new System.Drawing.Point(12, 64);
		this.textbox_RepeatCount.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.textbox_RepeatCount.Name = "textbox_RepeatCount";
		this.textbox_RepeatCount.Size = new System.Drawing.Size(100, 20);
		this.textbox_RepeatCount.TabIndex = 8;
		this.textbox_RepeatCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.textbox_StepValue.Location = new System.Drawing.Point(122, 64);
		this.textbox_StepValue.Maximum = new decimal(new int[4] { 100000, 0, 0, 0 });
		this.textbox_StepValue.Name = "textbox_StepValue";
		this.textbox_StepValue.Size = new System.Drawing.Size(100, 20);
		this.textbox_StepValue.TabIndex = 9;
		this.textbox_StepValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		base.AcceptButton = this.btnCreate;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(236, 162);
		base.Controls.Add(this.textbox_StepValue);
		base.Controls.Add(this.textbox_RepeatCount);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.lblName);
		base.Controls.Add(this.lblID);
		base.Controls.Add(this.txtName);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnCreate);
		base.Controls.Add(this.nudID);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "NewRow";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "FormNewRow";
		((System.ComponentModel.ISupportInitialize)this.nudID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.textbox_RepeatCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.textbox_StepValue).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
