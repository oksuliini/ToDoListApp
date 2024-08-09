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
        public Form1()
        {
            InitializeComponent();
            InitializeTaskListBox();
            reminderTimer.Start();
        }

        private void InitializeTaskListBox()
        {
            taskListBox.DrawMode = DrawMode.OwnerDrawFixed; // Asetetaan piirtotila omistajan piirtämäksi kiinteäksi
            taskListBox.ItemHeight = 20; // Asetetaan ListBox-kohteiden korkeus
            taskListBox.DrawItem += taskListBox_DrawItem; // Kytketään DrawItem-tapahtuma käsittelijään
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
                SetTaskColor(coloredItem, false); // Aseta väri vain lisättyyn tehtävään
                taskListBox.Items.Add(coloredItem);
                SortTasks();
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
                RemoveTaskFromFile(selectedItem.Text); // Poistetaan tehtävä myös tiedostosta
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
                SetTaskColor(selectedColoredItem, true); // Aseta väri merkitylle tehtävälle
                SortTasks();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("tasks.txt"))
            {
                foreach (ColoredListBoxItem item in taskListBox.Items)
                {
                    writer.WriteLine(item.Text);
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (File.Exists("tasks.txt"))
            {
                taskListBox.Items.Clear();
                using (StreamReader reader = new StreamReader("tasks.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var coloredItem = new ColoredListBoxItem
                        {
                            Text = line
                        };
                        SetTaskColor(coloredItem, false); // Aseta väri vain ladattuun tehtävään
                        taskListBox.Items.Add(coloredItem);
                    }
                }
                SortTasks();
            }
            else
            {
                MessageBox.Show("No saved tasks found.");
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

        private void SetTaskColor(ColoredListBoxItem item, bool isDone)
        {
            if (isDone)
            {
                item.Color = Color.Green; // Vihreä väri, kun tehtävä on tehty
            }
            else if (item.Text.Contains("High"))
            {
                item.Color = Color.Red; // Punainen väri, kun tehtävän kiireisyys on korkea
            }
            else if (item.Text.Contains("Medium"))
            {
                item.Color = Color.Orange; // Oranssi väri, kun tehtävän kiireisyys on keskitaso
            }
            else if (item.Text.Contains("Low"))
            {
                item.Color = Color.LightBlue; // Vaaleansininen väri, kun tehtävän kiireisyys on matala
            }
            else
            {
                item.Color = Color.Black; // Oletusväri muille tehtäville
            }
        }

        private void SortTasks()
        {
            var tasks = new List<ColoredListBoxItem>();
            var doneTasks = new List<ColoredListBoxItem>();

            foreach (ColoredListBoxItem item in taskListBox.Items)
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
                OpenEditTaskForm(selectedItem);
            }
            else
            {
                MessageBox.Show("Please select a task to edit.");
            }
        }
        private void OpenEditTaskForm(ColoredListBoxItem item)
        {
            // Oletetaan, että tekstin formaatti on "TaskName - Priority: [Priority] - Deadline: [Deadline]"
            string[] parts = item.Text.Split('-');
            string taskName = parts[0].Trim();
            string priorityPart = parts.FirstOrDefault(p => p.Contains("Priority:"))?.Replace("Priority:", "").Trim();
            string deadlinePart = parts.FirstOrDefault(p => p.Contains("Deadline:"))?.Replace("Deadline:", "").Trim();
            DateTime deadline = DateTime.Parse(deadlinePart);

            using (var editForm = new EditTaskForm(taskName, priorityPart, deadline))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // Päivitetään tehtävä uusilla arvoilla
                    item.Text = $"{editForm.TaskName} - Priority: {editForm.Priority} - Deadline: {editForm.Deadline.ToShortDateString()}";
                    SetTaskColor(item, false);
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
