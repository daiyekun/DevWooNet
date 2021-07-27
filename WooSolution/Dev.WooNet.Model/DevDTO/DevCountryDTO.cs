using Dev.WooNet.Common.Models;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.Model.DevDTO
{

    /// <summary>
    /// 国家
    /// </summary>
    public class DevCountryDTO: DevCountry, IModelDTO
    {

    }

    /// <summary>
    /// 省
    /// </summary>
    public class DevProvinceDTO: DevProvince,IModelDTO
    {

    }
    /// <summary>
    /// 市
    /// </summary>
    public class DevCityDTO : DevCity, IModelDTO
    {

    }

}
