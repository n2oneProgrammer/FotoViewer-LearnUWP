using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FotoViewer_Learn_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
           // OpenImage();
           
        }
        public async void LoadImageAsync()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();
            Debug.WriteLine("////////////////////////////////////////////////////////////////////////");

            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var image = new BitmapImage();
                image.SetSource(stream);

                Debug.WriteLine(RemoveFileNameFromPath(file.Path));
                LoadFileList(RemoveFileNameFromPath(file.Path));
               ImageView.Source = image;
                return;
            }
            else
            {
                return;
            }
            
        }

        string RemoveFileNameFromPath(string pathWithName)
        {
            string[] elements = pathWithName.Split('\\');
            string result="";
            for(int i = 0;i < elements.Length-1;i++)
            {
                result += elements[i] + '\\';
            }
            return result;
        }
        void LoadFileList(string path)
        {
            //if (!Directory.Exists(path)) throw new Exception("This is not path or in this path is file");
            string[] list = Directory.GetFiles(@"C:\Users\dominik\Desktop\Tajny folder ze zdjeciami");
            Debug.WriteLine("Files in " + path);
            foreach (string item in list)
            {
                Debug.WriteLine(item);
            }
                
            
        }
        internal void Close()
        {
            Application.Current.Exit();       
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            LoadImageAsync();
        }
      
    }
}
