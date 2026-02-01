using System.Windows.Forms;
using System.Drawing;

namespace ClinicQueue
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblId;
        private TextBox txtId;
        private Label lblName;
        private TextBox txtName;
        private Label lblType;
        private ComboBox cmbType;
        private Button btnRegister;

        private Label lblEnqueue;
        private TextBox txtEnqueueId;
        private Button btnEnqueue;

        private Button btnServe;
        private Button btnShowQueue;
        private Button btnRecent;
        private Button btnListPatients;

        private ListBox lstOutput;
        private Label lblTotalPatients;
        private Label lblQueueCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.SuspendLayout();

            // Form
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(820, 480);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Clinic Queue";

            // Labels / TextBoxes for registration
            lblId = new Label() { Text = "Patient Id", Location = new Point(12, 14), AutoSize = true };
            txtId = new TextBox() { Location = new Point(12, 34), Size = new Size(220, 23), TabIndex = 0 };

            lblName = new Label() { Text = "Full Name", Location = new Point(12, 64), AutoSize = true };
            txtName = new TextBox() { Location = new Point(12, 84), Size = new Size(220, 23), TabIndex = 1 };

            lblType = new Label() { Text = "Type", Location = new Point(12, 114), AutoSize = true };
            cmbType = new ComboBox() { Location = new Point(12, 134), Size = new Size(220, 23), DropDownStyle = ComboBoxStyle.DropDownList, TabIndex = 2 };

            btnRegister = new Button() { Text = "Register Patient", Location = new Point(12, 170), Size = new Size(220, 30), TabIndex = 3 };
            btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // Enqueue controls
            lblEnqueue = new Label() { Text = "Enqueue by Id", Location = new Point(12, 220), AutoSize = true };
            txtEnqueueId = new TextBox() { Location = new Point(12, 240), Size = new Size(150, 23), TabIndex = 4 };
            btnEnqueue = new Button() { Text = "Enqueue", Location = new Point(170, 238), Size = new Size(62, 26), TabIndex = 5 };
            btnEnqueue.Click += new System.EventHandler(this.btnEnqueue_Click);

            // Serve / show buttons
            btnServe = new Button() { Text = "Serve Next", Location = new Point(12, 280), Size = new Size(220, 30), TabIndex = 6 };
            btnServe.Click += new System.EventHandler(this.btnServe_Click);

            btnShowQueue = new Button() { Text = "Show Queue", Location = new Point(12, 325), Size = new Size(220, 28), TabIndex = 7 };
            btnShowQueue.Click += new System.EventHandler(this.btnShowQueue_Click);

            btnRecent = new Button() { Text = "Show Recent Visits", Location = new Point(12, 360), Size = new Size(220, 28), TabIndex = 8 };
            btnRecent.Click += new System.EventHandler(this.btnRecent_Click);

            btnListPatients = new Button() { Text = "List Registered Patients", Location = new Point(12, 395), Size = new Size(220, 28), TabIndex = 9 };
            btnListPatients.Click += new System.EventHandler(this.btnListPatients_Click);

            // Output listbox and status labels
            lstOutput = new ListBox() { Location = new Point(250, 14), Size = new Size(550, 420), TabIndex = 10 };

            lblTotalPatients = new Label() { Text = "Patients: 0", Location = new Point(12, 430), AutoSize = true };
            lblQueueCount = new Label() { Text = "Waiting: 0", Location = new Point(120, 430), AutoSize = true };

            // Add controls to form
            this.Controls.AddRange(new Control[] {
                lblId, txtId, lblName, txtName, lblType, cmbType, btnRegister,
                lblEnqueue, txtEnqueueId, btnEnqueue,
                btnServe, btnShowQueue, btnRecent, btnListPatients,
                lstOutput, lblTotalPatients, lblQueueCount
            });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
