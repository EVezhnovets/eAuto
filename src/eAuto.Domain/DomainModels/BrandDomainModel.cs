using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using BrandDataM = eAuto.Data.Interfaces.DataModels.BrandDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class BrandDomainModel : IBrand
    {
        private readonly IRepository<BrandDataM>? _brandRepository;
        private readonly bool _isNew;

        public int BrandId { get; set; }
        public string? Name { get; set; }
        public BrandDomainModel() { }

        internal BrandDomainModel(
            BrandDataM brandDataModel,
            IRepository<BrandDataM> brandRepository)
        {
            _brandRepository = brandRepository;

            BrandId = brandDataModel.BrandId;
            Name = brandDataModel.Name!;

        }

        internal BrandDomainModel(
             IRepository<BrandDataM> brandRepository,string name)
        {
            _brandRepository = brandRepository;
            _isNew = true;

            Name = name;
        }

		public void Save()
		{
			var brandDataModel = GetBrandDataModel();

            if (_isNew)
            {
                var result = _brandRepository!.Create(brandDataModel);
                BrandId = result.BrandId;
            }
            else
            {
                _brandRepository!.Update(brandDataModel);
            }
        }

		public void Delete()
		{
            var brandModel = GetBrandDataModel();
            if (!_isNew)
            {
                _brandRepository!.Delete(brandModel);
            }
		}

        private BrandDataM GetBrandDataModel()
        {
            var brandDataM = new BrandDataM
            {
                BrandId = BrandId,
                Name = Name
            };
            return brandDataM;
		}
	}
}