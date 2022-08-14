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

    public bool StartTimer()
    {
        double countdownMinutesToAdd = Convert.ToDouble(args["rawInput"]);
        CPH.SetGlobalVar("defaultMinuteValue", countdownMinutesToAdd, true);
        countdownSecondsLeft = Convert.ToInt32(Math.Floor((countdownMinutesToAdd * 60) + 1));
        countdownTimer.Start();
        return true;
    }

    public void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        countdownSecondsLeft--;
        TimeSpan time = TimeSpan.FromSeconds(countdownSecondsLeft);
        //string countdownString = time.ToString(@"hh\:mm\:ss");
        string countdownString = string.Format("{0}:{1}", (int)time.TotalMinutes, time.ToString("ss"));
        if (countdownSecondsLeft == 0)
        {
            StopTimer("Time's up!");
            CPH.RunAction("Timer Done");
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

    private void AddMinutes(double countdownMinutesToAdd)
    {
        int countdownSecondsToAdd = Convert.ToInt32(Math.Floor(countdownMinutesToAdd * 60));
        countdownSecondsLeft = countdownSecondsLeft + countdownSecondsToAdd;
    }

    public bool Stop()
    {
        StopTimer("Timer Cancelled!");
        return true;
    }

    public bool Set()
    {
        double countdownMinutesToAdd = Convert.ToDouble(args["rawInput"]);
        CPH.SetGlobalVar("defaultMinuteValue", countdownMinutesToAdd, true);
        countdownSecondsLeft = Convert.ToInt32(Math.Floor(countdownMinutesToAdd * 60));
        TimeSpan time = TimeSpan.FromSeconds(countdownSecondsLeft);
        //string countdownString = time.ToString(@"hh\:mm\:ss");
        string countdownString = string.Format("{0}:{1}", (int)time.TotalMinutes, time.ToString("ss"));
        CPH.ObsSetGdiText("[NS] Countdown Timer", "[TS] Countdown Timer", countdownString);
        countdownTimer.Stop();
        return true;
    }

    public bool Restart()
    {
        // Change countdownMinuteValue to initial length of stream in hours
        double defaultMinuteValue = CPH.GetGlobalVar<double>("defaultMinuteValue", true);
        countdownSecondsLeft = Convert.ToInt32(Math.Floor((defaultMinuteValue * 60) + 1));
        countdownTimer.Start();
        return true;
    }

    public bool AddX()
    {
        double countdownMinuteValue = Convert.ToDouble(args["rawInput"]);
        AddMinutes(countdownMinuteValue);
        return true;
    }
}
