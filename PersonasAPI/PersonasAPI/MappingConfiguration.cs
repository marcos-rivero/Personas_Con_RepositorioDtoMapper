using AutoMapper;
using PersonasAPI.Modelo;
using PersonasAPI.Modelo.Dtos;

namespace PersonasAPI
{
    public class MappingConfiguration
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<PersonasDto, Personas>();
				config.CreateMap<Personas, PersonasDto>();
			});
			return mappingConfig;
		}
	}
}
