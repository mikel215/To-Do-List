using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace MikelsToDoListWPF
{



    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XmlDataProvider tasks = (XmlDataProvider)FindResource("tasks");
            tasks.Document.Save("Tasks.xml");
        }

        // Subscribes to the view model's RefreshRequested event.
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var mainWindowViewModel = (MainWindowViewModel)DataContext;
            mainWindowViewModel.RefreshRequested += OnRefreshRequested;
        }

        // Refreshes calendar
        private void OnRefreshRequested(object sender, EventArgs e)
        {
            this.TaskCalendar.Refresh();
        }

        private void TaskCalendar_OnIntialized(object sender, RoutedEventArgs e)
        {
            // Get the doc
            XDocument doc = XDocument.Load("Tasks.xml");

            // Create List
            List<string> list = (from lv1 in doc.Descendants("Task")
                                 select lv1.Element("Due").Value).ToList();

            // Highlight the dates
            foreach (string date in list)
            {
                DateTime DueDate = Convert.ToDateTime(date); 
                TaskCalendar.SelectedDates.Add(DueDate);
            }
            TaskCalendar.SelectedDates.Add(new DateTime (2017, 2, 2));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the doc
            XmlDocument document = ((XmlDataProvider)FindResource("tasks")).Document;

            // Create the Task element
            XmlElement task = document.CreateElement("Task");

            // Create the Name Element
            XmlElement name = document.CreateElement("Name");
            name.InnerText = TaskTitle.Text;
            task.AppendChild(name);

            // Create the Priority Element
            XmlElement priority = document.CreateElement("Priority");
            priority.InnerText = TaskPriority.Text;
            task.AppendChild(priority);

            // Create the Done Element
            XmlElement done = document.CreateElement("Done");
            done.InnerText = "No";
            task.AppendChild(done);

            // Create the Descripion Element
            XmlElement description = document.CreateElement("Description");
            description.InnerText = TaskDescription.Text;
            task.AppendChild(description);

            // Create the Due Date Element
            XmlElement duedate = document.CreateElement("Due");
            duedate.InnerText = TaskDueDate.SelectedDate.Value.ToString("dddd, MMMM d, yyyy");
            task.AppendChild(duedate);

            document.DocumentElement.AppendChild(task);

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if(TaskListBox.SelectedIndex != -1)
            {

                XmlElement task = (XmlElement)TaskListBox.SelectedItem;

                task.ParentNode.RemoveChild(task);
            }

        }
    }
}

