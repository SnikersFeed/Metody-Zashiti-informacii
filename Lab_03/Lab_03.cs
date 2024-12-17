using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lab_03 : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private TMP_InputField cryptKey;

    [SerializeField] private TMP_Text cryptoText;
    [SerializeField] private TMP_InputField decryptKey;
    
    [SerializeField] private TMP_Text decryptoText;

    public void EncryptButton()
    {
        string plainText = inputText.text;
        string key = cryptKey.text;

        if (key.Length != 8)
        {
            Debug.LogError("���� ������ ��������� ����� 8 ��������.");
            return;
        }
        else if (key.Length == 0)
        {
            Debug.LogError("������� ���� ����������.");
            return;
        }

        string encryptedText = Encrypt(plainText, key);
        cryptoText.text = encryptedText;
    }

    public void DecryptButton()
    {
        string encryptedText = cryptoText.text;
        string key = decryptKey.text;

        if (key.Length != 8)
        {
            Debug.LogError("���� ������ ��������� ����� 8 ��������.");
            return;
        }
        else if (key.Length == 0)
        {
            Debug.LogError("������� ���� ������������.");
            return;
        }

        string decryptedText = Decrypt(encryptedText, key);
        decryptoText.text = decryptedText;
    }

    private string Encrypt(string plainText, string key)
    {
        if (key.Length != 8)
            throw new ArgumentException("���� ������ ��������� ����� 8 ��������.");

        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            des.Key = Encoding.UTF8.GetBytes(key);
            des.GenerateIV(); // ��������� ���������� IV
            byte[] iv = des.IV;

            ICryptoTransform encryptor = des.CreateEncryptor(des.Key, iv);
            using (MemoryStream ms = new MemoryStream())
            {
                // ������� ���������� IV � �����
                ms.Write(iv, 0, iv.Length);

                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private string Decrypt(string encryptedText, string key)
    {
        if (key.Length != 8)
            throw new ArgumentException("���� ������ ��������� ����� 8 ��������.");

        byte[] fullCipher = Convert.FromBase64String(encryptedText);

        // ��������� IV �� ������ 8 ����
        byte[] iv = new byte[8];
        Array.Copy(fullCipher, 0, iv, 0, iv.Length);

        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = iv; // ������������� IV

            ICryptoTransform decryptor = des.CreateDecryptor(des.Key, des.IV);
            using (MemoryStream ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
