using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPOK_System.Domain;
using AspNet.Identity.Dapper;

namespace PPOK_System.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TemplatesManager manager = new TemplatesManager();
            Template template = manager.findTemplateGivenId(1);
            Console.WriteLine(template.Id);
            Console.WriteLine(template.Temp_Message);
        }
    }
}
