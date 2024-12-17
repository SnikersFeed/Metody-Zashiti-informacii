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

        // Получаем информацию о процессоре (архитектура)
        systemInfo.AppendLine("ЦП: " + SystemInfo.processorType);
        systemInfo.AppendLine("Частота ЦП: " + SystemInfo.processorFrequency.ToString());
        systemInfo.AppendLine("Количество ядер: " + SystemInfo.processorCount.ToString());

        // Получаем информацию о графике
        systemInfo.AppendLine(GetGraphicsDeviceInfo());
        // Получаем информацию о памяти
        systemInfo.AppendLine("Оперативная память: " + SystemInfo.systemMemorySize.ToString() + "МБ");

        // Получаем имя пользователя
        systemInfo.AppendLine("Имя пользователя: " + Environment.UserName);

        // Получаем имя ОС
        systemInfo.AppendLine("ОС: " + SystemInfo.operatingSystem);
        systemInfo.AppendLine("Уникальный номер: " + SystemInfo.deviceUniqueIdentifier);

        // Можно добавить и другие параметры (например, ID девайса)

        return systemInfo.ToString();
    }


    //Объединяем данные об видеокарте в одну строку
    private static string GetGraphicsDeviceInfo()
    {
        StringBuilder graphicsInfo = new StringBuilder();
        graphicsInfo.AppendLine("ГП: " + SystemInfo.graphicsDeviceName);
        graphicsInfo.AppendLine("Производитель ГП: " + SystemInfo.graphicsDeviceVendor);
        graphicsInfo.AppendLine("Память ГП: " + SystemInfo.graphicsMemorySize.ToString() + "МБ");
        return graphicsInfo.ToString();
    }
}