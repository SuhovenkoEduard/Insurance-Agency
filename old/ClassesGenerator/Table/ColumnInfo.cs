using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesGenerator
{
    enum MyType
    {
        Int,
        String
    }

    class ColumnInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }

        public ColumnInfo(string name, MyType type, int size)
        {
            Name = name;
            Type = type.ToString().ToLower();
            Size = size;
        }

        public override string ToString()
        {
            return Name + " " +
                Type + " " +
                Size;
        }
    }
}
