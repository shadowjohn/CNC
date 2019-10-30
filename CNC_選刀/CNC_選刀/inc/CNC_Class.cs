using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNC_選刀.inc
{
    public class Item
    {
        public Item() { }

        public string Value { set; get; }
        public string Text { set; get; }
    }
    public class CNC_Class
    {

        public string version;
        public string author;
        public string orin_path;
        public string data;
        public List<string> m_data;
    }
}
