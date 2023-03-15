using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;

namespace eAuto.Domain.DomainModels
{
    public sealed class BodyTypeDomainModel : IBodyType
    {
        private readonly IRepository<BodyTypeDataModel> _bodyTypeRepository;
        private string _name;

        public int BodyTypeId { get; set; }
        public string Name { get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new BodyTypeNotFoundException();
                }
                _name = value;
            } 
        }
        public BodyTypeDomainModel()
        {

        }
        //public BodyTypeDomainModel(int bodyTypeId, string name)
        //{
        //    BodyTypeId = bodyTypeId;
        //    Name = name;
        //}
        internal BodyTypeDomainModel(
            BodyTypeDataModel bodyTypeDataModel,
            IRepository<BodyTypeDataModel> bodyTypeRepository)
        {
            _bodyTypeRepository = bodyTypeRepository;
            BodyTypeId = bodyTypeDataModel.BodyTypeId;
            _name = bodyTypeDataModel.Name;

        }

        internal BodyTypeDomainModel(
             IRepository<BodyTypeDataModel> bodyTypeRepository,string name)
        {
            _bodyTypeRepository = bodyTypeRepository;
            Name = name;
        }
    }
}