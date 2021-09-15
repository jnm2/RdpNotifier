using RdpNotifier.Properties;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Media;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RdpNotifier
{
    public sealed class MainViewModel : ViewModel, IDisposable
    {
        private readonly Func<string?> selectAudioFile;
        private readonly Action flashWindow;
        private SoundPlayer? soundPlayer;
        private readonly System.Windows.Forms.Timer timer = new() { Interval = 2000 };
        private bool isInitialLoad;
        private bool addressWasLastSeenOffline;

        public MainViewModel(Func<string?> selectAudioFile, Action flashWindow)
        {
            this.selectAudioFile = selectAudioFile;
            this.flashWindow = flashWindow;

            playSound = TryLoadSound(Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                @"Media\Windows Message Nudge.wav"))
                    && Settings.Default.PlaySound;

            timer.Tick += (_, _) => Refresh();

            Address = Settings.Default.Address;
        }

        public void Dispose()
        {
            timer.Dispose();
            soundPlayer?.Dispose();
        }

        private string? address;
        public string? Address
        {
            get => address;
            set
            {
                if (!Set(ref address, value)) return;
                Status = null;
                timer.Stop();
                isInitialLoad = true;
                addressWasLastSeenOffline = false;
                timer.Start();

                Settings.Default.Address = value;
                Settings.Default.Save();
            }
        }

        private bool playSound;
        public bool PlaySound
        {
            get => playSound;
            set
            {
                if (!Set(ref playSound, value)) return;

                if (playSound && soundPlayer is null)
                {
                    var fileName = selectAudioFile.Invoke();
                    if (fileName is null || !TryLoadSound(fileName))
                    {
                        playSound = false;
                        OnPropertyChanged();
                        return;
                    }
                }

                Settings.Default.PlaySound = value;
                Settings.Default.Save();
            }
        }

        private bool TryLoadSound(string path)
        {
            soundPlayer?.Dispose();
            soundPlayer = new(path);

            try
            {
                soundPlayer.Load();
                return true;
            }
            catch (FileNotFoundException)
            {
                soundPlayer.Dispose();
                soundPlayer = null;
                return false;
            }
        }

        private bool isLoading;
        public bool IsLoading { get => isLoading; private set => Set(ref isLoading, value); }

        private string? status;
        public string? Status { get => status; private set => Set(ref status, value); }

        private Color statusColor;
        public Color StatusColor { get => statusColor; private set => Set(ref statusColor, value); }

        public async void Refresh()
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                Status = null;
                return;
            }

            if (TryParseHostAndPort(address) is not var (host, port))
            {
                Status = "Please enter a valid address.";
                StatusColor = SystemColors.ControlText;
                return;
            }

            bool isOnline;

            if (isInitialLoad)
            {
                isInitialLoad = false;
                IsLoading = true;
            }
            try
            {
                var originalAddress = address;

                if (port is not (null or 443))
                {
                    isOnline = await ConnectionUtils.IsPortOpenAsync(new DnsEndPoint(host, port.Value), CancellationToken.None);
                }
                else
                {
                    var cancellationSource = new CancellationTokenSource();
                    var portTask = ConnectionUtils.IsPortOpenAsync(new DnsEndPoint(host, port ?? 3389), cancellationSource.Token);
                    var gatewayTask = ConnectionUtils.IsRdpGatewayOnlineAsync(new DnsEndPoint(host, port ?? 443), cancellationSource.Token);

                    var firstCompletedTaskReturnedTrue = (await Task.WhenAny(portTask, gatewayTask)).GetAwaiter().GetResult();
                    if (firstCompletedTaskReturnedTrue)
                    {
                        cancellationSource.Cancel();
                        isOnline = true;
                    }
                    else
                    {
                        isOnline = await portTask || await gatewayTask;
                    }
                }

                if (address != originalAddress) return;
            }
            finally
            {
                IsLoading = false;
            }

            (Status, StatusColor) = isOnline
                ? (string.Concat("✔ ", address.AsSpan().Trim(), " is online"), Color.FromArgb(0, 160, 0))
                : (string.Concat("❌ ", address.AsSpan().Trim(), " is offline"), Color.FromArgb(240, 0, 0));

            if (isOnline && addressWasLastSeenOffline)
            {
                flashWindow.Invoke();
                if (PlaySound) soundPlayer?.Play();
            }

            addressWasLastSeenOffline = !isOnline;
        }

        private static (string Host, int? Port)? TryParseHostAndPort(string value)
        {
            var portSeparatorIndex = value.IndexOf(':');
            if (portSeparatorIndex == -1)
            {
                return (Host: value.Trim(), Port: null);
            }

            if (int.TryParse(value.AsSpan(portSeparatorIndex + 1), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture, out var port))
            {
                return (Host: value.AsSpan(0, portSeparatorIndex).Trim().ToString(), port);
            }

            return null;
        }
    }
}
