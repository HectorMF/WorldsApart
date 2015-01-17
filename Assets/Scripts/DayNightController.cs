using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayNightController : MonoBehaviour {
    static DayNightController _instance;

    public Camera camera;
    public float transitionTime = 1;
    public List<Color> colors;

    private bool switching;
    private float transitionTimer;
    private int index;

	void Start () {
        index = 0;
        transitionTimer = 0;
        camera.backgroundColor = colors[index];
	}

	void Update () {
        if(!switching) return;

        transitionTimer += Time.deltaTime/(transitionTime);

        if (transitionTimer >= 1)
        {
            switching = false;
        }
        else
        {
            camera.backgroundColor = Color.Lerp(colors[index - 1], colors[index], transitionTimer);
        }
	}

    public void NextSkyColor()
    {
        if (index + 1 >= colors.Count) return;
        switching = true;
        index++;
        transitionTimer = 0;
    }

    static public DayNightController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType(typeof(DayNightController)) as DayNightController;

                if (_instance == null)
                {
                    GameObject go = new GameObject("DayNightController");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<DayNightController>();
                }
            }
            return _instance;
        }
    }
}
