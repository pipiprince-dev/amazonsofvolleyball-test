namespace Backend.Middleware.Binders;

public class JsonModelBinder : IModelBinder
{
    private IJsonAttribute attribute;
    private Type targetType;

    public JsonModelBinder(Type type, IJsonAttribute attribute)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        this.attribute = attribute as IJsonAttribute;
        targetType = type;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // Check the value sent in
        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult != ValueProviderResult.None)
        {
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            // Attempt to convert the input value
            string valueAsString = valueProviderResult.FirstValue;

            object result = attribute.TryConvert(valueAsString, targetType, out bool success);

            if (success)
            {
                bindingContext.Result = ModelBindingResult.Success(result);
                return Task.CompletedTask;
            }
        }

        return Task.CompletedTask;
    }
}
