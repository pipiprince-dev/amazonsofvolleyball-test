namespace Backend.Validation.Interceptors
{
    public class FluentvalidationInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                JsonObject errorObject = new JsonObject();
                int i = 1;

                foreach (ValidationFailure error in result.Errors)
                {
                    errorObject.Add($"{i} - {error.PropertyName}\n", error.ErrorMessage);
                    i++;
                }

                string errorMessage = errorObject.ToString().Replace(@"\n", "");

                throw new Exception(errorMessage);
            }

            return result;
        }

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}
