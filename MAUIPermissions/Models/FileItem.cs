using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUIPermissions.Models
{
    /// <summary>
    /// Represents an attached file item.
    /// </summary>
    public class FileItem : ObservableObject
    {
        private string _name;
        /// <summary>
        /// Name of the file.
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private ImageSource _imageSource;
        /// <summary>
        /// Source of the image if the file is an image.
        /// </summary>
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private string _fileIcon;
        /// <summary>
        /// Icon representing the file type.
        /// </summary>
        public string FileIcon
        {
            get => _fileIcon;
            set => SetProperty(ref _fileIcon, value);
        }

        private string _contentType;
        /// <summary>
        /// Content type of the file.
        /// </summary>
        public string ContentType
        {
            get => _contentType;
            set => SetProperty(ref _contentType, value);
        }

        private Stream _content;
        /// <summary>
        /// Content of the file as a stream.
        /// </summary>
        public Stream Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private string _path;
        /// <summary>
        /// Path to the file.
        /// </summary>
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        private bool _isImage;
        /// <summary>
        /// Indicates whether the file is an image.
        /// </summary>
        public bool IsImage
        {
            get => _isImage;
            set => SetProperty(ref _isImage, value);
        }
    }
}
