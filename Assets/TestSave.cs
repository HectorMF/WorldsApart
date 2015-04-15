using UnityEngine;
using System.Collections;
using Parse;


public class TestSave : MonoBehaviour {

    bool paused;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            Save();
            //OnApplicationFocus(true);
        }
	}
    void OnApplicationPause(bool pauseStatus)
    {
        paused = pauseStatus;
        
        
    }
    void OnApplicationFocus(bool focusStatus)
    {
    }
    private void Save()
    {
        Debug.Log("Saving...");
        ParseObject result = new ParseObject("SaveObject");
        var deviceId = SystemInfo.deviceUniqueIdentifier.ToString();

        var query = ParseObject.GetQuery("SaveObject")
            .WhereEqualTo("DeviceID", deviceId).CountAsync().ContinueWith(t =>
                { int count = t.Result;
                Debug.Log("Count:" + count);
                Save(count, deviceId);
                });
            

    }
    
    public void Save(int numberOfExistingRecords, string deviceId)
    {
        ParseObject dataObject = new ParseObject("SaveObject");
        dataObject["DeviceID"] = deviceId;
        dataObject["DaysAlive"] = 4; //TODO: Get the number of days alive
        dataObject["HasPack"] = false; //TODO: Get if the player has Ph2o
        dataObject["Water"] = ThirdWorldManager.Instance.CurrentWater;
        dataObject["Food"] = ThirdWorldManager.Instance.CurrentFood;
        dataObject["Mood"] = (int)ThirdWorldManager.Instance.CurrentMood;
        if (numberOfExistingRecords == 0)
        {
            var save = dataObject.SaveAsync();
            bool finishSaved = false;
            int tries = 0;
            Debug.Log("Saving..");
            if (save.IsCompleted)
            {
                Debug.Log("Save Completed!");
            }
            while (!finishSaved && tries < 4)
            {
                Debug.Log("...");
                if (save.IsCanceled)
                {
                    Debug.Log("Saving is canceled");
                    finishSaved = true;
                }
                if (save.IsFaulted)
                {
                    Debug.Log("Saving is crapped out");
                    finishSaved = true;
                }
                if (save.IsCompleted)
                {
                    Debug.Log("Save Completed!");
                }
                tries++;
            }
        }
        else
        {
            dataObject.SaveAsync().ContinueWith(t =>
                {
                    dataObject["DaysAlive"] = 4; //TODO: Get the number of days alive
                    dataObject["HasPack"] = false; //TODO: Get if the player has Ph2o
                    dataObject["Water"] = ThirdWorldManager.Instance.CurrentWater;
                    dataObject["Food"] = ThirdWorldManager.Instance.CurrentFood;
                    dataObject["Mood"] = (int)ThirdWorldManager.Instance.CurrentMood;
                    dataObject.SaveAsync();
                });
        }
    }
}
