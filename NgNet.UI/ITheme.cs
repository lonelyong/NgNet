using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI
{
    public interface ITheme : IThemeBase 
    {
        bool Blendable { get; set; }

        double Opacity { get; set; }
    }
}
