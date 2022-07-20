using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Chomp.Forms;

public class FindField : Form
{
	public string ResultPattern;

	private IContainer components;

	private Button btnFind;

	private Button btnCancel;

	private TextBox txtPattern;

	public FindField(string prompt)
	{
		InitializeComponent();
		Text = prompt;
		base.DialogResult = DialogResult.Cancel;
	}

	private void btnFind_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		ResultPattern = txtPattern.Text;
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
		this.btnFind = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.txtPattern = new System.Windows.Forms.TextBox();
		base.SuspendLayout();
		this.btnFind.Location = new System.Drawing.Point(12, 38);
		this.btnFind.Name = "btnFind";
		this.btnFind.Size = new System.Drawing.Size(75, 23);
		this.btnFind.TabIndex = 1;
		this.btnFind.Text = "Find";
		this.btnFind.UseVisualStyleBackColor = true;
		this.btnFind.Click += new System.EventHandler(btnFind_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(93, 38);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.txtPattern.Location = new System.Drawing.Point(12, 12);
		this.txtPattern.Name = "txtPattern";
		this.txtPattern.Size = new System.Drawing.Size(156, 20);
		this.txtPattern.TabIndex = 0;
		base.AcceptButton = this.btnFind;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(180, 73);
		base.Controls.Add(this.txtPattern);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnFind);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "FormFind";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Find row with name...";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
