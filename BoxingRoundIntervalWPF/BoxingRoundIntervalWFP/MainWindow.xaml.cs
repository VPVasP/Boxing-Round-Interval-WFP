using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BoxingRoundIntervalWFP
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private BoxingRoundIntervalManager manager;
        private DispatcherTimer countdownTimer = new DispatcherTimer();
        private DispatcherTimer beginRoundTimer = new DispatcherTimer();
        private DispatcherTimer beginRestTimer = new DispatcherTimer();

        private int countdownTime = 5;
        private int currentRound = 1;
        private int fightTime = 5;
        private int restTime = 5;

        private int initialRoundTime = 5;
        private int initialRestTime = 5;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            manager = new BoxingRoundIntervalManager(this);
            manager.Initialize();
            InitializeCountdownTimer();
            InitializeRoundTimer();
            InitializeRestTimer();
            BeginCountdown();
        }

        #region Initialization 
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
        #endregion

        #region GetReadyCountDownTimerFunctions
        private void GetReadyCountdownTimer(object? sender, EventArgs e)
        {
            if (countdownTimer != null)
            {
                if (countdownTime > 0)
                {
                    countdownTime--;
                    UpdateUIGetReadyCountdown();
                    OnPropertyChanged(nameof(CountdownText));
                }
               if(countdownTime==0)
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
            OnPropertyChanged(nameof(CountdownText));
            Debug.WriteLine($"Time Remaining: {countdownTime / 60:00}:{countdownTime % 60:00}");
            _countdownText = "Get Ready! " + ($"Time Remaining: {countdownTime / 60:00}:{countdownTime % 60:00}");
            _fightText = "";
            _restText = "";
            Debug.WriteLine("GET READY!");
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region RoundFunctions
        public void BeginRoundCountdown()
        {
            if (beginRoundTimer != null)
            {
                beginRoundTimer.Start();
            }
        }

        private void BeginRoundTimer(object? sender, EventArgs e)
        {
            if (beginRoundTimer != null)
            {
                if (fightTime > 0)
                {
                    fightTime--;
                    UIFight();
                }
                if (fightTime == 0)
                {
                    fightTime = initialRoundTime;
                    beginRoundTimer.Stop();
                    BeginRestCountdown();
                }
            }
        }

        private void UIFight()
        {
          


           
            int minutes = fightTime / 60;
            int seconds = fightTime % 60;
            Debug.WriteLine($"Time Remaining: {minutes:00}:{seconds:00}");
            FightText = "Fight " + ($"\"Time Remaining: {minutes:00}:{seconds:00}");
            RestText = "";
            CountdownText = "";
            OnPropertyChanged(nameof(FightText));
            Debug.WriteLine("FIGHT!");
        }
        #endregion

        #region RestFunctions
        public void BeginRestCountdown()
        {
            if (beginRestTimer != null)
            {
                beginRestTimer.Start();
            }
        }

        private void BeginRestTimer(object? sender, EventArgs e)
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
        public string CountdownText
        {
            get { return _countdownText; }
            set
            {
                _countdownText = value;
                OnPropertyChanged(); 
            }
        }
        private string _countdownText="";

        private void UpdateUIRest()
        {
            int minutes = restTime / 60;
            int seconds = restTime % 60;
            Debug.WriteLine($"Time Remaining: {minutes:00}:{seconds:00}");
            RestText = "Rest "+ ($"\"Time Remaining: {minutes:00}:{seconds:00}");
            FightText = "";
            CountdownText = "";
        }
        #endregion

        #region Properties
        public string FightText
        {
            get { return _fightText; }
            set
            {
                _fightText = value;
                OnPropertyChanged();
            }
        }
        private string _fightText = "";

        public string RestText
        {
            get { return _restText; }
            set
            {
                _restText = value;
                OnPropertyChanged();
            }
        }
        private string _restText = "";
        #endregion
    }


}
