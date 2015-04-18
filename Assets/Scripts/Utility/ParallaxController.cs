using UnityEngine;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour {
    static ParallaxController _instance;

    private List<ParallaxLayer> layers = new List<ParallaxLayer>();
	private Vector3 oldPosition;

    static public ParallaxController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType(typeof(ParallaxController)) as ParallaxController;

                if (_instance == null)
                {
                    GameObject go = new GameObject("ParallaxController");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<ParallaxController>();
                }
            }
            return _instance;
        }
    }

	void LateUpdate () {
		for(int i = layers.Count - 1; i >= 0; i--)
		{
			if(layers[i] == null)
				layers.RemoveAt(i);
		}

        foreach (ParallaxLayer layer in layers)
            layer.UpdateOffsets(transform.position.x, transform.position.y);

	}

    public void AddLayer(ParallaxLayer layer)
    {
        layers.Add(layer);
    }
}
