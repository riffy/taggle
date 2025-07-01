namespace Taggle.Views.Frames;

public partial class RepositoriesFrame : UserControl
{
	public RepositoriesFrame()
	{
		InitializeComponent();
		Loaded += RepositoryFrameLoaded;
	}

	private void RepositoryFrameLoaded(object? sender, RoutedEventArgs e)
	{
		if (DataContext is not RepositoriesFrameViewModel viewModel) return;
		viewModel.LoadRepositories();
	}
}

