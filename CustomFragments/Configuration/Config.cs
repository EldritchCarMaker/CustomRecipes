using Nautilus.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFragments.Configuration
{
    internal class Config : ConfigFile
    {
        public Dictionary<string, ConfigFragmentData> modifiedFragmentCounts = new Dictionary<string, ConfigFragmentData>()
        {
            {
                TechType.Seaglide.ToString(),
                new ConfigFragmentData(2, -1)
            }
        };
    }
}
