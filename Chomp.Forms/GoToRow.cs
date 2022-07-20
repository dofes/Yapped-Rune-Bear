using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Chomp.Forms;

public class GoToRow : Form
{
	public long ResultID;

	private IContainer components;

	private NumericUpDown nudID;

	private Button btnGoto;

	private Button btnCancel;

	public GoToRow()
	{
		InitializeComponent();
		base.DialogResult = DialogResult.Cancel;
	}

	private void btnGoto_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		ResultID = (long)nudID.Value;
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
		this.nudID = new System.Windows.Forms.NumericUpDown();
		this.btnGoto = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.nudID).BeginInit();
		base.SuspendLayout();
		this.nudID.Location = new System.Drawing.Point(12, 12);
		this.nudID.Maximum = new decimal(new int[4] { 1215752192, 23, 0, 0 });
		this.nudID.Name = "nudID";
		this.nudID.Size = new System.Drawing.Size(156, 20);
		this.nudID.TabIndex = 0;
		this.nudID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudID.Enter += new System.EventHandler(nudID_Enter);
		this.btnGoto.Location = new System.Drawing.Point(12, 38);
		this.btnGoto.Name = "btnGoto";
		this.btnGoto.Size = new System.Drawing.Size(75, 23);
		this.btnGoto.TabIndex = 1;
		this.btnGoto.Text = "Goto";
		this.btnGoto.UseVisualStyleBackColor = true;
		this.btnGoto.Click += new System.EventHandler(btnGoto_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(93, 38);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		base.AcceptButton = this.btnGoto;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(180, 73);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnGoto);
		base.Controls.Add(this.nudID);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "FormGoto";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Go to row ID...";
		((System.ComponentModel.ISupportInitialize)this.nudID).EndInit();
		base.ResumeLayout(false);
	}
}
