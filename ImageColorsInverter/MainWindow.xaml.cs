using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Drawing;
using System.IO;

namespace ImageColorsInverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var test = GetFileName(@"0.dll");
            InitializeComponent();
        }

        private void Button_Choose_Image_Click(object sender, RoutedEventArgs e)
        {
            var file_opening_dialog = new Microsoft.Win32.OpenFileDialog
            {
                //DefaultExt = ".txt",
                Filter = "Image Files (*.*)|*.*",
                CheckFileExists = true,
                //Multiselect = true
            };

            if (file_opening_dialog.ShowDialog(this) == true)
            {
                var sourceFullFilename = file_opening_dialog.FileName;
                var sourceFileShortName = file_opening_dialog.SafeFileName;
                Bitmap img;

                try
                {
                    img = new Bitmap(sourceFullFilename);
                }
                catch
                {
                    MessageBox.Show("Unsupported file format!");
                    return;
                }
                

                for (var y = 0; y < img.Height; y++)
                {
                    for (var x = 0; x < img.Width; x++)
                    {
                        var oldColor = img.GetPixel(x, y);

                        var newColor = Color.FromArgb(255 - oldColor.R, 255 - oldColor.G, 255 - oldColor.B);

                        img.SetPixel(x, y, newColor);
                    }
                }

                var extension = GetFileExtension(sourceFullFilename);
                var filename = GetFileName(sourceFullFilename);

                if (extension.ToLower() == "gif")
                    img.Save(String.Format("{0}_inverted.{1}", filename, extension), System.Drawing.Imaging.ImageFormat.Gif);
                else if (extension.ToLower() == "jpeg" || extension.ToLower() == "jpg")
                    img.Save(String.Format("{0}_inverted.{1}", filename, extension), System.Drawing.Imaging.ImageFormat.Jpeg);
                else if (extension.ToLower() == "png")
                    img.Save(String.Format("{0}_inverted.{1}", filename, extension), System.Drawing.Imaging.ImageFormat.Png);
                else
                    MessageBox.Show("Unsupported file format!");

                MessageBox.Show(String.Format("Saved to {0}_inverted.{1}", filename, extension));
            }
        }


        static string GetFileExtension(string filename)
        {
            for(int index = filename.Length - 1; index != 0; index--)
            {
                if (filename[index] == '.')
                    return filename.Substring(index + 1, filename.Length - index - 1);
            }

            return filename;
        }


        static string GetFileName(string filename)
        {
            for (int index = filename.Length - 1; index != 0; index--)
            {
                if (filename[index] == '.')
                    return filename.Substring(0, index);
            }

            return filename;
        }



    }
}
