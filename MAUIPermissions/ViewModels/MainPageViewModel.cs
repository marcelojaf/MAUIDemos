using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIPermissions.Models;
using System.Collections.ObjectModel;

namespace MAUIPermissions.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            AttachFromCameraCommand = new AsyncRelayCommand(AttachFromCamera);
        }

        #region Properties

        [ObservableProperty]
        private ObservableCollection<FileItem> attachments;

        #endregion

        #region Commands
        public IAsyncRelayCommand AttachFromCameraCommand { get; }

        private async Task AttachFromCamera()
        {
            // Get the current status of the Camera permission
            PermissionStatus cameraPermissionStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

            // If the Camera permission has not been granted, request it
            if (cameraPermissionStatus != PermissionStatus.Granted)
            {
                cameraPermissionStatus = await Permissions.RequestAsync<Permissions.Camera>();
            }

            // If the Camera permission is still denied, display an alert and do nothing
            if (cameraPermissionStatus != PermissionStatus.Granted)
            {
                await App.Current.MainPage.DisplayAlert("Camera Permission", "Camera permission denied", "OK");
                return;
            }

            // If the Camera permission has been granted, take a photo
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
        }
        #endregion
    }
}
