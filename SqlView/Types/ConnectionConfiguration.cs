using System;
using System.Collections.Generic;
using System.Text;

namespace SqlView.Types
{
    public class ConnectionConfiguration
    {
        public int ID { get; set; }
        public string ConnectionName { get; set; } = string.Empty;
        public string DataSource { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public override string ToString() => ConnectionName;
    }
}
