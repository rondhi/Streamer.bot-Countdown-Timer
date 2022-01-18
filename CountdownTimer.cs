using System;
using System.Timers;

class CPHInline
{
    public System.Timers.Timer countdownTimer;
    public int countdownSecondsLeft;
    public int countdownTotalTimeInSeconds;

    public void Init()
    {
        countdownTimer = new System.Timers.Timer(1000);
        countdownTimer.Elapsed += OnTimedEvent;
        countdownTimer.AutoReset = true;
        countdownTimer.Enabled = true;
        countdownTimer.Stop();
    }

    public void Dispose()
    {
        countdownTimer.Dispose();
    }

    private void StopTimer(string message) 
    {
        // Set to Scene and Source of your text source
        CPH.ObsSetGdiText("[NS] Countdown Timer", "[TS] Countdown Timer", message);
        countdownTimer.Stop();
    }

    private void AddMinutes(int countdownMinutesToAdd)
    {
        int countdownSecondsToAdd = countdownMinutesToAdd * 60;
        countdownTotalTimeInSeconds = countdownTotalTimeInSeconds + countdownSecondsToAdd;
        countdownSecondsLeft = countdownSecondsLeft + countdownSecondsToAdd;
    }

    public void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        countdownSecondsLeft--;
        TimeSpan time = TimeSpan.FromSeconds(countdownSecondsLeft);
        string countdownString = time.ToString(@"hh\:mm\:ss");
        if (countdownSecondsLeft == 0)
        {
            StopTimer("Time's up!");
        }
        else
        {
            // Set to Scene and Source of your text source
            CPH.ObsSetGdiText("[NS] Countdown Timer", "[TS] Countdown Timer", countdownString);
        }
    }

    public bool Execute()
    {
        // Change countdownHourValue to initial length of stream in minutes
        int countdownHourValue = 10;
        countdownSecondsLeft = countdownHourValue * (60) + 1;
        countdownTotalTimeInSeconds = countdownSecondsLeft;
        countdownTimer.Start();
        return true;
    }
	
    public bool Stop()
    {
        StopTimer("Timer cancelled!");
        countdownTimer.Stop();
		return true;
    }
	
    public bool Reset()
    {
        int countdownSecondsLeft = 0;
        countdownTimer.Stop();
		return true;
    }

    public bool Add1()
    {
        // Change countdownMinuteValue to minutes to add to the timer
        int countdownMinuteValue = 1;
        AddMinutes(countdownMinuteValue);
        return true;
    }

    public bool Add5()
    {
        // Change countdownMinuteValue to minutes to add to the timer
        int countdownMinuteValue = 5;
        AddMinutes(countdownMinuteValue);
        return true;
    }

    public bool Add10()
    {
        // Change countdownMinuteValue to minutes to add to the timer
        int countdownMinuteValue = 10;
        AddMinutes(countdownMinuteValue);
        return true;
    }
}
