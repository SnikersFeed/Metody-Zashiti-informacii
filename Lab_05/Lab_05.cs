using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lab_05 : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text hashText;

    public void OnComputeHash()
    {
        string input = inputField.text;
        string hash = MD5Hasher.ComputeMD5Hash(input);
        hashText.text = "MD5 Hash: " + hash;
    }
}
