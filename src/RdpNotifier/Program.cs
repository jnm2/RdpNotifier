using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace RdpNotifier
{
    public static class Program
    {
        private static MainForm? mainForm;

        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var viewModel = new MainViewModel(SelectAudioFile, FlashWindow);

            mainForm = new(viewModel);
            Application.Run(mainForm);
        }

        private static string? SelectAudioFile()
        {
            using var browser = new OpenFileDialog { Title = "Choose alert sound", Filter = "WAV audio|*.wav" };

            return browser.ShowDialog(mainForm) == DialogResult.OK
                ? browser.FileName
                : null;
        }

        private static void FlashWindow()
        {
            PInvoke.FlashWindowEx(new FLASHWINFO
            {
                cbSize = (uint)Marshal.SizeOf<FLASHWINFO>(),
                dwFlags = FLASHWINFO_FLAGS.FLASHW_ALL | FLASHWINFO_FLAGS.FLASHW_TIMERNOFG,
                hwnd = (HWND)mainForm!.Handle,
            });
        }
    }
}
