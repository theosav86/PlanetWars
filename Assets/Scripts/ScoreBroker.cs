using System;

public class ScoreBroker 
{
    //Events that update the Green and Red Score on the HUD.
    public static event Action<int> UpdateGreenCubeScore;

    public static event Action<int> UpdateRedCubeScore;

    //Events that get invoked when a cube dies.
    public static event Action GreenCubeKilled;

    public static event Action RedCubeKilled;

    //Event that gets invoked when a team of cubes has no opponents on the field.
    public static event Action<string> TeamIsWinning;

    public static void CallUpdateGreenCubeScore(int greenScoreModifier)
    {
        if(UpdateGreenCubeScore != null)
        {
            UpdateGreenCubeScore(greenScoreModifier);
        }
    }

    public static void CallUpdateRedCubeScore(int redScoreModifier)
    {
        if(UpdateRedCubeScore != null)
        {
            UpdateRedCubeScore(redScoreModifier);
        }
    }

    public static void CallGreenCubeKilled()
    {
        if (GreenCubeKilled != null)
        {
            GreenCubeKilled();
        }
    }

    public static void CallRedCubeKilled()
    {
        if (RedCubeKilled != null)
        {
            RedCubeKilled();
        }
    }

    public static void CallTeamIsWinning(string winner)
    {
        if(TeamIsWinning != null)
        {
            TeamIsWinning(winner);
        }
    }

}
