//In references tab >  rightclick under mscorlib.dll > "Add reference from file..."
//Add "System.dll" as a reference
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

    public bool Execute()
    {
        var num = args["rawInput"];
        int number = Convert.ToInt32(num);
        int countdownMinuteValue = number;
        countdownSecondsLeft = countdownMinuteValue * (60) + 1;
        CPH.SetGlobalVar("defaultMinuteValue", countdownMinuteValue, true);
        countdownTimer.Start();
        return true;
    }

    public void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        countdownSecondsLeft--;
        TimeSpan time = TimeSpan.FromSeconds(countdownSecondsLeft);
        string countdownString = time.ToString(@"hh\:mm\:ss");
        if (countdownSecondsLeft == 0)
        {
            StopTimer("Time's up!");
            CPH.RunAction("TimerDone");
        }
        else
        {
            // Set to Scene and Source of your text source
            CPH.ObsSetGdiText("[NS] Countdown Timer", "[TS] Countdown Timer", countdownString);
        }
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
        countdownSecondsLeft = countdownSecondsLeft + countdownSecondsToAdd;
    }

    public bool Stop()
    {
        StopTimer("Timer Cancelled!");
        return true;
    }

    public bool Set()
    {
        var num = args["rawInput"];
        int number = Convert.ToInt32(num);
        int countdownMinuteValue = number;
        int countdownSecondsLeft = countdownMinuteValue * (60);
        CPH.SetGlobalVar("defaultMinuteValue", countdownMinuteValue, true);
        TimeSpan time = TimeSpan.FromSeconds(countdownSecondsLeft);
        string countdownString = time.ToString(@"hh\:mm\:ss");
        CPH.ObsSetGdiText("[NS] Countdown Timer", "[TS] Countdown Timer", countdownString);
        countdownTimer.Stop();
        return true;
    }

    public bool Restart()
    {
        // Change countdownMinuteValue to initial length of stream in hours
        int defaultMinuteValue = CPH.GetGlobalVar<int>("defaultMinuteValue", true);
        countdownSecondsLeft = defaultMinuteValue * (60) + 1;
        countdownTimer.Start();
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
