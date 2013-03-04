using System;

namespace DVDVideoSoft.Utils
{
    public class ComboBoxItem<T>
    {
        T value;
        string name;

        public ComboBoxItem()
        {
            value = default(T);
            name = string.Empty;
        }

        public ComboBoxItem(T value, string name)
        {
            this.value = value;
            this.name = name;
        }

        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}