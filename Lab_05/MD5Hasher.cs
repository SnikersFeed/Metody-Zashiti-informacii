using System;
using System.Security.Cryptography;
using System.Text;

public class MD5Hasher
{
    public static string ComputeMD5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // ����������� ����� � ������ � ����������������� �������
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2")); // "x2" ������������ �������������� � ������ ��������
            }
            return sb.ToString();
        }
    }
}