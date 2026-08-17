using UserInformation.Services;

namespace MockCanvasMauiApp1;

public partial class SemesterSettingsPage : ContentPage
{
	public SemesterSettingsPage()
	{
		InitializeComponent();

        StartDatePicker.Date =
            SemesterService.Current.CurrentSemester.StartDate;

        EndDatePicker.Date =
            SemesterService.Current.CurrentSemester.EndDate;
    }

    private async void OnSaveSemesterClicked(object sender, EventArgs e)
    {
        if (EndDatePicker.Date < StartDatePicker.Date)
        {
            await DisplayAlert(
                "Error",
                "The end date cannot be before the start date.",
                "OK");

            return;
        }

        SemesterService.Current.CurrentSemester.StartDate =
            StartDatePicker.Date.Value;

        SemesterService.Current.CurrentSemester.EndDate =
            EndDatePicker.Date.Value;

        await DisplayAlert(
            "Saved",
            "Semester dates have been saved.",
            "OK");
    }
}