using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CreditCardMemory : MonoBehaviour {
    public string LastFourDigits = "4212";

	public bool CheckLast4Digits(string digits)
    {
        if (digits == null)
            return false;
        Debug.Log(digits.ToString());
        if(digits.Length!=4)
        {
            return false;    
        }
        if(digits==LastFourDigits)
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
            if(CheckLast4Digits(digits))
            {
                //TODO:Stuff Happens
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        
    }
}
