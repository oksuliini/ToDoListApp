﻿namespace TodoListApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.taskTextBox = new System.Windows.Forms.TextBox();
            this.addTaskButton = new System.Windows.Forms.Button();
            this.taskListBox = new System.Windows.Forms.ListBox();
            this.removeTaskButton = new System.Windows.Forms.Button();
            this.markAsDoneButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.priorityComboBox = new System.Windows.Forms.ComboBox();
            this.deadlinePicker = new System.Windows.Forms.DateTimePicker();
            this.reminderTimer = new System.Windows.Forms.Timer(this.components);
            this.editTaskButton = new System.Windows.Forms.Button();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // taskTextBox
            // 
            this.taskTextBox.Location = new System.Drawing.Point(40, 35);
            this.taskTextBox.Name = "taskTextBox";
            this.taskTextBox.Size = new System.Drawing.Size(200, 20);
            this.taskTextBox.TabIndex = 0;
            // 
            // addTaskButton
            // 
            this.addTaskButton.Location = new System.Drawing.Point(154, 114);
            this.addTaskButton.Name = "addTaskButton";
            this.addTaskButton.Size = new System.Drawing.Size(86, 23);
            this.addTaskButton.TabIndex = 1;
            this.addTaskButton.Text = "Add Task";
            this.addTaskButton.UseVisualStyleBackColor = true;
            this.addTaskButton.Click += new System.EventHandler(this.AddTaskButton_Click);
            // 
            // taskListBox
            // 
            this.taskListBox.FormattingEnabled = true;
            this.taskListBox.Location = new System.Drawing.Point(259, 35);
            this.taskListBox.Name = "taskListBox";
            this.taskListBox.Size = new System.Drawing.Size(413, 212);
            this.taskListBox.TabIndex = 2;
            // 
            // removeTaskButton
            // 
            this.removeTaskButton.Location = new System.Drawing.Point(154, 206);
            this.removeTaskButton.Name = "removeTaskButton";
            this.removeTaskButton.Size = new System.Drawing.Size(85, 23);
            this.removeTaskButton.TabIndex = 3;
            this.removeTaskButton.Text = "Remove Task";
            this.removeTaskButton.UseVisualStyleBackColor = true;
            this.removeTaskButton.Click += new System.EventHandler(this.RemoveTaskButton_Click);
            // 
            // markAsDoneButton
            // 
            this.markAsDoneButton.Location = new System.Drawing.Point(154, 249);
            this.markAsDoneButton.Name = "markAsDoneButton";
            this.markAsDoneButton.Size = new System.Drawing.Size(85, 23);
            this.markAsDoneButton.TabIndex = 4;
            this.markAsDoneButton.Text = "Mark as Done";
            this.markAsDoneButton.UseVisualStyleBackColor = true;
            this.markAsDoneButton.Click += new System.EventHandler(this.MarkAsDoneButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(259, 249);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(84, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // priorityComboBox
            // 
            this.priorityComboBox.FormattingEnabled = true;
            this.priorityComboBox.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
            this.priorityComboBox.Location = new System.Drawing.Point(40, 61);
            this.priorityComboBox.Name = "priorityComboBox";
            this.priorityComboBox.Size = new System.Drawing.Size(200, 21);
            this.priorityComboBox.TabIndex = 7;
            this.priorityComboBox.Text = "Priority:";
            // 
            // deadlinePicker
            // 
            this.deadlinePicker.Location = new System.Drawing.Point(40, 88);
            this.deadlinePicker.Name = "deadlinePicker";
            this.deadlinePicker.Size = new System.Drawing.Size(200, 20);
            this.deadlinePicker.TabIndex = 8;
            // 
            // reminderTimer
            // 
            this.reminderTimer.Interval = 86400000;
            // 
            // editTaskButton
            // 
            this.editTaskButton.Location = new System.Drawing.Point(154, 160);
            this.editTaskButton.Name = "editTaskButton";
            this.editTaskButton.Size = new System.Drawing.Size(86, 23);
            this.editTaskButton.TabIndex = 9;
            this.editTaskButton.Text = "Edit";
            this.editTaskButton.UseVisualStyleBackColor = true;
            this.editTaskButton.Click += new System.EventHandler(this.EditTaskButton_Click);
            // 
            // filterComboBox
            // 
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(49, 249);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(90, 21);
            this.filterComboBox.TabIndex = 10;
            this.filterComboBox.Text = "Filter Tasks";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 387);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.editTaskButton);
            this.Controls.Add(this.deadlinePicker);
            this.Controls.Add(this.priorityComboBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.markAsDoneButton);
            this.Controls.Add(this.removeTaskButton);
            this.Controls.Add(this.taskListBox);
            this.Controls.Add(this.addTaskButton);
            this.Controls.Add(this.taskTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox taskTextBox;
        private System.Windows.Forms.Button addTaskButton;
        private System.Windows.Forms.ListBox taskListBox;
        private System.Windows.Forms.Button removeTaskButton;
        private System.Windows.Forms.Button markAsDoneButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox priorityComboBox;
        private System.Windows.Forms.DateTimePicker deadlinePicker;
        private System.Windows.Forms.Timer reminderTimer;
        private System.Windows.Forms.Button editTaskButton;
        private System.Windows.Forms.ComboBox filterComboBox;
    }
}

