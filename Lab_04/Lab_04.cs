using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;

public class Lab_04 : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text encryptedText;
    public TMP_Text decryptedText;

    [Space]
    public TMP_Text publicKeyText;
    public TMP_Text privateKeyText;

    private RSAParameters publicKey;
    private RSAParameters privateKey;

    void Start()
    {
        GenerateKeys();
        DisplayKeys();
    }

    private void GenerateKeys()
    {
        using (RSA rsa = RSA.Create())
        {
            publicKey = rsa.ExportParameters(false);
            privateKey = rsa.ExportParameters(true);
        }
    }

    private void DisplayKeys()
    {
        publicKeyText.text = "Публичный ключ: " + Convert.ToBase64String(publicKey.Modulus);
        privateKeyText.text = "Приватный ключ: " + Convert.ToBase64String(privateKey.D);
    }

    public void EncryptText()
    {
        string plainText = inputField.text;
        byte[] encryptedData;

        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(publicKey);
            encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.Pkcs1);
        }

        encryptedText.text =  Convert.ToBase64String(encryptedData);
    }

    public void DecryptText()
    {
        string encryptedBase64 = encryptedText.text;
        byte[] decryptedData;

        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(privateKey);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
            decryptedData = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
        }

        decryptedText.text = "Расшифрованный текст: " + Encoding.UTF8.GetString(decryptedData);
    }
}
