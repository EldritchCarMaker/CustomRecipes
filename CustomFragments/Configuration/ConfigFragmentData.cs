using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFragments.Configuration
{
    internal class ConfigFragmentData
    { 
        public int fragmentCount = 1;
        public float scanTime = -1;

        [JsonConstructor]
        public ConfigFragmentData(int fragmentCount, float scanTime = -1f)
        {
            this.fragmentCount = fragmentCount;
            this.scanTime = scanTime;
        }

    }
}
