using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassesGenerator.Generator
{
    class ClassGenerator
    {
        protected static string endl = new string(new char[] { (char)0x0D, (char)0x0A });
        protected static string tab = "\t";
        protected static string mask =
            "using System;" + endl +
            "using System.Collections.Generic;" + endl +
            "using System.Linq;" + endl +
            "using System.Text;" + endl +
            "using System.Threading.Tasks;" + endl + endl + 
            "namespace CourseApp.Classes" + endl;

        public static string Generate(TableInfo table)
        {
            StringBuilder sb = new StringBuilder(mask);
            StringBuilder tabs = new StringBuilder();


            sb.Append("{").Append(endl);
            {
                AddTab(tabs);
                   

                sb.Append(tabs).Append("public class ").Append(table.Name).Append(endl);
                sb.Append(tabs).Append("{").Append(endl);
                {
                    AddTab(tabs);


                    // properties
                    foreach (var property in table)
                        sb.Append(tabs).Append("public ").Append(property.Type).Append(" ")
                            .Append(property.Name).Append(" ").Append("{ get; set; }").Append(endl);
                    sb.Append(endl);


                    // constructor
                    sb.Append(tabs).Append("public ").Append(table.Name).Append("() { }").Append(endl).Append(endl);
                    

                    // constructor
                    sb.Append(tabs).Append("public ").Append(table.Name).Append("(");

                    sb.Append(table.Skip(1).First().Type).Append(" ").Append(GetLocalName(table.Skip(1).First().Name));
                    foreach (var property in table.Skip(2))
                        sb.Append(", ").Append(property.Type).Append(" ").Append(GetLocalName(property.Name));
                    sb.Append(")").Append(endl);

                    sb.Append(tabs).Append("{").Append(endl);
                    {
                        AddTab(tabs);
                        foreach (var property in table.Skip(1))
                            sb.Append(tabs).Append(property.Name).Append(" = ").Append(GetLocalName(property.Name)).Append(";").Append(endl);
                        RemoveTab(tabs);
                    }
                    sb.Append(tabs).Append("}").Append(endl).Append(endl);


                    // ToString()
                    sb.Append(tabs).Append("public override string ToString()").Append(endl);
                    sb.Append(tabs).Append("{").Append(endl);
                    {
                        AddTab(tabs);
                        sb.Append(tabs).Append("return ").Append(table.First().Name);
                        AddTab(tabs);
                        
                        foreach (var propName in table.Select(x => x.Name).Skip(1))
                            sb.Append(" + \" \" +").Append(endl).Append(tabs).Append(propName);
                        sb.Append(";").Append(endl);

                        RemoveTab(tabs);
                        RemoveTab(tabs);
                    }
                    sb.Append(tabs).Append("}").Append(endl);

                    RemoveTab(tabs);
                }
                sb.Append(tabs).Append("}").Append(endl);


                RemoveTab(tabs);
            }
            sb.Append("}").Append(endl);

            return sb.ToString();
        }
        
        private static StringBuilder AddTab(StringBuilder tabs) => tabs.Append(tab);
        private static StringBuilder RemoveTab(StringBuilder tabs) => tabs.Remove(tabs.Length - 1, 1);
        private static string GetLocalName(string globalName)
        {
            StringBuilder sb = new StringBuilder(globalName);
            sb[0] = char.ToLower(globalName[0]);
            return sb.ToString();
        }
    }
}
