namespace HttpUtility.Services.AutomationDataFactory.Models.Merchandise
{
    public class TestProductConfiguration
    {
        public PatcheableProp<string> OfferingDescription = new PatcheableProp<string>();
        public PatcheableProp<TestProp65Message> Prop65Message = new PatcheableProp<TestProp65Message>();
        public PatcheableProp<TestProductShipping> ShippingAttributes = new PatcheableProp<TestProductShipping>();

        public object GetRequestModel()
        {
            return new { };
        }
    }
}
