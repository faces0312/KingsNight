using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class GoogleLogIn : MonoBehaviour
{

    private void Awake()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }
    public void LogIn()
    {
        Social.ReportScore(Data.Instance.gameData.hightScore, GPGSIds.leaderboard_highscore, (bool success) => { });
        

        Social.localUser.Authenticate((bool success) =>
        {
        });
    }

    public void ShowLearderboardUI() => Social.ShowLeaderboardUI();

    /*
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestIdToken().RequestServerAuthCode(false).Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        SignInGooglePlayGames();
    }

    void SignInGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
        });
    }
*/
}
