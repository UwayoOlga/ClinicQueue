using System;
using System.Linq;
using System.Windows.Forms;

namespace ClinicQueue
{
    public partial class Form1 : Form
    {
        private readonly ClinicManager _clinic = new();

        public Form1()
        {
            InitializeComponent();
            cmbType.Items.AddRange(new[] { "Adult", "Child", "Emergency" });
            cmbType.SelectedIndex = 0;
            RefreshAllDisplays();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var id = txtId.Text.Trim();
                var name = txtName.Text.Trim();
                Patient p = cmbType.SelectedItem?.ToString() switch
                {
                    "Adult" => new AdultPatient(id, name),
                    "Child" => new ChildPatient(id, name),
                    "Emergency" => new EmergencyPatient(id, name),
                    _ => throw new InvalidOperationException("Select a valid patient type"),
                };
                _clinic.RegisterPatient(p);
                MessageBox.Show($"Registered: {p}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllDisplays();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Register failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEnqueue_Click(object sender, EventArgs e)
        {
            try
            {
                var id = txtEnqueueId.Text.Trim();
                _clinic.Enqueue(id);
                MessageBox.Show($"Enqueued id: {id}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllDisplays();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Enqueue failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnServe_Click(object sender, EventArgs e)
        {
            try
            {
                var served = _clinic.ServeNext();
                MessageBox.Show($"Served: {served}", "Served", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAllDisplays();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Serve failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnShowQueue_Click(object sender, EventArgs e)
        {
            lstOutput.Items.Clear();
            lstOutput.Items.Add("Queue:");
            var q = _clinic.GetQueue();
            if (!q.Any()) lstOutput.Items.Add("(empty)");
            foreach (var item in q) lstOutput.Items.Add(item.ToString());
        }

        private void btnRecent_Click(object sender, EventArgs e)
        {
            lstOutput.Items.Clear();
            lstOutput.Items.Add("Recent visits:");
            var r = _clinic.GetRecentVisits();
            if (!r.Any()) lstOutput.Items.Add("(none)");
            foreach (var item in r) lstOutput.Items.Add(item.ToString());
        }

        private void btnListPatients_Click(object sender, EventArgs e)
        {
            lstOutput.Items.Clear();
            lstOutput.Items.Add("Registered patients:");
            var p = _clinic.ListPatients();
            if (!p.Any()) lstOutput.Items.Add("(none)");
            foreach (var item in p) lstOutput.Items.Add(item.ToString());
        }

        private void RefreshAllDisplays()
        {
            // Update small status labels or left lists if needed
            lblTotalPatients.Text = $"Patients: {_clinic.ListPatients().Count}";
            lblQueueCount.Text = $"Waiting: {_clinic.GetQueue().Count}";
        }
    }
}
