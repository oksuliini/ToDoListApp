using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace TodoListApp
{
    public partial class Form1 : Form
    {
        private List<ColoredListBoxItem> originalTaskList = new List<ColoredListBoxItem>();

        public Form1()
        {
            InitializeComponent();
            InitializeTaskListBox();
            InitializeFilterComboBox();
            reminderTimer.Start();
            LoadTasks(); // Lataa tehtävät sovelluksen käynnistyessä
        }

        private void InitializeTaskListBox()
        {
            taskListBox.DrawMode = DrawMode.OwnerDrawFixed;
            taskListBox.ItemHeight = 20;
            taskListBox.DrawItem += taskListBox_DrawItem;
        }

        private void InitializeFilterComboBox()
        {
            filterComboBox.Items.AddRange(new string[] { "All", "High", "Medium", "Low", "Completed" });
            filterComboBox.SelectedIndex = 0;
            filterComboBox.SelectedIndexChanged += filterComboBox_SelectedIndexChanged;
        }

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            string task = taskTextBox.Text.Trim();
            string priority = priorityComboBox.SelectedItem?.ToString() ?? "Low";
            string deadline = deadlinePicker.Value.ToShortDateString();
            if (!string.IsNullOrEmpty(task))
            {
                var coloredItem = new ColoredListBoxItem
                {
                    Text = $"{task} - Priority: {priority} - Deadline: {deadline}"
                };
                SetTaskColor(coloredItem);
                originalTaskList.Add(coloredItem);
                SortTasksByPriority();
                ApplyFilter(filterComboBox.SelectedItem.ToString());
                taskTextBox.Clear();
                priorityComboBox.SelectedIndex = -1;
                deadlinePicker.Value = DateTime.Now;
            }
        }

        private void removeTaskButton_Click(object sender, EventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                var selectedItem = (ColoredListBoxItem)taskListBox.SelectedItem;
                taskListBox.Items.Remove(selectedItem);
                originalTaskList.Remove(selectedItem);
                RemoveTaskFromFile(selectedItem.Text);
            }
        }

        private void RemoveTaskFromFile(string taskToRemove)
        {
            string filePath = "tasks.txt";
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath).ToList();
                lines.RemoveAll(line => line.Trim() == taskToRemove.Trim());
                File.WriteAllLines(filePath, lines);
            }
        }

        private void markAsDoneButton_Click(object sender, EventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                var selectedColoredItem = (ColoredListBoxItem)taskListBox.SelectedItem;
                var originalTaskText = selectedColoredItem.Text.Split('-')[0].Trim();
                selectedColoredItem.Text = $"{originalTaskText} - Completed on: {DateTime.Now.ToShortDateString()}";
                SetTaskColor(selectedColoredItem);
                SortTasks();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("tasks.txt"))
            {
                foreach (ColoredListBoxItem item in originalTaskList)
                {
                    writer.WriteLine(item.Text);
                }
            }
        }

        private void LoadTasks()
        {
            if (File.Exists("tasks.txt"))
            {
                taskListBox.Items.Clear();
                originalTaskList.Clear();
                using (StreamReader reader = new StreamReader("tasks.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var coloredItem = new ColoredListBoxItem
                        {
                            Text = line
                        };
                        SetTaskColor(coloredItem);
                        originalTaskList.Add(coloredItem);
                    }
                }
                SortTasksByPriority();
                ApplyFilter(filterComboBox.SelectedItem.ToString());
            }
        }

        private void reminderTimer_Tick(object sender, EventArgs e)
        {
            foreach (ColoredListBoxItem item in taskListBox.Items)
            {
                string task = item.Text;
                if (task.Contains("Deadline:"))
                {
                    string[] parts = task.Split('-');
                    string deadlinePart = parts.FirstOrDefault(p => p.Contains("Deadline:"))?.Trim();
                    if (deadlinePart != null)
                    {
                        DateTime deadline = DateTime.Parse(deadlinePart.Replace("Deadline: ", ""));
                        if (deadline.Date == DateTime.Now.Date)
                        {
                            MessageBox.Show($"Reminder: '{parts[0].Trim()}' is due today!", "Reminder");
                        }
                    }
                }
            }
        }

        private void SetTaskColor(ColoredListBoxItem item)
        {
            if (item.Text.Contains("Completed on:"))
            {
                item.Color = Color.Green; // Completed tehtävät
            }
            else if (item.Text.Contains("High"))
            {
                item.Color = Color.Red;
            }
            else if (item.Text.Contains("Medium"))
            {
                item.Color = Color.Orange;
            }
            else if (item.Text.Contains("Low"))
            {
                item.Color = Color.LightBlue;
            }
            else
            {
                item.Color = Color.Black;
            }
        }

        private void SortTasks()
        {
            var tasks = new List<ColoredListBoxItem>();
            var doneTasks = new List<ColoredListBoxItem>();

            foreach (ColoredListBoxItem item in originalTaskList)
            {
                if (item.Text.Contains("Completed on:"))
                {
                    doneTasks.Add(item);
                }
                else
                {
                    tasks.Add(item);
                }
            }

            taskListBox.Items.Clear();

            foreach (var task in tasks)
            {
                taskListBox.Items.Add(task);
            }

            foreach (var doneTask in doneTasks)
            {
                taskListBox.Items.Add(doneTask);
            }
        }

        private void SortTasksByPriority()
        {
            originalTaskList = originalTaskList
                .OrderByDescending(item => GetPriorityValue(item.Text))
                .ThenBy(item => GetDeadline(item.Text))
                .ToList();
        }

        private int GetPriorityValue(string taskText)
        {
            if (taskText.Contains("High")) return 3;
            if (taskText.Contains("Medium")) return 2;
            if (taskText.Contains("Low")) return 1;
            return 0;
        }

        private DateTime GetDeadline(string taskText)
        {
            var parts = taskText.Split('-');
            var deadlinePart = parts.FirstOrDefault(p => p.Contains("Deadline:"))?.Replace("Deadline:", "").Trim();
            if (DateTime.TryParse(deadlinePart, out DateTime deadline))
            {
                return deadline;
            }
            return DateTime.MaxValue;
        }

        private void ApplyFilter(string priority)
        {
            taskListBox.Items.Clear();

            foreach (var item in originalTaskList)
            {
                if (priority == "All" || item.Text.Contains($"Priority: {priority}"))
                {
                    taskListBox.Items.Add(item);
                }
                else if (priority == "Completed" && item.Text.Contains("Completed on:"))
                {
                    taskListBox.Items.Add(item);
                }
            }
        }

        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter(filterComboBox.SelectedItem?.ToString());
        }

        private void taskListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            var listBox = sender as ListBox;
            if (listBox != null && e.Index >= 0 && e.Index < listBox.Items.Count)
            {
                var item = listBox.Items[e.Index] as ColoredListBoxItem;
                if (item != null)
                {
                    e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(item.Color), e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
                }
            }
            e.DrawFocusRectangle();
        }

        private void editTaskButton_Click(object sender, EventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                var selectedItem = (ColoredListBoxItem)taskListBox.SelectedItem;
                if (selectedItem.Text.Contains("Completed on:"))
                {
                    MessageBox.Show("Completed tasks cannot be edited.", "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    OpenEditTaskForm(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select a task to edit.");
            }
        }

        private void OpenEditTaskForm(ColoredListBoxItem item)
        {
            string[] parts = item.Text.Split('-');
            string taskName = parts[0].Trim();
            string priorityPart = parts.FirstOrDefault(p => p.Contains("Priority:"))?.Replace("Priority:", "").Trim();
            string deadlinePart = parts.FirstOrDefault(p => p.Contains("Deadline:"))?.Replace("Deadline:", "").Trim();
            DateTime deadline = DateTime.Parse(deadlinePart);

            using (var editForm = new EditTaskForm(taskName, priorityPart, deadline))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    item.Text = $"{editForm.TaskName} - Priority: {editForm.Priority} - Deadline: {editForm.Deadline.ToShortDateString()}";
                    SetTaskColor(item);

                    // Poista muokattu tehtävä listalta ja lisää se takaisin alkuperäiseen listaan
                    originalTaskList.Remove(item);
                    originalTaskList.Add(item);

                    // Järjestä tehtävät uudelleen
                    SortTasksByPriority();
                    ApplyFilter(filterComboBox.SelectedItem.ToString());

                    taskListBox.Refresh(); // Päivitetään ListBox näkymä
                }
            }
        }
    }

    public class ColoredListBoxItem
    {
        public string Text { get; set; }
        public Color Color { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
