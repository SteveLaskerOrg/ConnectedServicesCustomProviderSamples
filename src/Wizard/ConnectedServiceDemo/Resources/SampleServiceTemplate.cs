using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $DefaultNamespace$
{
    public static class SampleWizardService
    {
        public static string CallSampleWizardService()
        {
            return "Hello, from $ServiceInstance.Page1.Text$, $ServiceInstance.Page2.Text$, and $ServiceInstance.Page3.Text$!";
        }
    }
}
