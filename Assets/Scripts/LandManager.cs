using UnityEngine;
using System.Collections;

public class LandManager : MonoBehaviour
{
    public GameObject StartOFTheEarth;
    public GameObject EndOfTheEarth;
	// Use this for initialization

    
    public Vector3 GetStart()
    {
        return StartOFTheEarth.transform.position;
    }
    public Vector3 GetEnd()
    {
        return EndOfTheEarth.transform.position;
    }

}
