using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace FotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void LoadImageDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.bmp, *.jpg)|*.bmp;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == false) return;
            
            Debug.WriteLine("////////////////////////////////////////////////////////////////////////");

            if (openFileDialog.FileName != "")
            {
                var stream = openFileDialog.OpenFile();
                var image = new BitmapImage();
                image.BeginInit();
                string a = System.IO.Path.GetFullPath(openFileDialog.FileName);
                //Debug.Write(a);
                image.UriSource = new Uri(a);
                image.EndInit();
                Debug.WriteLine(RemoveFileNameFromPath(a));
                LoadFileList(RemoveFileNameFromPath(a),a);
               ImageView.Source = image;
                
            }
            

        }

        string RemoveFileNameFromPath(string pathWithName)
        {
            string[] elements = pathWithName.Split('\\');
            string result = "";
            for (int i = 0; i < elements.Length - 1; i++)
            {
                result += elements[i] + '\\';
            }
            return result;
        }
        string[] listImageInDir = null;
        int idOpenedFile=-1;
        void LoadFileList(string path,string PathCurrentOpenFile)
        {
            if (!Directory.Exists(path)) throw new Exception("This is not path or in this path is file");
            listImageInDir = Directory.GetFiles(path,"*.jpg");
            //TODO(dominik) add more type file
            
            idOpenedFile = -1;
            for (int i=0;i< listImageInDir.Length;i++)
            {
                if (listImageInDir[i] == PathCurrentOpenFile)
                {
                    idOpenedFile = i;
                    break;
                }
            }
            if(idOpenedFile == -1)
            {
                throw new Exception("Not Find id file "+ PathCurrentOpenFile + " in {0}"+path);
            }


        }
        public void LoadImagefromPath(string path)
        {
            var image = new BitmapImage();
            string a = System.IO.Path.GetFullPath(path);
            image.BeginInit();
            image.UriSource = new Uri(a);
            image.EndInit();
            ImageView.Source = image;
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            LoadImageDialog();
        }
        private void NextImage(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("listImageInDir has " + (listImageInDir == null));
            if (listImageInDir == null) return;
            if (++idOpenedFile >= listImageInDir.Length) idOpenedFile = 0;
            Debug.WriteLine("idOpenetFile has "+idOpenedFile);
           LoadImagefromPath(listImageInDir[idOpenedFile]);
        }
        private void BackImage(object sender, RoutedEventArgs e)
        {
            if (listImageInDir == null) return;
            if (--idOpenedFile < 0) idOpenedFile = listImageInDir.Length-1;
            Debug.WriteLine("idOpenetFile has " + idOpenedFile);
            LoadImagefromPath(listImageInDir[idOpenedFile]);
        }
    }
}

