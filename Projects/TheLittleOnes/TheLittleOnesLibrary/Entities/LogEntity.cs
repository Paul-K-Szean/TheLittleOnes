using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TheLittleOnesLibrary.Entities
{
    public class LogEntity
    {
        private string logLevelValue;
        public string logLevel
        {
            get
            {
                return "System";
            }
            set
            {
                this.logLevelValue = value;
            }
        }
        public string message { get; set; }
    }
}
