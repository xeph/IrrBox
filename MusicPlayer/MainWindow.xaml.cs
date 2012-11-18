using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MusicPlayer
{
    public partial class MainWindow : MetroWindow
    {
        protected IrrKlang.ISoundEngine irrKlangEngine;
        protected IrrKlang.ISound currentlyPlayingSound;
        private IrrBox.SongList songList = new IrrBox.SongList();

        #region Constructor/Destructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            irrKlangEngine = new IrrKlang.ISoundEngine();
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Dark);
            volumeTrackBar.Value = 100;

            ListViewPlaylist.AddHandler(MetroContentControl.MouseDoubleClickEvent, new RoutedEventHandler(Playlist_DoubleClick));
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~MainWindow()
        {
            if (currentlyPlayingSound != null)
                currentlyPlayingSound.Dispose();
            irrKlangEngine.Dispose();
            songList = null;
        }
        #endregion
        #region UI Interaction
        /// <summary>
        /// Playlist button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaylistButtonClick(object sender, RoutedEventArgs e)
        {
            Flyouts[0].IsOpen = !Flyouts[0].IsOpen;
        }

        /// <summary>
        /// Settings button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            Flyouts[1].IsOpen = !Flyouts[1].IsOpen;
        }

        /// <summary>
        /// Stop button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, System.EventArgs e)
        {
            if (currentlyPlayingSound != null)
            {
                currentlyPlayingSound.Stop();
                currentlyPlayingSound = null;
            }
        }

        /// <summary>
        /// Pause button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseButton_Click(object sender, System.EventArgs e)
        {
            if (currentlyPlayingSound != null)
            {
                currentlyPlayingSound.Paused = !currentlyPlayingSound.Paused;
                UpdatePauseButtonText();
            }
        }

        /// <summary>
        /// Playlist double click on selection action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Playlist_DoubleClick(object sender, RoutedEventArgs e)
        {
            playSelectedFile(songList.getSong(ListViewPlaylist.SelectedIndex).path);
            currentlyPlayingSound.Volume = getCurrentVolume();
            songList.currentSong = ListViewPlaylist.SelectedIndex;
        }

        /// <summary>
        /// Volume slider value changed action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void volumeTrackBar_Scroll(object sender, System.EventArgs e)
        {
            if (currentlyPlayingSound != null)
            {
                float volume = getCurrentVolume();
                currentlyPlayingSound.Volume = volume;

                if (volume == 0)
                {
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.mute.png"), UriKind.Relative));
                }
                else if (volume > 0 && volume <= 0.25)
                {
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.0.png"), UriKind.Relative));
                }
                else if (volume > 0.25 && volume <= 0.50)
                {
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.1.png"), UriKind.Relative));
                }
                else if (volume > 0.50 && volume <= 0.75)
                {
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.2.png"), UriKind.Relative));
                }
                else if (volume > 0.75 && volume <= 1)
                {
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.3.png"), UriKind.Relative));
                }
                else
                {
                    volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.3.png"), UriKind.Relative));
                }
            }
        }

        private void VolumeButton_Click(object sender, System.EventArgs e)
        {
            if (getCurrentVolume() <= 0)
            {
                currentlyPlayingSound.Volume = 100;
                volumeTrackBar.Value = volumeTrackBar.Maximum;
                volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.3.png"), UriKind.Relative));
            }
            else
            {
                currentlyPlayingSound.Volume = 0;
                volumeTrackBar.Value = volumeTrackBar.Minimum;
                volumePicture.Source = new BitmapImage(new Uri(String.Format(@"./Icons/appbar.sound.mute.png"), UriKind.Relative));
            }
        }

        /// <summary>
        /// Previous button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousButton_Click(object sender, System.EventArgs e)
        {
            if (songList.Count() > 0 && songList.currentSong >= 1)
            {
                playSelectedFile(songList.getSong(songList.currentSong - 1).path);
                currentlyPlayingSound.Volume = getCurrentVolume();
                songList.currentSong -= 1;
            }
        }

        /// <summary>
        /// Next button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, System.EventArgs e)
        {
            if (songList.Count() > 0 && songList.currentSong < songList.Count() - 1)
            {
                playSelectedFile(songList.getSong(songList.currentSong + 1).path);
                currentlyPlayingSound.Volume = getCurrentVolume();
                songList.currentSong += 1;
            }
        }

        /// <summary>
        /// Select file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFileButton_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();

            dialog.Multiselect = true;
            dialog.Filter = "All playable files (*.mp3;*.ogg;*.wav;*.mod;*.xm;*.it;*.s3d;*.flac)|*.mp3;*.ogg;*.wav;*.mod;*.xm;*.it;*.s3d;*.flac|MP3 files (*.mp3)|*.mp3|OGG files (*.ogg)|*.ogg|FLAC files (*.flac)|*.flac|Wave files (*.wav)|*.wav|MOD files (*.mod)|*.mod|XM files (*.xm)|*.xm|IT files (*.it)|*.it|S3D files (*.s3d)|*.s3d";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                songList = new IrrBox.SongList();
                ListViewPlaylist.Items.Clear();
                int i = 1;
                foreach (string filename in dialog.FileNames)
                {
                    IrrBox.Song song = new IrrBox.Song();
                    song.path = filename;
                    songList.addSong(song);
                    ListViewPlaylist.Items.Add(i + " - " + song.path);
                    i += 1;
                }
                playSelectedFile(songList.getSong(0).path);
                currentlyPlayingSound.Volume = getCurrentVolume();
            }
        }
        #endregion
        #region Misc functions
        /// <summary>
        /// Play the selected file using the path
        /// </summary>
        /// <param name="filename"></param>
        void playSelectedFile(string filename)
        {
            if (currentlyPlayingSound != null)
            {
                currentlyPlayingSound.Stop();
            }
            
            currentlyPlayingSound = irrKlangEngine.Play2D(filename, true);

            UpdatePauseButtonText();
        }

        /// <summary>
        /// Update the pause button icon
        /// </summary>
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
        }

        /// <summary>
        /// Get the current volume
        /// </summary>
        /// <returns></returns>
        private float getCurrentVolume()
        {
            return (float)volumeTrackBar.Value / 100.0f;
        }
        #endregion
    }
}