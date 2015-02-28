using UnityEngine;
using System.Collections;

public class GradientColors : MonoBehaviour {
    public Color top = Color.red;
    public Color bottom = Color.blue;
    public bool shouldUpdate = true;

    void Update()
    {
        if (shouldUpdate)
        {
            UpdateColors();
            shouldUpdate = false;
        }
    }

    public void UpdateColors()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        var colors = new Color[mesh.vertices.Length];
        colors[0] = bottom;
        colors[1] = top;
        colors[2] = bottom;
        colors[3] = top;

        mesh.colors = colors;
    }
}
