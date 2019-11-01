using System.Collections.Generic;
using UnityEngine;
using mixpanel;

public class AnalyticsManager : SingletonBehaviour<AnalyticsManager>
{
    const string FirstLaunchPrefsKey = "FirstLaunchPrefsKey";
    const string AccountUIDPrefsKey = "AccountUIDPrefsKey";

    private string m_sAccountUID;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// This method should be used instead of MonoBehaviour.Awake()
    /// </summary>
    protected override void OnSingletonAwake()
    {
        DontDestroyOnLoad(this);

        // Then, you can track events with
        Mixpanel.Track("Analytics Initialized");
        Log("Analytics Initialized");
        if (IsFistLaunch())
        {
            TrackFirstLaunch();
        }
        Dictionary<string, string> dParameterNameConvertionTable = new Dictionary<string, string>();
        dParameterNameConvertionTable.Add("soft_currency_balance", "soft_currency_balance_int");
    }

    /// <summary>
    /// This method should be used instead of MonoBehaviour.OnDestroy()
    /// </summary>
    protected override void OnSingletonDestroy()
    {
        Log("OnDestroy");
    }


    void Log(string sEventName, Value pParams = null)
    {
        Debug.Log("EventName: " + sEventName);
    }

    public string GetAccountUID()
    {
        if (string.IsNullOrEmpty(m_sAccountUID))
        {
            if (!PlayerPrefs.HasKey(AccountUIDPrefsKey))
            {
                m_sAccountUID = System.Guid.NewGuid().ToString();
                PlayerPrefs.SetString(AccountUIDPrefsKey, m_sAccountUID);
            }
            else
            {
                m_sAccountUID = PlayerPrefs.GetString(AccountUIDPrefsKey);
            }
        }

        return m_sAccountUID;
    }

    bool IsFistLaunch()
    {
        if (!PlayerPrefs.HasKey(FirstLaunchPrefsKey))
        {
            PlayerPrefs.SetString(AccountUIDPrefsKey, GetAccountUID());
            PlayerPrefs.SetString(FirstLaunchPrefsKey, SystemInfo.deviceUniqueIdentifier);
            return true;
        }
        else
        {
            return false;
        }
    }

    void TrackFirstLaunch()
    {
        var pProperties = new Value();
        pProperties["AccountId"] = GetAccountUID();
        pProperties["DeviceId"] = SystemInfo.deviceUniqueIdentifier;
        pProperties["DeviceModel"] = SystemInfo.deviceModel;
        pProperties["DeviceName"] = SystemInfo.deviceName;

        Mixpanel.Identify(GetAccountUID());
        Mixpanel.Track("1stLaunch", pProperties);
        Log("1stLaunch", pProperties);
    }

    public void TrackGameStarted(int iChar)
    {
        var pProperties = new Value();
        pProperties["CharacterIndex"] = iChar;

        Mixpanel.Track("GameStarted", pProperties);
        Log("GameStarted", pProperties);
    }

    public void TrackGameEnd(int iChar)
    {
        var pProperties = new Value();
        pProperties["CharacterIndex"] = iChar;

        Mixpanel.Track("GameEnded", pProperties);
        Log("GameEnded", pProperties);
    }

    public void TrackPowerUp(string sPowerUpName)
    {
        var pProperties = new Value();
        pProperties["PowerUp"] = sPowerUpName;

        Mixpanel.Track("PowerUp_Collected", pProperties);
        Log("PowerUp_Collected", pProperties);
    }

    public void TrackToogleSound(string sSoundConfig, bool bStatus)
    {
        var pProperties = new Value();
        pProperties["Sound_Config"] = sSoundConfig;
        pProperties["Status"] = bStatus;

        Mixpanel.Track("SoundConfigChanged", pProperties);
        Mixpanel.People.Set(sSoundConfig + "Pref", bStatus);

        Log("SoundConfigChanged", pProperties);
    }

}
