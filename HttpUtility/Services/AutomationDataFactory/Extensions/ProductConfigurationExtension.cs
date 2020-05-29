using HttpUtility.Services.AutomationDataFactory.Models.Merchandise;

namespace HttpUtility.Services.AutomationDataFactory.Extensions
{
    static public class ProductConfigurationExtension
    {
        static public object GetProductRequest(this TestProductConfiguration productConfiguration)
        {
            var result = new { };

            if (productConfiguration.Prop65Message.HasValue)
            {
                return new
                {
                    productConfiguration.Prop65Message
                };
            }

            return result;
        }
    }
}
