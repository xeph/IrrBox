using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        protected IrrKlang.ISoundEngine irrKlangEngine;
        protected IrrKlang.ISound currentlyPlayingSound;

        public MainWindow()
        {
            InitializeComponent();
            
            irrKlangEngine = new IrrKlang.ISoundEngine();
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Dark);

            SelectFileButton.Click += new System.Windows.RoutedEventHandler(this.SelectFileButton_Click);
            PauseButton.Click += new System.Windows.RoutedEventHandler(this.PauseButton_Click);
        }

        private void PlaylistButtonClick(object sender, RoutedEventArgs e)
        {
            Flyouts[0].IsOpen = !Flyouts[0].IsOpen;
        }

        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            Flyouts[1].IsOpen = !Flyouts[1].IsOpen;
        }


        void playSelectedFile()
        {
            // stop currently playing sound

            if (currentlyPlayingSound != null)
                currentlyPlayingSound.Stop();

            // start new sound
            
            currentlyPlayingSound = irrKlangEngine.Play2D(filenameTextBox.Text, true);

            // update controls to display the playing file

            UpdatePauseButtonText();

            volumeTrackBar.Value = 100;
        }

        private void PauseButton_Click(object sender, System.EventArgs e)
        {
            if (currentlyPlayingSound != null)
            {
                currentlyPlayingSound.Paused = !currentlyPlayingSound.Paused;
                UpdatePauseButtonText();
            }
        }

        private void UpdatePauseButtonText()
        {
            if (currentlyPlayingSound != null)
            {
                if (currentlyPlayingSound.Paused)
                {
                    PausePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.control.play.png"), UriKind.Relative));
                }
                else
                {
                    PausePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.control.pause.png"), UriKind.Relative));
                }
            }
            else
                PauseButton.Content = "";
        }


        // Sets new volume of currently playing sound
        private void volumeTrackBar_Scroll(object sender, System.EventArgs e)
        {
            if (currentlyPlayingSound != null)
            {
                float volume = (float) volumeTrackBar.Value / 100.0f;
                currentlyPlayingSound.Volume =  volume;

                if (volume == 0)
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.mute.png"), UriKind.Relative));
                else if (volume > 0 && volume <= 0.25)
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.0.png"), UriKind.Relative));
                else if (volume > 0.25 && volume <= 0.50)
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.1.png"), UriKind.Relative));
                else if (volume > 0.50 && volume <= 0.75)
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.2.png"), UriKind.Relative));
                else if (volume > 0.75 && volume <= 1)
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.3.png"), UriKind.Relative));
            }
        }


        // selects a new file to play
        private void SelectFileButton_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();

            dialog.Filter = "All playable files (*.mp3;*.ogg;*.wav;*.mod;*.xm;*.it;*.s3d;*.flac)|*.mp3;*.ogg;*.wav;*.mod;*.xm;*.it;*.s3d;*.flac|MP3 files (*.mp3)|*.mp3|OGG files (*.ogg)|*.ogg|FLAC files (*.flac)|*.flac|Wave files (*.wav)|*.wav";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filenameTextBox.Text = dialog.FileName;
                playSelectedFile();
            }
        }
    }
}
