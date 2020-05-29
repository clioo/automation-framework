namespace HttpUtility.Services.AutomationDataFactory.Models.Merchandise
{
    internal class ProductConfigurationRequest
    {
        TestProp65Message _prop65Message;
        bool _prop65MessageHasChanged = false;
        public TestProp65Message Prop65Message
        {
            get => _prop65Message;
            set
            {
                _prop65Message = value;
                _prop65MessageHasChanged = true;
            }
        }
        string _offeringDescription;
        bool _offeringDescriptionHasChanged = false;
        public string OfferingDescription
        {
            get => _offeringDescription;
            set
            {
                _offeringDescriptionHasChanged = true;
                _offeringDescription = value;
            }
        }

        public object GetProductRequest()
        {
            if (_prop65MessageHasChanged)
            {
                return new
                {
                    Prop65Message
                };
            }

            //default path
            return new { };
        }
    }
}
