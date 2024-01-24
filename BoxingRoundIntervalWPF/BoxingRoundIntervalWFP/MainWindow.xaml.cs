using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace BoxingRoundIntervalWFP
{
    
    public partial class MainWindow : Window
    {
        private BoxingRoundIntervalManager manager;
        private DispatcherTimer countdownTimer;
        private int countdownTime = 5;

        public MainWindow()
        {
            InitializeComponent();
            manager = new BoxingRoundIntervalManager(this);
            manager.Initialize();
            InitializeCountdownTimer();
            BeginCountdown();
        }

        private void InitializeCountdownTimer()
        {
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += GetReadyCountdownTimer;
        }

        private void GetReadyCountdownTimer(object sender, EventArgs e)
        {
            if (countdownTimer != null) 
            {
                if (countdownTime > 0)
                {
                    countdownTime--;
                    UpdateUI();
                }
                else
                {
                    countdownTimer.Stop();
                }
            }
        }

        private void UpdateUI()
        {
            int minutes = countdownTime / 60;
            int seconds = countdownTime % 60;
            Debug.WriteLine($"Time Remaining: {minutes:00}:{seconds:00}");
            Debug.WriteLine("GET READY!");
        }

       public void BeginCountdown()
        {
            if (countdownTimer != null)
            {
                countdownTimer.Start();
            }
        }
    }
}