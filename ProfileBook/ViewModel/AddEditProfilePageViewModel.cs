using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Service;
using ProfileBook.Service.CameraAndImage;
using ProfileBook.Service.DatabaseСonverter;
using ProfileBook.Services.Interface;
using ProfileBook.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class AddEditProfilePageViewModel: BindableBase, INavigatedAware
    {
        private string _titlePage;
        private string _nickName;
        private string _name;
        private string _description;
        private bool _isEnabled;
        private int _id;
        private string pathPicture;
        private Image _picture { get; set; }
        private ImageSource _pictureSource;
        private ProfileModel profileUser;
        public bool _Enabled { get; set; }
        public Action OpenGallery { get; set; }
        public Action TakePhoto { get; set; }

        private ICommand Command { get; set; }

        private ICommand tapCommand;

        private readonly INavigationService navigationService;
        private readonly ICameraAndImage cameraAndImage;
        private readonly IRepository repository;
        public ICommand NavigationToMainList { get; set; }
        private bool _isEnable;
        public AddEditProfilePageViewModel(INavigationService navigationService, ICameraAndImage cameraAndImage, IRepository repository)
        {
            TitlePage = ($"{ nameof(AddEditProfilePage)}");
            this.navigationService = navigationService;
            Name = string.Empty;
            NickName = string.Empty;
            NavigationToMainList = new DelegateCommand(Execute, CanExecute);
            tapCommand = new Command(OnTapped);
            this.cameraAndImage = cameraAndImage;
            IsEnable = false;
            OpenGallery = SelectImage;
            TakePhoto = TakingPictures;
            this.repository = repository;
        }
        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }
        public ICommand TapCommand
        {
            get => tapCommand;
            set => SetProperty(ref tapCommand, value);
        }
        
        public ProfileModel ProfileUser
        {
            get => profileUser;
            set => SetProperty(ref profileUser, value);
        }
        public ImageSource PictureSource
        {
            get => _pictureSource;
            set => SetProperty(ref _pictureSource, value);
        }
        public string TitlePage
        {
            get => _titlePage;
            set => SetProperty(ref _titlePage, value);
        }
        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public string PathPicture
        {
            get => pathPicture;
            set => SetProperty(ref pathPicture, value);
        }
        public async void ExecuteNavigateToNavigationToMainList()
        {
            var parametr = new NavigationParameters();
            parametr.Add("ProfileUser", profileUser);
            //To description of logic add information
            await navigationService.NavigateAsync(($"/{ nameof(NavigationPage)}/{ nameof(MainList)}"));

        }
       public void OnTapped()
        {
            IUserDialogs d = UserDialogs.Instance;
            ActionSheetConfig config = new ActionSheetConfig();
            List<ActionSheetOption> Options = new List<ActionSheetOption>();
            Options.Add(new ActionSheetOption("Pick at Gallery", OpenGallery, "folder_image.png"));
            Options.Add(new ActionSheetOption("Take photo with camera", TakePhoto, "camera.png"));
            ActionSheetOption cancel = new ActionSheetOption("Cancel", null, null);
            config.Options = Options;
            config.Cancel = cancel;
            var select = d.ActionSheet(config);
            //var a= cameraAndImage.CallImageAndCamera();
        }
        public bool CanExecute()
        {
            return true;
        }
        public void Execute()
        {
            if (Validation.IsInformationInNameAndNickName(Name, NickName))
            {
                //Method for add to database
                AddProfileModel();
                
                ExecuteNavigateToNavigationToMainList();
            }  
        }
        public void AddProfileModel()
        {
            ProfileModel profileModel = new ProfileModel()
            {
                UserId = _id,
                Description = Description,
                ImageSource = PathPicture,
                MomentOfRegistration = DateTime.Now,
                Name = Name,
                NickName = NickName
            };
            repository.InsertAsync<ProfileModel>(profileModel);
        }
        //Method for selecting photo
        async public void SelectImage()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });
            var stream = await result.OpenReadAsync();
            PictureSource = ImageSource.FromStream(() => stream);
            //Получаем путь к картинке
            PathPicture = result.FullPath;
        }

        //Method for taking pictures
        async public void TakingPictures()
        {
            var result = await MediaPicker.CapturePhotoAsync();
            var stream = await result.OpenReadAsync();
            var newFile = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
            var newStream = File.OpenWrite(newFile);
            await stream.CopyToAsync(newStream);
            //Получаем путь к картинке
            PathPicture = result.FullPath;
            PictureSource = ImageSource.FromFile(result.FullPath);
            //PictureSource = ImageSource.FromStream(() => stream);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _id = parameters.GetValue<int>("CurrentId");
        }
    }
}
