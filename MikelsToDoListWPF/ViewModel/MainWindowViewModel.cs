using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;
using System.Windows;



namespace MikelsToDoListWPF
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        // Property variables
        private DateTime p_DisplayDate;
        private List<string> p_HighlightedDateText;

        // Member variables
        private DateTime m_OldDisplayDate;

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            this.Initialize();
        }

        #endregion

        #region Events

        /// <summary>
        /// Notifies subscriubers that a UI refresh has been requested.
        /// </summary>
        /// <remarks>
        /// We use this event because this view model has no knowledge of the view that
        /// uses it. That keeps the view model from being dependent on the view. But it
        /// means that this view model can't directly invoke a refresh method on the view.
        /// So, it raises this event, and the view subscribes to it. The view invokes any
        /// refresh method that may be implemented there.
        /// </remarks>
        public event EventHandler RefreshRequested;

        #endregion


        #region Properties

        /// <summary>
        /// The DisplayDate in the calendar.
        /// </summary>
        public DateTime DisplayDate
        {
            get { return p_DisplayDate; }

            set
            {
                base.RaisePropertyChangingEvent("DisplayDate");
                p_DisplayDate = value;
                base.RaisePropertyChangedEvent("DisplayDate");
            }
        }

        /// <summary>
        /// The text to be shown in tool tips for highlighted dates.
        /// </summary>
        public List<string> HighlightedDateText
        {
            get { return p_HighlightedDateText; }

            set
            {
                base.RaisePropertyChangingEvent("HighlightedDateText");
                p_HighlightedDateText = value;
                base.RaisePropertyChangedEvent("HighlightedDateText");
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Updates the HighlightedDateText property when the calendar is changed to a different month.
        /// </summary>
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Ignore properties other than DisplayDate
            if (e.PropertyName != "DisplayDate") return;

            // Ignore change if date is DateTime.MinValue
            if (p_DisplayDate == DateTime.Today) return;

            // Ignore change if month is the same
            if (p_DisplayDate.IsSameMonthAs(m_OldDisplayDate)) return;

            // Populate month
            this.SetMonthHighlighting();

            // Update OldDisplayDate
            m_OldDisplayDate = p_DisplayDate;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        private void Initialize()
        {
            


            p_HighlightedDateText = new List<string>();

            // Set the display date to today
            p_DisplayDate = DateTime.Today;

            // Set month highlighting
            this.SetMonthHighlighting();

            // Subscribe to PropertyChanged event
            this.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Requests a Refresh from the view.
        /// </summary>
        private void RequestRefresh()
        {
            if (this.RefreshRequested != null)
            {
                this.RefreshRequested(this, new EventArgs());
            }
        }

        /// <summary>
        /// Sets highlighting for a month.
        /// </summary>
        private void SetMonthHighlighting()
        {
            var displayMonth = this.DisplayDate.Month;
            var displayYear = this.DisplayDate.Year;

            // Get the last day of the display month
            var month = this.DisplayDate.Month;
            var year = this.DisplayDate.Year;
            var lastDayOfMonth = DateTime.DaysInMonth(year, month);

            // Get xml document
            XmlDocument document = XmlDocument.;
            XmlElement dueDate = document.GetElementById("Due");

            foreach (XElement date in dueDate)
            {
                var due = date.ToString();
                DateTime dt = DateTime.Parse(due);
                string dueD = dt.ToLongDateString();
                p_HighlightedDateText.Add(dueD);
            }
            // Refresh the calendar
            this.RequestRefresh();
        }

        #endregion
    }
}
