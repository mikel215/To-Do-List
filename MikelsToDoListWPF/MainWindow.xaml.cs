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

namespace MikelsToDoListWPF
{



    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
        }
    }
}

