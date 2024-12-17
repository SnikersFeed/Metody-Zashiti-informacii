using System;
using System.Security.Cryptography;
using UnityEngine;
using System.Text;
using System.IO;
using TMPro;
using UnityEngine.Device;

public class LicenseManager : MonoBehaviour
{
    public TMP_Text PCInfoText;
    public GameObject InvalidLicensePanel;
    public GameObject ValidLicensePanel;

    private const string hash = "c390db97a24242e7906482947f7be34cee3c3e74082ab6be49084a74af3bd7d9";
    private const string KeyFileName = "license.key";
    private const string ErrorMessage = "This application is not licensed for use on this system.";

    private void Start()
    {
        Debug.Log(UnityEngine.Application.persistentDataPath);
        string systemInfo = SystemInfoUtil.GetSystemInfoHash();
        PCInfoText.text = systemInfo;
    }

    public void CheckLicense()
    {
        string systemInfo = SystemInfoUtil.GetSystemInfoHash();
        string expectedKey = GenerateHash(systemInfo);

        //string savedKey = GetSavedKey();
        if (!expectedKey.Equals(hash))
        {
            InvalidLicensePanel.SetActive(true);
            Debug.LogError(ErrorMessage);
        }
        else
        {
            ValidLicensePanel.SetActive(true);
            Debug.Log("License is valid!");
        }
    }

    private string GenerateHash(string data)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }


    private string GetSavedKey()
    {
        // Для демонстрации используем простой файловый ввод/вывод, в реальных проектах лучше использовать реестр или другой безопасный способ
        string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, KeyFileName);
        if (File.Exists(filePath))
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading key file: {e.Message}");
                return null;
            }
        }
        else
        {
            string systemInfo = SystemInfoUtil.GetSystemInfoHash();
            string newKey = GenerateHash(systemInfo);
            SaveKey(newKey);
            Debug.LogWarning("No license key was found, new key has been generated.");
            return newKey;
        }
    }

    public void SaveKey(string key)
    {
        // Для демонстрации используем простой файловый ввод/вывод, в реальных проектах лучше использовать реестр или другой безопасный способ
        string filePath = Path.Combine(UnityEngine.Application.persistentDataPath, KeyFileName);
        try
        {
            File.WriteAllText(filePath, key);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving key to file: {e.Message}");
        }
    }

}