using System;

namespace MikelsToDoListWPF
{
	public class MainWindowViewModel : ViewModelBase
	{
		#region Fields

		// Property variables
		private DateTime p_DisplayDate;
		private string[] p_HighlightedDateText;

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
		public string[] HighlightedDateText
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
			// Initialize highlight text array
			p_HighlightedDateText = new String[31];

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

			// Set the highlighted date text
			for (var i = 0; i < 31; i++)
			{
				// First set this array element to null
				p_HighlightedDateText[i] = null;

				/* This demo simply highlights odd dates. So, if the array element represents 
				 * an even date, we leave the element at its null setting and skip to the next 
				 * increment of the loop. Note that the array is indexed from zero, while a 
				 * calendar is indexed from one. That means odd-numbered elements represent 
				 * even-numbered dates. So, if the index is odd, we skip.  */

				// If index is odd, skip to next
				if (i%2 == 1) continue;

				/* An element may be out of range for the current month. For example, element
				 * 30 would represent the 31st, which would be out of range for a month that 
				 * has only 30 days. If that's the case for the current element, we leave it
				 * set to null and skip to the next increment of the loop. */

				// If element is out of range, skip to next
				if (i >= lastDayOfMonth) continue;

				/* Since the array is indexed from zero, and a calendar is indexed from one, 
				 * we have to add one to the array index to get the calendar day to which it 
				 * corresponds. All we do in this demo is put the Long Date String is the
				 * HighlightedDateText array. */

				// Set highlight date text
				var targetDate = new DateTime(displayYear, displayMonth, i + 1);
				p_HighlightedDateText[i] = targetDate.ToLongDateString();
			}

			// Refresh the calendar
			this.RequestRefresh();
		}

		#endregion
	}
}