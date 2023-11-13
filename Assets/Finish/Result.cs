using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;


public class Result : MonoBehaviour
{
    public LvManager lvManager;

    public TextMeshProUGUI mm;
    public TextMeshProUGUI ss;
    public TextMeshProUGUI score;
    public TextMeshProUGUI result;
    public TextMeshProUGUI timeMoney;
    public TextMeshProUGUI scoreMoney;
    public TextMeshProUGUI resultMoney;

    public void ResultPage()
    {
        mm.text = Data.Instance.gameData.playTimeLv.ToString();
        ss.text = ": " + ((int)lvManager.playTime).ToString();
        score.text = Data.Instance.gameData.cutcount.ToString();
        result.text = ((Data.Instance.gameData.playTimeLv * 6) + (((int)lvManager.playTime) / 10) + (Data.Instance.gameData.cutcount)).ToString();
        timeMoney.text = ((Data.Instance.gameData.playTimeLv * 6) + (((int)lvManager.playTime) / 10)).ToString();
        scoreMoney.text = Data.Instance.gameData.cutcount.ToString();
        resultMoney.text = ((Data.Instance.gameData.playTimeLv * 6) + (((int)lvManager.playTime) / 10) + (Data.Instance.gameData.cutcount)).ToString();
    }

    public void GoMain()
    {
        Data.Instance.gameData.playCount++;
        Data.Instance.gameData.money += (Data.Instance.gameData.playTimeLv * 6) + (((int)lvManager.playTime) / 10) + (Data.Instance.gameData.cutcount);
        if(Data.Instance.gameData.hightScore < (Data.Instance.gameData.playTimeLv * 6) + (((int)lvManager.playTime) / 10) + (Data.Instance.gameData.cutcount))
        {
            Data.Instance.gameData.hightScore = (Data.Instance.gameData.playTimeLv * 6) + (((int)lvManager.playTime) / 10) + (Data.Instance.gameData.cutcount);
            Data.Instance.SaveGameData();
        }
        SceneManager.LoadScene("Main");
    }

}
