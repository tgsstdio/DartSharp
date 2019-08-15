namespace DartSharp.Functions
{
    public class ReturnValue
    {
        private object value;

        public ReturnValue(object value)
        {
            this.value = value;
        }

        public object Value { get { return this.value; } }
    }
}
