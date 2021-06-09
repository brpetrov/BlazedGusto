using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary
{
    public class SqlConnectionConfiguration
    {
        public SqlConnectionConfiguration(string value) => Value = value;
        public string Value { get; }
    }
}
