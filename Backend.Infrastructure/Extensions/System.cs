namespace System;

public static partial class System
{
    public static TResult ConvertTo<TResult>(this object entity) where TResult : new()
    {
        //result props
        IEnumerable<PropertyDescriptor> convertProperties = TypeDescriptor.GetProperties(typeof(TResult)).Cast<PropertyDescriptor>();
        //source props
        IEnumerable<PropertyDescriptor> entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

        //source prop
        PropertyDescriptor? property = null;
        //result prop
        PropertyDescriptor? convertProperty = null;

        TResult convert = new TResult();
        bool nullable = false;

        foreach (PropertyDescriptor entityProperty in entityProperties)
        {
            property = entityProperty;
            convertProperty = convertProperties?.FirstOrDefault(prop => prop.Name == property.Name);

            nullable = property.PropertyType.IsNullableProperty();

            if (convertProperty != null && convertProperty.PropertyType == property.PropertyType && !nullable)
            {
                convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
            }
            else if (convertProperty != null && convertProperty.PropertyType == property.PropertyType && nullable)
            {
                convertProperty.SetValue(convert, entityProperty.GetValue(entity));
            }
        }

        return convert;
    }

    private static bool IsNullableProperty(this Type type) => type.Name.IndexOf("Nullable`", StringComparison.Ordinal) > -1;
}
