using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIPermissions.Models;
using System.Collections.ObjectModel;

namespace MAUIPermissions.ViewModels
{
    /// <summary>
    /// ViewModel for the MainPage.
    /// </summary>
    public partial class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            Attachments = new ObservableCollection<FileItem>();
            AttachFromCameraCommand = new AsyncRelayCommand(AttachFromCamera);
            AttachFromGalleryCommand = new AsyncRelayCommand(AttachFromGallery);
            AttachFromFileCommand = new AsyncRelayCommand(AttachFromFile);
        }

        #region Properties

        /// <summary>
        /// Collection of attached files.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<FileItem> attachments;

        #endregion

        #region Commands

        /// <summary>
        /// Command to attach a file from the camera.
        /// </summary>
        public IAsyncRelayCommand AttachFromCameraCommand { get; }

        /// <summary>
        /// Command to attach a file from the gallery.
        /// </summary>
        public IAsyncRelayCommand AttachFromGalleryCommand { get; }

        /// <summary>
        /// Command to attach a file from the file system.
        /// </summary>
        public IAsyncRelayCommand AttachFromFileCommand { get; }

        /// <summary>
        /// Method to handle attaching a file from the camera.
        /// </summary>
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
                    // Save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);

                    Attachments.Add(new FileItem
                    {
                        Name = photo.FileName,
                        Path = localFilePath,
                        ImageSource = ImageSource.FromFile(localFilePath),
                        IsImage = true
                    });
                }
            }
        }

        /// <summary>
        /// Method to handle attaching a file from the gallery.
        /// </summary>
        private async Task AttachFromGallery()
        {
            // Check and request storage permission if not granted
            PermissionStatus storagePermissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (storagePermissionStatus != PermissionStatus.Granted)
            {
                storagePermissionStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (storagePermissionStatus != PermissionStatus.Granted)
            {
                await App.Current.MainPage.DisplayAlert("Storage Permission", "Storage permission denied", "OK");
                return;
            }

            // Pick a photo from the gallery
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                // Save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                Attachments.Add(new FileItem
                {
                    Name = photo.FileName,
                    Path = localFilePath,
                    ImageSource = ImageSource.FromFile(localFilePath),
                    IsImage = true
                });
            }
        }

        /// <summary>
        /// Method to handle attaching a file from the file system.
        /// </summary>
        private async Task AttachFromFile()
        {
            // Check and request storage permission if not granted
            PermissionStatus storagePermissionStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (storagePermissionStatus != PermissionStatus.Granted)
            {
                storagePermissionStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (storagePermissionStatus != PermissionStatus.Granted)
            {
                await App.Current.MainPage.DisplayAlert("Storage Permission", "Storage permission denied", "OK");
                return;
            }

            // Pick a file from the file system
            FileResult file = await FilePicker.Default.PickAsync();

            if (file != null)
            {
                // Save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, file.FileName);

                using Stream sourceStream = await file.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                Attachments.Add(new FileItem
                {
                    Name = file.FileName,
                    Path = localFilePath,
                    ImageSource = null,
                    IsImage = false
                });
            }
        }

        #endregion
    }
}
