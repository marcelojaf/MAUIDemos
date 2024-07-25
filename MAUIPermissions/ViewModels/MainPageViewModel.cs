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
            PermissionStatus cameraPermissionStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (cameraPermissionStatus != PermissionStatus.Granted)
            {
                cameraPermissionStatus = await Permissions.RequestAsync<Permissions.Camera>();
            }

            if (cameraPermissionStatus != PermissionStatus.Granted)
            {
                await App.Current.MainPage.DisplayAlert("Camera Permission", "Camera permission denied", "OK");
                return;
            }

            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
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
            if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
            {
                PermissionStatus photoPermissionStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();

                if (photoPermissionStatus != PermissionStatus.Granted)
                {
                    photoPermissionStatus = await Permissions.RequestAsync<Permissions.Photos>();
                }

                if (photoPermissionStatus != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert("Photo Permission", "Photo library permission denied", "OK");
                    return;
                }
            }
            else
            {
                PermissionStatus storagePermissionStatus = await CheckAndRequestStoragePermission();

                if (storagePermissionStatus != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert("Storage Permission", "Storage permission denied", "OK");
                    return;
                }
            }

            FileResult photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
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
            if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
            {
                PermissionStatus filePermissionStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();

                if (filePermissionStatus != PermissionStatus.Granted)
                {
                    filePermissionStatus = await Permissions.RequestAsync<Permissions.Photos>();
                }

                if (filePermissionStatus != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert("Photo Permission", "Photo library permission denied", "OK");
                    return;
                }
            }
            else
            {
                PermissionStatus storagePermissionStatus = await CheckAndRequestStoragePermission();

                if (storagePermissionStatus != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert("Storage Permission", "Storage permission denied", "OK");
                    return;
                }
            }

            FileResult file = await FilePicker.Default.PickAsync();

            if (file != null)
            {
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

        /// <summary>
        /// Helper method to check and request storage permission.
        /// </summary>
        private async Task<PermissionStatus> CheckAndRequestStoragePermission()
        {
            PermissionStatus status;

            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major >= 11)
            {
                status = await Permissions.CheckStatusAsync<Permissions.Media>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Media>();
                }
            }
            else
            {
                status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }
            }

            return status;
        }

        #endregion
    }
}
