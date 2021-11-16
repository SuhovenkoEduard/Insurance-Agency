using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassesGenerator.Writer
{
    static class MyWriter
    {
        public static void Write(string path, string text)
        {
            using (StreamWriter sw = new StreamWriter(path, append: false))
            {
                sw.Write(text);
            }
        }
    }
}
