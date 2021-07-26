using Dev.WooNet.Model.DevDTO;
using Dev.WooNet.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.WooNet.WooService
{

    /// <summary>
    /// 国家省市查询
    /// </summary>
    public partial class DevCountryService
    {

        /// <summary>
        /// 返回3级联动数据集合(国家\省\市)
        /// </summary>
        /// <returns></returns>
        public IList<AddressDTO> GetAddress()
        {
            IList<AddressDTO> listAddress = new List<AddressDTO>();
            //国家
            var listCountry = GetQueryable(a => 1 == 1).ToList();
            //省
            var listProvince = DevDb.Set<DevProvince>().ToList();
            //市
            var listCity = DevDb.Set<DevCity>().ToList();
            foreach (var country in listCountry)
            {
                var ct = new AddressDTO
                {
                    Code = country.Id.ToString(),
                    Name = country.ShowName,
                };
                ct.Childs = AddProvince(listProvince, listCity, country.Id);
                listAddress.Add(ct);
            }

            return listAddress;


        }
        /// <summary>
        /// 添加省
        /// </summary>
        /// <param name="listlistProvince">省数据</param>
        /// <param name="listcity">市数据</param>
        /// <param name="countryId">国家ID</param>
        private IList<AddressDTO> AddProvince(IList<DevProvince> listlistProvince, IList<DevCity> listcity, int countryId)
        {
            var listprvs = listlistProvince.Where(a => a.Cid == countryId).ToList();
            IList<AddressDTO> listProvs = new List<AddressDTO>();
            foreach (var prv in listprvs)
            {
                var add_pv = new AddressDTO
                {

                    Code = prv.Id.ToString(),
                    Name = prv.ShowName,
                };
                add_pv.Childs = AddCity(listcity, prv.Id);
                listProvs.Add(add_pv);
            }

            return listProvs;




        }

        /// <summary>
        /// 添加省
        /// </summary>
        /// <param name="listcity">市数据集合</param>
        /// <param name="ProvinceId">省ID</param>
        private IList<AddressDTO> AddCity(IList<DevCity> listcity, int ProvinceId)
        {
            var listcitys = listcity.Where(a => a.PrId == ProvinceId).ToList();
            IList<AddressDTO> listCitys = new List<AddressDTO>();
            foreach (var prv in listcitys)
            {
                var add_city = new AddressDTO
                {

                    Code = prv.Id.ToString(),
                    Name = prv.ShowName,
                };
                listCitys.Add(add_city);

            }

            return listCitys;




        }
    }
}
