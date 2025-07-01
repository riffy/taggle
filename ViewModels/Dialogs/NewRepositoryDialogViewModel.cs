namespace Taggle.ViewModels.Dialogs;

[RegisterTransient]
public sealed partial class NewRepositoryDialogViewModel(LogController logController, ContentDialog dialog) : ViewModelBase
{
	#region FOLDER PROMPT
	[ObservableProperty]
	private string _selectedFolderPath = string.Empty;

	/// <summary>
	/// Opens the folder selection picker.
	/// </summary>
	[RelayCommand]
	public async Task PromptFolderSelection()
	{
		try
		{
			ErrorMessage = string.Empty;
			var toplevel = TopLevel.GetTopLevel(dialog);
			if (toplevel is null)
				throw new NullReferenceException("Failed to retrieve top level");
			var result = await toplevel.StorageProvider.OpenFolderPickerAsync(new()
			{
				AllowMultiple = false,
				Title = "Repository Folder Selection"
			});
			var folder = result.Count > 0 ? result[0] : null;
			if (folder?.Path.AbsolutePath is null)
			{
				ErrorMessage = "No folder provided";
				return;
			}

			if (!Directory.Exists(folder.Path.AbsolutePath))
			{
				ErrorMessage = "Selected folder is invalid";
				return;
			}
			SelectedFolderPath = folder.Path.AbsolutePath;
		}
		catch (Exception ex)
		{
			logController.Exception(ex);
			ErrorMessage = "Critical Error while trying to open folder picker.";
		}
	}

	#endregion

	[ObservableProperty]
	private string _errorMessage = string.Empty;
}
