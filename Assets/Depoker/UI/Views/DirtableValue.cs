using System;

namespace Depoker.UI.Views
{
    public class DirtableValue<T>
    {
        private bool inited;
        private T value;

        public T Value
        {
            get { return value; }
            set { SetValue(value); }
        }

        public Action<T> ValueUpdated;

        private void SetValue(T possibleNewValue)
        {
            if (!inited || !value.Equals(possibleNewValue))
            {
                inited = true;
                value = possibleNewValue;
                ValueUpdated?.Invoke(value);
            }
        }
    }
}