using Dev.WooNet.Model.DevDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.IWooService
{

    /// <summary>
    /// 国家/省/市
    /// </summary>
   public partial interface IDevCountryService
    {
        /// <summary>
        /// 查询国家/省/市
        /// </summary>
        /// <returns></returns>
        IList<AddressDTO> GetAddress();
        
        }
}
