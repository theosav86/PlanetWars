using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    #region Variables
    public TextMeshProUGUI greenScoreText;
   
    public TextMeshProUGUI redScoreText;

    public TextMeshProUGUI leadingTeamText;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        #region Event Subscriptions
        //Subscribing to the event Green Cube Spawned.
        ScoreBroker.UpdateGreenCubeScore += ScoreBroker_UpdateGreenCubeScore;

        //Subscribing to the event Red Cube Spawned.
        ScoreBroker.UpdateRedCubeScore += ScoreBroker_UpdateRedCubeScore;

        //Subscribing to the event Game Over
        ScoreBroker.TeamIsWinning += ScoreBroker_TeamIsWinning;
        #endregion
    }

    //Updates winning text on HUD if 1 Team is Winning the War
    private void ScoreBroker_TeamIsWinning(string winningTeam)
    {
        leadingTeamText.text = "Team Currently Winning: " + winningTeam;

        if(winningTeam.Equals("GREEN"))
        {
            leadingTeamText.color = Color.green;
        }
        else
        {
            leadingTeamText.color = Color.red;
        }
    }

    //Updates the Green Score on the HUD
    private void ScoreBroker_UpdateGreenCubeScore(int greenScoreValue)
    {
        greenScoreText.text = "Green Score: " + greenScoreValue.ToString("D2");
    }

    //Updates the Red Score on the HUD
    private void ScoreBroker_UpdateRedCubeScore(int redScoreValue)
    {
        redScoreText.text = "Red Score: " + redScoreValue.ToString("D2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
