using eAuto.Data.Interfaces.Enum;
using eAuto.Data.Interfaces.Exceptions;

namespace eAuto.Data.Converters
{
	public static class EngineTypesEnumConverter
	{
		private const string gasoline = "Gasoline";
		private const string diesel = "Diesel";
		private const string electric = "Electric";
		private const string gas = "Gas";
		private const string gasolineGibrid = "Gasoline (gibrid)";
		private const string dieselGibrid = "Diesel (gibrid)";

		public static string ConvertToDbValue(EngineTypesEnum engineType)
		{
			return engineType switch
			{
				EngineTypesEnum.Gasoline => gasoline,
				EngineTypesEnum.Diesel => diesel,
				EngineTypesEnum.Electric => electric,
				EngineTypesEnum.Gas => gas,
				EngineTypesEnum.GasolineGibrid => gasolineGibrid,
				EngineTypesEnum.DieselGibrid => dieselGibrid,
				_ => throw new UnknownEngineTypeException(),
			};
		}

		public static EngineTypesEnum ConvertToDomainValue (string engineType)
		{
			return engineType switch
			{
				gasoline => EngineTypesEnum.Gasoline,
				diesel => EngineTypesEnum.Diesel,
				electric => EngineTypesEnum.Electric,
				gas => EngineTypesEnum.Gas,
				gasolineGibrid => EngineTypesEnum.GasolineGibrid,
				dieselGibrid => EngineTypesEnum.DieselGibrid,
				_ => throw new UnknownEngineTypeException(),
			};
		}
	}
}