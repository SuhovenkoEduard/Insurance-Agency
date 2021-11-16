using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ClassesGenerator
{
    class TableInfo : ICollection<ColumnInfo>
    {
        public string Name { get; set; }
        protected List<ColumnInfo> typeInfos { get; set; }
        public TableInfo(string name)
        {
            Name = name;
            typeInfos = new List<ColumnInfo>();
        }

        public int Count => typeInfos.Count;

        public bool IsReadOnly => false;

        public void Add(ColumnInfo item) => typeInfos.Add(item);

        public void Clear() => typeInfos.Clear();

        public bool Contains(ColumnInfo item) => typeInfos.Contains(item);

        public void CopyTo(ColumnInfo[] array, int arrayIndex) => throw new NotImplementedException();

        public bool Remove(ColumnInfo item) => typeInfos.Remove(item);

        public IEnumerator<ColumnInfo> GetEnumerator() => typeInfos.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => typeInfos.GetEnumerator();
    }
}
