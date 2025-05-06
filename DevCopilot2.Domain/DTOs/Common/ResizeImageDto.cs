using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCopilot2.Domain.DTOs.Common
{
    public class ResizeImageDto
    {
        public string ResizedImagePath { get; set; }

        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }
    }
}
