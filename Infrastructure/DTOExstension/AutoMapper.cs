using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.DTOExstension
{
    public static class AutoMapper<Tsource, Tdestination>
    {
        private static Mapper _mapper = new Mapper(new MapperConfiguration(
            cfg =>
                cfg.CreateMap<Tsource, Tdestination>().ReverseMap()
            ));

        public static Tdestination Map(Tsource source)
        {
            return _mapper.Map<Tdestination>(source);
        }
        public static Tdestination Map(Tsource source, Tdestination destination)
        {
            return _mapper.Map(source, destination);
        }

        public static List<Tdestination> MapList(List<Tsource> source)
        {
            var list = new List<Tdestination>();
            foreach (var item in source)
            {
                list.Add(Map(item));
            }
            return list;
        }

        public static List<Tdestination> MapListIenum(IEnumerable<Tsource> source)
        {
            var list = new List<Tdestination>();
            foreach (var item in source)
            {
                list.Add(Map(item));
            }
            return list;
        }
    }
}
