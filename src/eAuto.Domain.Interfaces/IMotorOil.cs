namespace eAuto.Domain.Interfaces
{
    public interface IMotorOil
	{
        int MotorOilId { get; set; }
        string Name { get; set; }
        string PictureUrl { get; set; }
        decimal Price { get; set; }
        string Viscosity { get; set; }
		string Composition { get; set; }
		int Volume { get; set; }
		int ProductBrandId { get; set; }
		string ProductBrand { get; set; }
		void Save();
        void Delete();
    }
}