using UnityEngine;
using System.Collections;

public class SaveAndLoad : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        DAL.Instance.Save(cloudSave: false);
    }
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            DAL.Instance.Save();
            //OnApplicationFocus(true);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            DAL.Instance.DeleteAllSaves();
        }
    }
}
