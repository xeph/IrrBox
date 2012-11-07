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
    public partial class MainWindow : Window
    {
        protected IrrKlang.ISoundEngine irrKlangEngine;
        protected IrrKlang.ISound currentlyPlayingSound;

        public MainWindow()
        {
            InitializeComponent();

            irrKlangEngine = new IrrKlang.ISoundEngine();

            SelectFileButton.Click += new System.Windows.RoutedEventHandler(this.SelectFileButton_Click);
            PauseButton.Click += new System.Windows.RoutedEventHandler(this.PauseButton_Click);
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
                    PauseButton.Content = "Play";
                else
                    PauseButton.Content = "Pause";
            }
            else
                PauseButton.Content = "";
        }


        // Sets new volume of currently playing sound
        private void volumeTrackBar_Scroll(object sender, System.EventArgs e)
        {
            if (currentlyPlayingSound != null)
            {
                //currentlyPlayingSound.Volume = volumeTrackBar.Value / 100.0f;
            }
        }


        // selects a new file to play
        private void SelectFileButton_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new
                System.Windows.Forms.OpenFileDialog();

            dialog.Filter = "All playable files (*.mp3;*.ogg;*.wav;*.mod;*.it;*.xm;*.it;*.s3d)|*.mp3;*.ogg;*.wav;*.mod;*.it;*.xm;*.it;*.s3d;*.flac";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filenameTextBox.Text = dialog.FileName;
                playSelectedFile();
            }
        }
    }
}
