using DemoLibrary.Helpers;
using DemoLibrary.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WPFAppConsumeAPIDataAsync.Commands;

namespace WPFAppConsumeAPIDataAsync.ViewModels
{
    public class MainViewModel :ViewModelBase
    {
        private int maxNumber, curentNumber = 0;

        private bool _isNextImageCommandEnable;

        public bool IsNextImageCommandEnable
        {
            get { return _isNextImageCommandEnable; }
            set {
                _isNextImageCommandEnable = value;
                NotifyPropertyChanged(nameof(IsNextImageCommandEnable));
            }
        }

        private bool _isPreviousImageCommandEnable;

        public bool IsPreviousImageCommandEnable
        {
            get { return _isPreviousImageCommandEnable; }
            set {
                _isPreviousImageCommandEnable = value;
                NotifyPropertyChanged(nameof(IsPreviousImageCommandEnable));
            }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get { return _imageSource; }
            set {
                _imageSource = value;
                NotifyPropertyChanged(nameof(ImageSource));
            }
        }



        public DelegateCommand ShowNextImageCommand { get; set; }
        public DelegateCommand ShowInformationCommand { get; set; }
        public DelegateCommand ShowPreviousImageCommand { get; set; }

        public MainViewModel()
        {
            ApiHelper.InitializeClient();

            LoadImage();
            
            IsNextImageCommandEnable = false;

            ShowInformationCommand = new DelegateCommand(ShowInformation, CanShowInformation);
            ShowNextImageCommand = new DelegateCommand(ShowNextImage, CanShowPreviousImage);
            ShowPreviousImageCommand = new DelegateCommand(ShowPreviousImage, null);
        }

        private bool CanShowPreviousImage(object obj)
        {
            return true;
        }
        private bool CanShowNextImage(object obj)
        {
            return true;
        }
        private bool CanShowInformation(object obj)
        {
            return true;
        }

        private async void ShowPreviousImage(object obj)
        {
            if (curentNumber > 1)
            {
                curentNumber -= 1;
                IsNextImageCommandEnable = true;
                await LoadImage(curentNumber);

                if (curentNumber == 1)
                {
                    IsPreviousImageCommandEnable = false;
                }
            }
        }

        private async void ShowNextImage(object obj)
        {
            if (curentNumber < maxNumber)
            {
                curentNumber += 1;
                IsPreviousImageCommandEnable = true;
                await LoadImage(curentNumber);

                if (curentNumber == maxNumber)
                {
                    IsNextImageCommandEnable = false;
                }
            }
        }

        private void ShowInformation(object obj)
        {
           
        }

        private async Task LoadImage(int imageNumber = 0)
        {
            var comic = await ComicProcessor.LoadComic(imageNumber);

            if (imageNumber == 0)
            {
                maxNumber = comic.Num;
            }

            curentNumber = comic.Num;

            var uriSource = new Uri(comic.Img, UriKind.Absolute);
            //comicImage.Source = new BitmapImage(uriSource);
            ImageSource = new BitmapImage(uriSource);
        }
    }
}
