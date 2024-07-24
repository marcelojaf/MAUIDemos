using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUIPermissions.Models
{
    public class FileItem : ObservableObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private string _fileIcon;
        public string FileIcon
        {
            get => _fileIcon;
            set => SetProperty(ref _fileIcon, value);
        }

        private string _contentType;
        public string ContentType
        {
            get => _contentType;
            set => SetProperty(ref _contentType, value);
        }

        private Stream _content;
        public Stream Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private string _path;
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        private bool _isImage;
        public bool IsImage
        {
            get => _isImage;
            set => SetProperty(ref _isImage, value);
        }
    }
}
