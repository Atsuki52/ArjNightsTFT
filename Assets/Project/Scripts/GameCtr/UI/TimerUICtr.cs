using UnityEngine;
using UnityEngine.UI;

public class TimerUICtr : MonoBehaviour
{
    [SerializeField]
    private Text timerUItext;

    [SerializeField]
    private GameTimeCtr gameTimeCtr;

    // Update is called once per frame
    void FixedUpdate()
    {
        int minutes = (int)(gameTimeCtr.GameTime / 60);
        int seconds = (int)(gameTimeCtr.GameTime % 60);
        int milliseconds = (int)((gameTimeCtr.GameTime * 100) % 100);

        timerUItext.text = $"{minutes:00}:{seconds:00}<size=60%>.{milliseconds:00}</size>";
    }
}
