using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour
{
    public bool isTestMode;
    public Button rewardAdsButton;
    public GameObject rewardLoading;

    
    // Start is called before the first frame update
    void Start()
    {
        LoadRewardAd();
    }

    // Update is called once per frame
    void Update()
    {
        rewardAdsButton.interactable = rewardAd.IsLoaded();
        if (rewardAdsButton.interactable == false)
            rewardLoading.gameObject.SetActive(true);
        else
            rewardLoading.gameObject.SetActive(false);
    }

    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-7537224848353526/9619445937";
    RewardedAd rewardAd;

    void LoadRewardAd()
    {
        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            Data.Instance.gameData.money += 100;
        };
    }
    public void ShowRewardAd()
    {
        rewardAd.Show();
        LoadRewardAd();
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }


}
