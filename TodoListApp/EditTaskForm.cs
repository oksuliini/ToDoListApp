using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoListApp
{
    public partial class EditTaskForm : Form
    {
        public string TaskName { get; private set; }
        public string Priority { get; private set; }
        public DateTime Deadline { get; private set; }

        public EditTaskForm(string currentTaskName, string currentPriority, DateTime currentDeadline)
        {
            InitializeComponent();

            taskNameTextBox.Text = currentTaskName;
            priorityComboBox.Items.AddRange(new string[] { "High", "Medium", "Low" });
            priorityComboBox.SelectedItem = currentPriority;
            deadlinePicker.Value = currentDeadline;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            TaskName = taskNameTextBox.Text.Trim();
            Priority = priorityComboBox.SelectedItem?.ToString();
            Deadline = deadlinePicker.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void EditTaskForm_Load(object sender, EventArgs e)
        {

        }
    }

}
