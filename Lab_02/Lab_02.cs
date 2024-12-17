using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Lab_02 : MonoBehaviour
{
    [SerializeField] private int key = 3;
    private const int russianLenght = 33;
    private const int englishLenght = 26;
    private const string pun�tuaction = ".,!?:;'()";

    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private TMP_InputField inputKey;
    [SerializeField] private TMP_Text cryptoText;
    [SerializeField] private TMP_Text uncryptoText;

    public void Crypto()
    {
        string result = "";
        key = Int32.Parse(inputKey.text);
        result = Shift(inputText.text, key);
        cryptoText.text = result;
    }

    public void Uncrypto()
    {
        string result = "";
        key = Int32.Parse(inputKey.text);
        result = Shift(cryptoText.text, -key);
        uncryptoText.text = result;
    }

    private string Shift(string text, int key)
    {
        char[] buffer = text.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];

            if (letter >= '�' && letter <= '�')
            {
                letter = (char)(letter + key);

                if (letter > '�')
                    letter = (char)(letter - russianLenght);
                else if (letter < '�')
                    letter = (char)(letter + russianLenght);
                buffer[i] = letter;
            }
            else if (letter >= 'a' && letter <= 'z')
            {
                letter = (char)(letter + key);

                if (letter > 'z')
                    letter = (char)(letter - englishLenght);
                else if (letter < 'a')
                    letter = (char)(letter + englishLenght);
                buffer[i] = letter;
            }
            else if (pun�tuaction.Contains(letter))
            {
                int punctuationIndex = pun�tuaction.IndexOf(letter);
                punctuationIndex = (punctuationIndex + key) % pun�tuaction.Length;

                if (punctuationIndex < 0)
                    punctuationIndex += pun�tuaction.Length;

                buffer[i] = pun�tuaction[punctuationIndex];
            }
            else
            {
                buffer[i] = letter;
            }
        }
        return new string(buffer);
    }
}
