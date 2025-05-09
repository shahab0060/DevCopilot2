using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCopilot2.Domain.DTOs.Generators
{
    public class ViewDataListDto
    {
        public string Name { get; set; } = null!;

        public string ServiceName { get; set; } = null!;

        public bool IsRequired { get; set; } = true;
    }
}
