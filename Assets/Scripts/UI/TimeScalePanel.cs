using UnityEngine;

public class TimeScalePanel : MonoBehaviour
{
    public void TimeStop()
    {
        Time.timeScale = 0;
    }

    public void TimeSet(int multiplier)
    {
        if (multiplier >= 0)
        {
            Time.timeScale = multiplier;
        }
    }
}
