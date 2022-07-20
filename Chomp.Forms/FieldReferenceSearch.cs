using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Chomp.Forms;

public class FieldReferenceSearch : Form
{
	private IContainer components;

	private Button btnCreate;

	private Button btnCancel;

	private TextBox textbox_referenceText;

	private Label label1;

	public FieldReferenceSearch()
	{
		InitializeComponent();
		base.DialogResult = DialogResult.Cancel;
	}

	private void btnCreate_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	public string GetReferenceText()
	{
		return textbox_referenceText.Text;
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
		this.label1 = new System.Windows.Forms.Label();
		this.btnCreate = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.textbox_referenceText = new System.Windows.Forms.TextBox();
		System.Windows.Forms.Label lblName = new System.Windows.Forms.Label();
		base.SuspendLayout();
		lblName.AutoSize = true;
		lblName.Location = new System.Drawing.Point(12, 9);
		lblName.Name = "lblName";
		lblName.Size = new System.Drawing.Size(79, 13);
		lblName.TabIndex = 5;
		lblName.Text = "Number to Find";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 58);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(0, 13);
		this.label1.TabIndex = 7;
		this.btnCreate.Location = new System.Drawing.Point(12, 53);
		this.btnCreate.Name = "btnCreate";
		this.btnCreate.Size = new System.Drawing.Size(75, 23);
		this.btnCreate.TabIndex = 2;
		this.btnCreate.Text = "OK";
		this.btnCreate.UseVisualStyleBackColor = true;
		this.btnCreate.Click += new System.EventHandler(btnCreate_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(143, 53);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.textbox_referenceText.Location = new System.Drawing.Point(13, 25);
		this.textbox_referenceText.Name = "textbox_referenceText";
		this.textbox_referenceText.Size = new System.Drawing.Size(204, 20);
		this.textbox_referenceText.TabIndex = 6;
		base.AcceptButton = this.btnCreate;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(229, 85);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.textbox_referenceText);
		base.Controls.Add(lblName);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnCreate);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "FormReferenceFinder";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reference Finder";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
