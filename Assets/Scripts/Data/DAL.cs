using UnityEngine;
using System.Collections;
using Parse;


public class DAL 
{
    private DAL()
    {

    }
    private static DAL instance;
    public static DAL Instance
    {
        get
        {
            if(instance==null)
            {
                instance = new DAL();
            }
            return instance;
        }
    }
    bool paused;
    bool saving = false;
    public void Load()
    {
        Debug.Log("HAS PACK:" + PlayerPrefs.GetInt("HasPack"));
        bool hp =PlayerPrefs.GetInt("HasPack") == 0 ? false : true;
        ThirdWorldManager.Instance.LoadGame(PlayerPrefs.GetInt("DaysAlive"), PlayerPrefs.GetInt("Water"), PlayerPrefs.GetInt("Food"), PlayerPrefs.GetInt("Mood"), hp);
    }
    public void Save(bool cloudSave = false)
    {
        if(cloudSave)
        {                    

            Debug.Log("Saving...");
            ParseObject result = new ParseObject("SaveObject");
            var deviceId = SystemInfo.deviceUniqueIdentifier.ToString();
            var query = ParseObject.GetQuery("SaveObject").WhereEqualTo("DeviceID", deviceId).CountAsync().ContinueWith(t =>
                        {
                            int count = t.Result;
                            CloudSave(count, deviceId);
                        });
        }
        else
        {
            LocalSave();
        }
    }

    private void CloudSave(int numberOfExistingRecords, string deviceId)
    {
        if (!saving)
        {
            saving = true;
            //Saving a new version
            if (numberOfExistingRecords == 0)
            {
                ParseObject dataObject = new ParseObject("SaveObject");
                dataObject["DeviceID"] = deviceId;
                dataObject["DaysAlive"] = ThirdWorldManager.Instance.DaysAlive; //TODO: Get the number of days alive
                dataObject["HasPack"] = false; //TODO: Get if the player has Ph2o
                dataObject["Water"] = ThirdWorldManager.Instance.CurrentWater;
                dataObject["Food"] = ThirdWorldManager.Instance.CurrentFood;
                dataObject["Mood"] = (int)ThirdWorldManager.Instance.CurrentMood;

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
                //Updating
                Debug.Log("Updating an existing data row");
                var query = ParseObject.GetQuery("SaveObject")
                .WhereEqualTo("DeviceID", deviceId).FirstAsync().ContinueWith(t =>
                    {
                        var dataObject = t.Result;
                        dataObject.SaveAsync().ContinueWith(z =>
                        {
                            dataObject["DaysAlive"] = ThirdWorldManager.Instance.DaysAlive; //TODO: Get the number of days alive
                            dataObject["HasPack"] = false; //TODO: Get if the player has Ph2o
                            dataObject["Water"] = ThirdWorldManager.Instance.CurrentWater;
                            dataObject["Food"] = ThirdWorldManager.Instance.CurrentFood;
                            dataObject["Mood"] = (int)ThirdWorldManager.Instance.CurrentMood;
                            dataObject.SaveAsync();
                        });
                        saving = false;
                    });
            }
        }
    }

    private void LocalSave()
    {
        if(ThirdWorldManager.Instance.HasPack)
        {
            PlayerPrefs.SetInt("HasPack", 1);
        }
        else
        {
            PlayerPrefs.SetInt("HasPack", 0);
        }
        PlayerPrefs.SetInt("DaysAlive", ThirdWorldManager.Instance.DaysAlive);
        PlayerPrefs.SetInt("Water", ThirdWorldManager.Instance.CurrentWater);
        PlayerPrefs.SetInt("Food", ThirdWorldManager.Instance.CurrentFood);
        PlayerPrefs.SetInt("Mood", (int)ThirdWorldManager.Instance.CurrentMood);
        PlayerPrefs.Save();

    }
    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}