using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace WorldsApart.GUI
{
    public class CreditCardMemory : MonoBehaviour
    {
        public string LastFourDigits = "4212";
        public GameObject computer;
        public bool CheckLast4Digits(string digits)
        {
            if (digits == null)
                return false;
            Debug.Log(digits.ToString());
            if (digits.Length != 4)
            {
                return false;
            }
            if (digits == LastFourDigits)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DoStuffBasedOnInput()
        {
            try
            {
                string digits = this.GetComponent<InputField>().text;
                if (CheckLast4Digits(digits))
                {
                    PopOutMessage(1);
                }
                else
                {
                    PopOutMessage(0);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

        }
        /// <summary>
        /// Type = 1 Correct; type = 0 not correct
        /// </summary>
        /// <param name="type"></param>
        private void PopOutMessage(int type)
        {
            //Remove the input
            var input = this.GetComponent<InputField>();
            input.DeactivateInputField();

            ////Replace it by the message
            //var msg = new GameObject("Message");
            //Instantiate(msg);
            //var text = msg.AddComponent<Text>();
            //text.transform.position = input.transform.position;
            if (type == 1)
            {
                input.textComponent.CalculateLayoutInputHorizontal();
                input.text = "Correct Answer, Congratulation you";
                input.text += " just helped someone with PackH2O";

                ThirdWorldManager.Instance.HasPack = true;

                try
                {
                    computer.GetComponent<Computer>().ShutDown();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            else
            {
                input.text = "Wrong information. Please try again";
                input.ActivateInputField();
            }
        }
    }
}
