using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace BoxingRoundIntervalWFP
{

    public partial class MainWindow : Window
    {
        private BoxingRoundIntervalManager manager;
        private DispatcherTimer countdownTimer;
        private DispatcherTimer beginRoundTimer;
        private DispatcherTimer beginRestTimer;

        private int countdownTime = 5;
        private int roundTime = 5;
        private int restTime = 5;

        private int initialRoundTime = 5;
        private int initialRestTime = 5;
        public MainWindow()
        {
            InitializeComponent();
            manager = new BoxingRoundIntervalManager(this);
            manager.Initialize();
            InitializeCountdownTimer();
            InitializeRoundTimer();
            InitializeRestTimer();
            BeginCountdown();
        }
        #region Initialiazation 
        private void InitializeCountdownTimer()
        {
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += GetReadyCountdownTimer;
        }
        private void InitializeRoundTimer()
        {
            beginRoundTimer = new DispatcherTimer();
            beginRoundTimer.Interval = TimeSpan.FromSeconds(1);
            beginRoundTimer.Tick += BeginRoundTimer;
        }
        private void InitializeRestTimer()
        {
            beginRestTimer = new DispatcherTimer();
            beginRestTimer.Interval = TimeSpan.FromSeconds(1);
            beginRestTimer.Tick += BeginRestTimer;
        }
        #endregion #region Initialiazation 
        #region GetReadyCountDownTimerFunctions
        private void GetReadyCountdownTimer(object sender, EventArgs e)
        {
            if (countdownTimer != null)
            {
                if (countdownTime > 0)
                {
                    countdownTime--;
                    UpdateUIGetReadyCountdown();
                }
                if (countdownTime == 0)
                {
                    countdownTimer.Stop();
                    BeginRoundCountdown();
                }
            }
        }
        public void BeginCountdown()
        {
            if (countdownTimer != null)
            {
                countdownTimer.Start();
            }
        }
        private void UpdateUIGetReadyCountdown()
        {
            int minutes = countdownTime / 60;
            int seconds = countdownTime % 60;
            Debug.WriteLine($"Time Remaining: {minutes:00}:{seconds:00}");
            Debug.WriteLine("GET READY!");
        }

        #endregion GetReadyCountDownTimerFunctions
        #region RoundFunctions
        public void BeginRoundCountdown()
        {
            if (beginRoundTimer != null)
            {
                beginRoundTimer.Start();
            }

        }
        private void BeginRoundTimer(object sender, EventArgs e)
        {
            if (beginRoundTimer != null)
            {
                if (roundTime > 0)
                {
                    roundTime--;
                    UpdateUIRound();
                }
                if (roundTime == 0)
                {
                    roundTime = initialRoundTime;
                    beginRoundTimer.Stop();
                    BeginRestCountdown();
                }
            }
        }
        private void UpdateUIRound()
        {
            int minutes = roundTime / 60;
            int seconds = roundTime % 60;
            Debug.WriteLine($"Time Remaining: {minutes:00}:{seconds:00}");
            Debug.WriteLine("FIGHT!");
        }

    
    #endregion RoundFunctions

    #region RestFunctions
    public void BeginRestCountdown()
    {
        if (beginRestTimer != null)
        {
          beginRestTimer.Start();
        }

    }
    private void BeginRestTimer(object sender, EventArgs e)
    {
        if (beginRestTimer != null)
        {
            if (restTime > 0)
            {
                restTime--;
                UpdateUIRest();
            }
            if (restTime == 0)
            {
              restTime = initialRestTime;
              beginRestTimer.Stop();
              BeginRoundCountdown();
             }
        }
    }
    private void UpdateUIRest()
    {
        int minutes = restTime / 60;
        int seconds = restTime % 60;
        Debug.WriteLine($"Time Remaining: {minutes:00}:{seconds:00}");
        Debug.WriteLine("Rest!");
    }

}
    #endregion RestFunctions
}