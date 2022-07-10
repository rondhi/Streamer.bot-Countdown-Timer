//In the references tab, right click under mscorlib.dll and "Add reference from file..."
//Add System.dll as a reference
using System;
using System.Timers;

class CPHInline
{
    public System.Timers.Timer countdownTimer;
    public int subathonSecondsLeft;
    public int subathontotalTimeInSeconds;
    public int subathonCapInSeconds;
    public void Init()
    {
        countdownTimer = new System.Timers.Timer(1000);
        countdownTimer.Elapsed += OnTimedEvent;
        countdownTimer.AutoReset = true;
        countdownTimer.Enabled = true;
        countdownTimer.Stop();
    }

    public bool StartSubathon()
    {
        int maxHourValue = Convert.ToInt32(args["maxHourValue"]);
        subathonCapInSeconds = maxHourValue * (3600);
        int hourValue = Convert.ToInt32(args["hourValue"]);
        subathonSecondsLeft = hourValue * (3600) + 1;
        subathontotalTimeInSeconds = subathonSecondsLeft;
        countdownTimer.Start();
        return true;
    }

    public void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        subathonSecondsLeft--;
        TimeSpan time = TimeSpan.FromSeconds(subathonSecondsLeft);
        string countdownString = time.ToString(@"hh\:mm\:ss");
        if (subathonSecondsLeft == 0)
        {
            StopSubathon("Subathon Complete!");
            CPH.RunAction("Subathon Done Action");
        }
        else
        {
            string subathonScene = CPH.GetGlobalVar<string>("subathonScene", true);
            string subathonSource = CPH.GetGlobalVar<string>("subathonSource", true);
            CPH.ObsSetGdiText(subathonScene, subathonSource, countdownString);
        }
    }

    public void Dispose()
    {
        countdownTimer.Dispose();
    }

    private void StopSubathon(string message)
    {
        // Set to Scene and Source of your text source
        string subathonScene = CPH.GetGlobalVar<string>("subathonScene", true);
        string subathonSource = CPH.GetGlobalVar<string>("subathonSource", true);
        CPH.ObsSetGdiText(subathonScene, subathonSource, message);
        countdownTimer.Stop();
    }

    private void AddMinutes(int minutesToAdd)
    {
        int secondsToAdd = minutesToAdd * 60;
        if ((subathontotalTimeInSeconds + secondsToAdd) < subathonCapInSeconds)
        {
            subathontotalTimeInSeconds = subathontotalTimeInSeconds + secondsToAdd;
            subathonSecondsLeft = subathonSecondsLeft + secondsToAdd;
            if (minutesToAdd == 1)
            {
                string message = minutesToAdd + " minute has been added to the Subathon";
                CPH.SendMessage(message, true);
            }
            else
            {
                string message = minutesToAdd + " minutes has been added to the Subathon";
                CPH.SendMessage(message, true);
            }
        }
        else
        {
            subathonSecondsLeft = subathonSecondsLeft + (subathonCapInSeconds - subathontotalTimeInSeconds);
            subathontotalTimeInSeconds = subathonCapInSeconds;
            CPH.SendMessage("We've reached the sub-a-thon limit! No more time will be added.", true);
        }
    }

    public bool Stop()
    {
        StopSubathon("Sub-a-thon cancelled!");
        return true;
    }

    public bool AddTime()
    {
        int minuteValue = Convert.ToInt32(args["minutesToAdd"]);
        AddMinutes(minuteValue);
        return true;
    }

    public bool Cheers()
    {
        double bitsGiven = Convert.ToDouble(args["bits"]);
        int bitsDivide = Convert.ToInt32(args["bitsDivide"]);
        int bitsHundred = Convert.ToInt32(Math.Floor(bitsGiven / bitsDivide));
        AddMinutes(bitsHundred);
        return true;
    }
}
