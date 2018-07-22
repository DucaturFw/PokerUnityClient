using System;

namespace Depoker.UI.Views
{
    public class DirtableValue<T>
    {
        private T value;

        public T Value
        {
            get { return value; }
            set { SetValue(value); }
        }

        public Action<T> ValueUpdated;

        private void SetValue(T possibleNewValue)
        {
            if (!value.Equals(possibleNewValue))
            {
                value = possibleNewValue;
                ValueUpdated?.Invoke(value);
            }
        }
    }
}