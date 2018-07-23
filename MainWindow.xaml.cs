using HelixToolkit.Wpf;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Microsoft.Win32;

namespace Display3DModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Path to the model file
        //private const string MODEL_PATH = "D:/Display 3D in WPF C#/soccer+ball/soccer ball.obj";
        //private const string MODEL_PATH = "D:/Display 3D in WPF C#/bcbikjiauz-UH60Blackhawk/UH-60 Blackhawk/uh60.obj";
        //private const string MODEL_PATH = "D:/Display 3D in WPF C#/MQ-9 Predator/MQ-9.obj";
        private string MODEL_PATH = @"D:\Display 3D in WPF C#\UH60Blackhawk\uh60.obj";
        private ModelVisual3D device3D = null;
        public MainWindow()
        {
            InitializeComponent();

            load_new_file(MODEL_PATH);
        }

        /// <summary>
        /// Display 3D Model
        /// </summary>
        /// <param name="model">Path to the Model file</param>
        /// <returns>3D Model Content</returns>
        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {
                //Adding a gesture here
                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                //Import 3D model file
                ModelImporter import = new ModelImporter();

                //Load the 3D model file
                device = import.Load(model);

                //Hide SubTitle
                viewPort3d.SubTitle = "";
            }
            catch (Exception e)
            {
                //Show SubTitle
                viewPort3d.SubTitle = "File is not supported.";

                // Handle exception in case can not file 3D model
                MessageBox.Show("Exception Error : " + e.StackTrace, "Not supported");
            }
            return device;
        }

        private void load_new_file(string path)
        {
            MODEL_PATH = path;
            bool resetcamera = device3D == null ? false : true;

            //Remove previous 3D Object
            viewPort3d.Children.Remove(device3D);

            //Loading 3D file
            device3D = new ModelVisual3D();
            device3D.Content = Display3d(MODEL_PATH);

            // Add to view port
            viewPort3d.Children.Add(device3D);

            //Reset Camera Viewer
            if (resetcamera)
            {
                viewPort3d.Camera.Reset();
                viewPort3d.Camera.ZoomExtents(viewPort3d.Viewport, 1000);
            }


            //Set Subtitle File Name For User
            this.Title = PathGetFileName() + " - Display 3D Object File ";

        }

        private string PathGetFileName()
        {
            int index = MODEL_PATH.LastIndexOf("\\");
            string str = MODEL_PATH.Substring(index + 1);
            return str;
        }

        private void MenuItem_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("About Display 3D File \n\nThe program is written in XAML (C#) language. Which is used plugin Helix Toolkit, a collection of 3D components for .NET.\n\nDisplay 3D program is protected by MIT License (MIT).\n\nAuthor: Vu Van Duc (ducduc08@gmail.com).\nCopyright: June 2018.", 
                "About Display 3D File", 
                MessageBoxButton.OK, 
                MessageBoxImage.Information);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult id = MessageBox.Show("Current 3D object file will be unload from program. Do you want to continue?", "Display 3D File", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.No);
            if(id == MessageBoxResult.OK)
            {
                Environment.Exit(1);
            }
        }

        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.AddExtension = true;
            openFileDialog.Filter = "All Supported Files (*.obj, *.objz, *.stl, *.3ds, *.lwo, *.off) |*.obj;*.objz;*.stl;*.3ds;*.lwo;*.off|" +
                                    "All Files (*.*) |*.*|" +
                                    "3D Object File (*.obj, *.objz) |*.obj;*.objz|" +
                                    "STL File (*.stl) |*.stl|" +
                                    "LWO File (*.lwo)|*.lwo|" +
                                    "OFF file (*.off)|*.off";
            openFileDialog.DefaultExt = "";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Title = "Open 3D file...";
            

            if (openFileDialog.ShowDialog() == true) {
                load_new_file(openFileDialog.FileName);
            }
        }

        private void MenuItem_Location(object sender, RoutedEventArgs e)
        {
            string strCmdText = MODEL_PATH.Replace("/", "\\");
            System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + strCmdText + "\"");
        }

        private void MenuItem_DefPosition(object sender, RoutedEventArgs e)
        {
            viewPort3d.Camera.Reset();
            viewPort3d.Camera.ZoomExtents(viewPort3d.Viewport, 1000);
        }

        private void MenuItem_CameraInfo(object sender, RoutedEventArgs e)
        {
            viewPort3d.ShowCameraInfo = !viewPort3d.ShowCameraInfo;
        }

        private void MenuItem_CameraTarget(object sender, RoutedEventArgs e)
        {
            viewPort3d.ShowCameraTarget = !viewPort3d.ShowCameraTarget;
        }

        private void MenuItem_CoordinateSystem(object sender, RoutedEventArgs e)
        {
            viewPort3d.ShowCoordinateSystem = !viewPort3d.ShowCoordinateSystem;
        }

        private void MenuItem_FieldOfView(object sender, RoutedEventArgs e)
        {
            viewPort3d.ShowFieldOfView = !viewPort3d.ShowFieldOfView;
        }

        private void MenuItem_FrameRate(object sender, RoutedEventArgs e)
        {
            viewPort3d.ShowFrameRate = !viewPort3d.ShowFrameRate;
        }

        private void MenuItem_TriangleCountInfo(object sender, RoutedEventArgs e)
        {
            viewPort3d.ShowTriangleCountInfo = !viewPort3d.ShowTriangleCountInfo;
        }

        private void MenuItem_ViewCube(object sender, RoutedEventArgs e)
        {
            viewPort3d.ShowViewCube = !viewPort3d.ShowViewCube;
        }

        private void MenuItem_CharacterCubeHelp(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("T - Top side of object\n" +
                            "B - Behind side of object\n" +
                            "D - Down side of object\n" +
                            "F - Front side of object\n" +
                            "L - Left side of object\n" +
                            "R - Right side of object\n",
                            "Character in Cube view", 
                            MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}