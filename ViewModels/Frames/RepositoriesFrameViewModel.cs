namespace Taggle.ViewModels.Frames;

[RegisterSingleton]
public sealed partial class RepositoriesFrameViewModel(
	IServiceProvider serviceProvider,
	RepositoryController repositoryController,
	LogController logController) : ViewModelBase
{

	#region TABLE
	[ObservableProperty]
	private ObservableCollection<Repository> _repositories = [];

	/// <summary>
	/// Loads the repositories and fills the collection
	/// </summary>
	public async Task LoadRepositories()
	{
		Repositories.Clear();
		var repos = await repositoryController.GetRepositoriesAsync();
		foreach (var repo in repos)
			Repositories.Add(repo);
	}
	#endregion

	#region NEW REPO DIALOG
	/// <summary>
	/// Opens the dialog to add a new repository
	/// </summary>
	[RelayCommand]
	public async Task AddNewRepository()
	{
		try
		{
			ContentDialog dialog = new()
			{
				Title = "New Repository",
				PrimaryButtonText = "Add",
				CloseButtonText = "Cancel"
			};
			var vm = ActivatorUtilities.CreateInstance<NewRepositoryDialogViewModel>(serviceProvider, dialog);
			dialog.Content = new NewRepositoryDialog { DataContext = vm };
			var result = await dialog.ShowAsync();
			if (result != ContentDialogResult.Primary)
				return;
			if (string.IsNullOrEmpty(vm.SelectedFolderPath) || !Directory.Exists(vm.SelectedFolderPath))
			{
				logController.Error($"Selected repository folder does not exist: {vm.SelectedFolderPath}");
				return;
			}
			logController.Info($"Adding \"{vm.SelectedFolderPath}\" as new repository");
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
		}
	}
	#endregion
}
