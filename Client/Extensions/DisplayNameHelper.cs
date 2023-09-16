using System;
using System.ComponentModel;
using System.Reflection;

public static class DisplayNameHelper
{
    public static string GetDisplayName<T>(T obj, string propertyName)
    {
        Type type = obj.GetType();
        PropertyInfo propertyInfo = type.GetProperty(propertyName);

        if (propertyInfo != null)
        {
            DisplayNameAttribute displayNameAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();

            if (displayNameAttribute != null)
            {
                return displayNameAttribute.DisplayName;
            }
        }

        return propertyName; // Fallback to the property name if no display name is found.
    }
}
