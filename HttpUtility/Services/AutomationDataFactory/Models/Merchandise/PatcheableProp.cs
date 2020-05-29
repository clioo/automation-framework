namespace HttpUtility.Services.AutomationDataFactory.Models.Merchandise
{
    public class PatcheableProp<T>
    {
        T _value;
        public bool HasValue { get; private set; } = false;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                HasValue = true;
            }
        }
    }
}
