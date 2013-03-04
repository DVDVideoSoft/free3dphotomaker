using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public class ObjectArrayCreator<T>
    {
        public static object[] Create(T[] objects)
        {
            object[] retObjectsArray = new object[objects.Length];
            int i = 0;
            foreach (T item in objects)
            {
                retObjectsArray.SetValue(item, i);
                i++;
            }
            return retObjectsArray;
        }

        public static object[] Create(IList<T> objects)
        {
            object[] retObjectsArray = new object[objects.Count];
            int i = 0;
            foreach (T item in objects)
            {
                retObjectsArray.SetValue(item, i);
                i++;
            }
            return retObjectsArray;
        }
    }

    public class Helper<T>
    {
        public static T StringToEnum(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }

    public class EventArgs<T> : EventArgs
    {
        private T value;

        public T Value { get { return this.value; } }

        public EventArgs(T value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
