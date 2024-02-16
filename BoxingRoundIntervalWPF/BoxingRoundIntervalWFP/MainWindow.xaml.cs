using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Converters;
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
        private int rounds;
        private int currentRound = 1;
        private int totalRound = 0;
        private float fightTime = 0;
        private float restTime = 0;
        private float totalTrainingTime;
        private float initialRoundTime = 0;
        private float initialRestTime = 0;

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool startedTraining = false;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            manager = new BoxingRoundIntervalManager(this);
            manager.Initialize();
            InitializeCountdownTimer();
            InitializeRoundTimer();
            InitializeRestTimer();
            FindButtons();

            float totalTrainingMinutes = (int)(totalTrainingTime / 60);
            float totalTrainingSeconds = (int)(totalTrainingTime % 60);

            float roundMenuMinutes = (int)(initialRoundTime / 60);
            float roundMenuSeconds = (int)(initialRoundTime % 60);

            float restMenuMinutes = (int)(initialRestTime / 60);
            float restMenuSeconds = (int)(initialRestTime % 60);

            RoundLengthMenu = "Round Length " + ($": {roundMenuMinutes:00}:{roundMenuSeconds:00}");
            RestTimeLengthMenu = "Rest Time " + ($": {restMenuMinutes:00}:{restMenuSeconds:00}");
            _roundsMenu = "Rounds " + currentRound;
            _countdownText = "";
            TotalTrainingLengthMenu = "Total Training " + ($": {totalTrainingMinutes:00}:{totalTrainingSeconds:00}");
        }

        #region Initialization 

        private void FindButtons()
        {


            subtractRestLengthButton = (Button)FindName("subtractRestLengthButton");
            addRestLengthButton = (Button)FindName("addRestLengthButton");

            subtractRoundLengthButton = (Button)FindName("subtractRoundLengthButton");
            addRoundLengthButton = (Button)FindName("addRoundLengthButton");

            subtractRoundsButton = (Button)FindName("subtractRoundsButton");
            addRoundsButton = (Button)FindName("addRoundsButton");
        }

        private void DisableButtons()
        {
            subtractRestLengthButton.Visibility = Visibility.Collapsed;
            addRestLengthButton.Visibility = Visibility.Collapsed;

            subtractRoundLengthButton.Visibility = Visibility.Collapsed;
            addRoundLengthButton.Visibility = Visibility.Collapsed;


            subtractRoundsButton.Visibility = Visibility.Collapsed;
            addRoundsButton.Visibility = Visibility.Collapsed;
        }
        //private void DisableTexts()
        //{
        //    TotalTrainingLengthMenu = "";
        //    RoundLengthMenu = "";
        //    _restLengthMenu = "";
        //}
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
        #endregion Initialization
        private void InitializeRestTimer()
        {
            beginRestTimer = new DispatcherTimer();
            beginRestTimer.Interval = TimeSpan.FromSeconds(1);
            beginRestTimer.Tick += BeginRestTimer;
        }


        private void BeginTrainingButton(object sender, RoutedEventArgs e)
        {


            startedTraining = true;
            Button startButton = (Button)sender;
            startButton.Visibility = Visibility.Collapsed;
            BeginCountdown();
            DisableButtons();
            currentRound = 1;
            initialRoundTime = fightTime;
            initialRestTime = restTime;
            totalRound = rounds;
            //  DisableTexts();
        }
        #region AddAndSubtract
        private void TotalTrainingTime()
        {
            totalTrainingTime = currentRound * (fightTime + restTime);
            int minutes = (int)(totalTrainingTime / 60);
            int seconds = (int)(totalTrainingTime % 60);
            TotalTrainingLengthMenu = "Total Training " + ($": {minutes:00}:{seconds:00}");
        }

        private void SubtractRoundLength(object sender, EventArgs e)
        {
            subtractRoundLengthButton = (Button)sender;
            fightTime -= 5f;
            if (fightTime < 0f)
            {
                fightTime = 0f;
            }
            int minutes = (int)(fightTime / 60);
            int seconds = (int)(fightTime % 60);
            RoundLengthMenu = "Round Length " + ($": {minutes:00}:{seconds:00}");
            TotalTrainingTime();
        }


        private void AddRoundLength(object sender, EventArgs e)
        {
            addRoundLengthButton = (Button)sender;
            fightTime += 5f;
            if (fightTime < 0f)
            {
                fightTime = 0f;
            }
            int minutes = (int)(fightTime / 60);
            int seconds = (int)(fightTime % 60);
            RoundLengthMenu = "Round Length " + ($": {minutes:00}:{seconds:00}");
            TotalTrainingTime();
        }
        private void SubtractRestLength(object sender, EventArgs e)
        {
            subtractRestLengthButton = (Button)sender;
            restTime -= 5f;
            if (restTime < 0f)
            {
                restTime = 0f;
            }
            int minutes = (int)(restTime / 60);
            int seconds = (int)(restTime % 60);

            RestTimeLengthMenu = "Rest Time " + ($": {minutes:00}:{seconds:00}");
            TotalTrainingTime();
        }

        private void AddRestLength(object sender, EventArgs e)
        {
            addRestLengthButton = (Button)sender;
            restTime += 5f;
            if (restTime < 0f)
            {
                restTime = 0f;
            }
            int minutes = (int)(restTime / 60);
            int seconds = (int)(restTime % 60);

            RestTimeLengthMenu = "Rest Time " + ($": {minutes:00}:{seconds:00}");
            TotalTrainingTime();
        }


        private void SubtractRounds(object sender, EventArgs e)
        {
            addRoundsButton = (Button)sender;
            rounds -= 1;
            rounds = (int)MathF.Max(1, rounds);
            RoundsMenu = "Rounds " + rounds;
            TotalTrainingTime();
        }
        private void AddRounds(object sender, EventArgs e)
        {
            addRoundsButton = (Button)sender;
            rounds += 1;
            currentRound = (int)MathF.Max(1, rounds);
            RoundsMenu = "Rounds " + rounds;
            TotalTrainingTime();
        }
        #endregion AddAndSubtract



        #region GetReadyCountDownTimerFunctions
        public void BeginCountdown()
        {
            if (countdownTimer != null)
            {
                countdownTimer.Start();
            }
        }

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
                if (countdownTime == 0)
                {
                    countdownTimer.Stop();

                    BeginRoundCountdown();

                }
            }
        }




        private void UpdateUIGetReadyCountdown()
        {
            OnPropertyChanged(nameof(CountdownText));
            Debug.WriteLine($"GET READY Countdown!: {countdownTime / 60:00}:{countdownTime % 60:00}");
            CountdownText = "Get Ready! " + ($": {countdownTime / 60:00}:{countdownTime % 60:00}");
            _fightText = "";
            _restText = "";
            TotalTrainingLengthMenu = " ";
            RoundLengthMenu = " ";
            RestTimeLengthMenu = " ";
            RoundsMenu = " ";
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
            if (beginRoundTimer != null && startedTraining)
            {
                beginRoundTimer.Start();
            }
        }

        private void BeginRoundTimer(object? sender, EventArgs e)
        {
            if (beginRoundTimer != null && startedTraining)
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

            float minutes = fightTime / 60;
            float seconds = fightTime % 60;
            Debug.WriteLine($"Fight Time Remaining: {minutes:00}:{seconds:00}");
            FightText = "Fight " + ($": {minutes:00} : {seconds:00}");
            RestTimeLengthMenu = "";
            CountdownText = "";
            TotalTrainingLengthMenu = " ";
            RoundLengthMenu = " ";
            GameRestLength = " ";
            RoundsMenu = " ";
            RoundGame = "Current Round " + currentRound;
            TotalRoundGame = "Total Round " + totalRound;
            OnPropertyChanged(nameof(FightText));
            Debug.WriteLine("FIGHT!");
        }
        #endregion

        #region RestFunctions
        public void BeginRestCountdown()
        {
            if (beginRestTimer != null && startedTraining)
            {
                beginRestTimer.Start();
            }
        }

        private void BeginRestTimer(object? sender, EventArgs e)
        {
            if (beginRestTimer != null && startedTraining)
            {
                if (restTime > 0)
                {
                    restTime--;
                    UpdateUIRest();
                }
                else if (restTime == 0)
                {
                    restTime = initialRestTime;
                    beginRestTimer.Stop();
                    BeginRoundCountdown();
                    currentRound += 1;
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
        private string _countdownText = "";

        private void UpdateUIRest()
        {
            float minutes = restTime / 60;
            float seconds = restTime % 60;
            Debug.WriteLine($"Rest Time Remaining: {minutes:00}:{seconds:00}");
            GameRestLength = "Rest Time " + ($": {minutes:00}:{seconds:00}");
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


        private string _restText = "";
        #endregion

        #region MenuUI
        public string TotalTrainingLengthMenu
        {
            get { return _totalTrainingLengthMenu; }
            set
            {
                _totalTrainingLengthMenu = value;
                OnPropertyChanged();
            }
        }
        private string _totalTrainingLengthMenu = "";
        public string RestTimeLengthMenu
        {
            get { return _restTimeLengthMenu; }
            set
            {
                _restTimeLengthMenu = value;
                OnPropertyChanged();
            }
        }
        private string _restTimeLengthMenu = "";
        public string RoundLengthMenu
        {
            get { return _roundLengthMenu; }
            set
            {
                _roundLengthMenu = value;
                OnPropertyChanged();
            }
        }
        private string _roundLengthMenu = "";


        private string _restLengthMenu = "";


        public string GameRestLength
        {
            get { return _gameRestLength; }
            set
            {
                _gameRestLength = value;
                OnPropertyChanged();
            }
        }
        private string _gameRestLength = "";


        public string RoundsMenu
        {
            get { return _roundsMenu; }
            set
            {
                _roundsMenu = value;
                OnPropertyChanged();
            }
        }
        private string _roundsMenu = "";
        #endregion MenuUI


        public string RoundGame
        {

            get { return roundGame; }
            set
            {
                roundGame = value;
                OnPropertyChanged();
            }
        }
        private string roundGame = "";


        public string TotalRoundGame
        {
            get { return _totalgameMenu; }
            set
            {
                _totalgameMenu = value;
                OnPropertyChanged();
            }
        }
        private string _totalgameMenu = "";
    }
}


