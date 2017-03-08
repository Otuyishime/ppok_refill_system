using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPOK_System.Domain
{
    public class Template
    {
        public int Id { get; set; }
        public TemplateType templateType { get; set; }
        public CommunicationType communicationType { get; set; }
        public string Temp_Message { get; set; }
    }
}
