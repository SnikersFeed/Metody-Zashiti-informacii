using System;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using System.Runtime.InteropServices;

public class SystemInfoUtil
{
    public static string GetSystemInfoHash()
    {
        StringBuilder systemInfo = new StringBuilder();

        // �������� ���������� � ���������� (�����������)
        systemInfo.AppendLine("��: " + SystemInfo.processorType);
        systemInfo.AppendLine("������� ��: " + SystemInfo.processorFrequency.ToString());
        systemInfo.AppendLine("���������� ����: " + SystemInfo.processorCount.ToString());

        // �������� ���������� � �������
        systemInfo.AppendLine(GetGraphicsDeviceInfo());
        // �������� ���������� � ������
        systemInfo.AppendLine("����������� ������: " + SystemInfo.systemMemorySize.ToString() + "��");

        // �������� ��� ������������
        systemInfo.AppendLine("��� ������������: " + Environment.UserName);

        // �������� ��� ��
        systemInfo.AppendLine("��: " + SystemInfo.operatingSystem);
        systemInfo.AppendLine("���������� �����: " + SystemInfo.deviceUniqueIdentifier);

        // ����� �������� � ������ ��������� (��������, ID �������)

        return systemInfo.ToString();
    }


    //���������� ������ �� ���������� � ���� ������
    private static string GetGraphicsDeviceInfo()
    {
        StringBuilder graphicsInfo = new StringBuilder();
        graphicsInfo.AppendLine("��: " + SystemInfo.graphicsDeviceName);
        graphicsInfo.AppendLine("������������� ��: " + SystemInfo.graphicsDeviceVendor);
        graphicsInfo.AppendLine("������ ��: " + SystemInfo.graphicsMemorySize.ToString() + "��");
        return graphicsInfo.ToString();
    }
}