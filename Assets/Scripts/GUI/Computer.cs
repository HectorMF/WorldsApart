using UnityEngine;
using System.Collections;

namespace WorldsApart.GUI
{
    public class Computer : MonoBehaviour
    {

        public void ShutDown()
        {
            var start = Time.time;
            Debug.Log("Shut Down");
            Debug.Log(Time.time);
            Debug.Log(start);

            this.gameObject.SetActive(false);

        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}