using System;

namespace Depoker.UI.Views
{
    public class DirtableValue<T>
    {
        private bool inited;
        private T value;
        private T previous;

        public T Value
        {
            get { return value; }
            set { SetValue(value); }
        }

        public T Previous => previous;

        public Action<T> ValueUpdated;

        private void SetValue(T possibleNewValue)
        {
            if (!inited || !value.Equals(possibleNewValue))
            {
                inited = true;
                previous = value;
                value = possibleNewValue;
                ValueUpdated?.Invoke(value);
            }
        }
    }
}