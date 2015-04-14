using UnityEngine;
using System.Collections;
using Parse;

public class TestSave : MonoBehaviour {

    bool paused;
	// Use this for initialization
	void Start () {
        Save();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("p"))
        {
            OnApplicationPause(true);
            //OnApplicationFocus(true);
        }
	}
    void OnApplicationPause(bool pauseStatus)
    {
        paused = pauseStatus;
      //  Save();
        
    }
    void OnApplicationFocus(bool focusStatus)
    {
    }
    private bool Save()
    {
        Debug.Log("Saving...");
        ParseObject SaveObject = new ParseObject("SaveObject");
        SaveObject["Device ID"] = SystemInfo.deviceUniqueIdentifier;
        SaveObject["Days Alive"] = 4; //TODO: Get the number of days alive
        SaveObject["Has Pack"] = false; //TODO: Get if the player has Ph2o
        SaveObject["Water"] = ThirdWorldManager.Instance.CurrentWater;
        SaveObject["Food"] = ThirdWorldManager.Instance.CurrentFood;
        SaveObject["Mood"] = (int)ThirdWorldManager.Instance.CurrentMood;
        var save = SaveObject.SaveAsync();
        if(save.IsCompleted)
        {
            Debug.Log("Save Completed!");
        }
        else
        {
            if(save.IsCanceled)
            {
                Debug.Log("Saving is canceled");
            }
            if(save.IsFaulted)
            {
                Debug.Log("Saving is crapped out");
            }
            Debug.Log("SAVE STATUS:" + save.IsCompleted);
        }
        return save.IsCompleted;
    }
}
