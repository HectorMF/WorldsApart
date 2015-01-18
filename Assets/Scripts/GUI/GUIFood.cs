using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WorldsApart.GUI
{
    public class GUIFood : MonoBehaviour
    {

        private Text _food;
        // Use this for initialization
        void Start()
        {
            _food = this.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            var foodtext = string.Format("{0}/{1}", ThirdWorldManager.Instance.CurrentFood, ThirdWorldManager.Instance.RequiredFood);
            _food.text = foodtext;
        }
    }
}