namespace Taggle.Views.Pages;

public partial class MainPage : UserControl
{
	public MainPage()
	{
		InitializeComponent();
	}

	/// <summary>
	/// Invoked on selection change, navigates to the respective item.
	/// </summary>
	private void NavigationPanel_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (DataContext is not MainPageViewModel viewModel) return;

		switch (e.SelectedItem)
		{
			case NavigationItem item:
				viewModel.NavigateUsingItem(item);
				break;
		}
	}
}

