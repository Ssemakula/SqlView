using System;
using System.Collections.Generic;
using System.Text;

namespace SqlView.Types
{
    public class UserDefault
    {
        public int ID { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemValue { get; set; } = string.Empty;

        public override string ToString() => ItemName;
    }
}
