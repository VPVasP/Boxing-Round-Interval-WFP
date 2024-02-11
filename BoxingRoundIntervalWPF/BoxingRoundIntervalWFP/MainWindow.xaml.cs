﻿using System;
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

        private int countdownTime = 0;
        private int currentRound = 1;
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
            BeginCountdown();
            TotalTrainingLengthMenu = "Total Training ";

            float totalTrainingMinutes = (int)(totalTrainingTime / 60);
            float totalTrainingSeconds = (int)(totalTrainingTime % 60);


            float roundMenuMinutes = (int)(initialRoundTime / 60);
            float roundMenuSeconds = (int)(initialRoundTime % 60);

            float restMenuMinutes = (int)(initialRestTime / 60);
            float restMenuSeconds = (int)(initialRestTime % 60);

            TotalTrainingLengthMenu = "Total Training " + ($"\": {totalTrainingMinutes:00}:{totalTrainingSeconds:00}");
            RoundLengthMenu = "Round Length " + ($"\"RoundLength: {roundMenuMinutes:00}:{roundMenuSeconds:00}");
            _restLengthMenu = "Rest Time " + ($"\"RoundLength: {restMenuMinutes:00}:{restMenuSeconds:00}");
            _roundsMenu = "Rounds " + currentRound;
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
        #endregion Initialization
        private void InitializeRestTimer()
        {
            beginRestTimer = new DispatcherTimer();
            beginRestTimer.Interval = TimeSpan.FromSeconds(1);
            beginRestTimer.Tick += BeginRestTimer;
        }
        #region AddAndSubtract
        private void TotalTrainingTime()
        {
            totalTrainingTime = initialRoundTime * MathF.Max(0f, initialRoundTime * restTime);
            int minutes = (int)(totalTrainingTime / 60);
            int seconds = (int)(totalTrainingTime % 60);
            TotalTrainingLengthMenu = "Total Training " + $"\": {minutes:00}:{seconds:00}\"";
        }

        private void SubtractRoundLength(object sender, EventArgs e)
        {
            Button subtractRoundsLengthButton = (Button)sender;
            initialRoundTime -= 5f;
            if (initialRoundTime < 0f)
            {
                initialRoundTime = 0f;
            }
            int minutes = (int)(initialRoundTime / 60);
            int seconds = (int)(initialRoundTime % 60);
            RoundLengthMenu = "Round Length " + $"\": {minutes:00}:{seconds:00}\"";
            totalTrainingTime += initialRoundTime * MathF.Max(0f, initialRoundTime * restTime);
            //TotalTrainingTime();
        }

        private void AddRoundLength(object sender, EventArgs e)
        {
            Button addRoundsLengthButton = (Button)sender;
            initialRoundTime += 5f;
            if (initialRoundTime < 0f)
            {
                initialRoundTime = 0f;
            }
            int minutes = (int)(initialRoundTime / 60);
            int seconds = (int)(initialRoundTime % 60);
            RoundLengthMenu = "Round Length " + $"\": {minutes:00}:{seconds:00}\"";
            totalTrainingTime += initialRoundTime * MathF.Max(0f, initialRoundTime * restTime);
            TotalTrainingLengthMenu = "Total Training " + $"\": {minutes:00}:{seconds:00}\"";
            //TotalTrainingTime();
        }
        private void SubtractRestLength(object sender, EventArgs e)
        {
            Button decreaseRestLengthButton = (Button)sender;
            restTime -= 5f;
            if (restTime < 0f)
            {
                restTime = 0f;
            }
            int minutes = (int)(restTime / 60);
            int seconds = (int)(restTime % 60);

            RestTimeMenu = "Rest Length " + $"\": {minutes:00}:{seconds:00}\"";
            totalTrainingTime -= restTime * MathF.Max(0f, initialRoundTime * restTime);
            TotalTrainingLengthMenu = "Total Training " + $"\": {minutes:00}:{seconds:00}\"";
        }
        private void AddRestLength(object sender, EventArgs e)
        {
            Button addRestLengthButton = (Button)sender;
            restTime += 5f;
            if (restTime < 0f)
            {
                restTime = 0f;
            }
            int minutes = (int)(restTime / 60);
            int seconds = (int)(restTime % 60);

            RestTimeMenu = "Rest Length " + $"\": {minutes:00}:{seconds:00}\"";
            totalTrainingTime += restTime * MathF.Max(0f, initialRoundTime * restTime);
            TotalTrainingLengthMenu = "Total Training " + $"\": {minutes:00}:{seconds:00}\"";
        }


        private void SubtractRounds(object sender, EventArgs e)
        {
            Button addRoundsButton = (Button)sender;
            currentRound -= 1;
            currentRound = (int)MathF.Max(1, currentRound);
            RoundsMenu = "Rounds " + currentRound;
        }
        private void AddRounds(object sender, EventArgs e)
        {
            Button addRoundsButton = (Button)sender;
            currentRound += 1;
            currentRound = (int)MathF.Max(1, currentRound);
            RoundsMenu = "Rounds " + currentRound;
            
        }
        #endregion AddAndSubtract


        private void BeginTrainingButton(object sender, RoutedEventArgs e)
        {

            startedTraining = true;
            Button startButton = (Button)sender;
            startButton.Visibility = Visibility.Collapsed;
            BeginCountdown();
        }
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
                if (countdownTime == 0)
                {
                    countdownTimer.Stop();

                    BeginRoundCountdown();

                }
            }
        }

        public void BeginCountdown()
        {
            if (countdownTimer != null && startedTraining)
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
            TotalTrainingLengthMenu = " ";
            RoundLengthMenu = " ";
            RestTimeMenu = " ";
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
            Debug.WriteLine($"Time Remaining: {minutes:00}:{seconds:00}");
            FightText = "Fight " + ($"\"Time Remaining: {minutes:00}:{seconds:00}");
            RestText = "";
            CountdownText = "";
            TotalTrainingLengthMenu = " ";
            RoundLengthMenu = " ";
            RestTimeMenu = " ";
            RoundsMenu = " ";
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
        private string _countdownText = "";

        private void UpdateUIRest()
        {
            float minutes = restTime / 60;
            float seconds = restTime % 60;
            Debug.WriteLine($"Time Remaining: {minutes:00}:{seconds:00}");
            RestText = "Rest " + ($"\"Time Remaining: {minutes:00}:{seconds:00}");
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
    
        public string RoundLengthMenu
        {
            get { return _roundLengthMenu; }
            set
            {
                _roundLengthMenu  = value;
                OnPropertyChanged();
            }
        }
        private string _roundLengthMenu= "";

        public string RestTimeMenu
        {
            get { return _restLengthMenu; }
            set
            {
                _restLengthMenu = value;
                OnPropertyChanged();
            }
        }
        private string _restLengthMenu = "";

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

    }
}

