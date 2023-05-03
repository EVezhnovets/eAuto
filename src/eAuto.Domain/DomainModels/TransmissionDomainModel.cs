using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using TransmissionDataM = eAuto.Data.Interfaces.DataModels.TransmissionDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class TransmissionDomainModel : ITransmission
    {
        private readonly IRepository<TransmissionDataM> _transmissionRepository;
        private string _name;
        private readonly bool _isNew;

        public int TransmissionId { get; set; }
        public string Name { get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new GenericNotFoundException<TransmissionDomainModel>();
                }
                _name = value;
            } 
        }

        public TransmissionDomainModel()
        {
        }

        public TransmissionDomainModel(IRepository<TransmissionDataM> transmissionRepository)
        {
            _transmissionRepository = transmissionRepository;
            _isNew = true;
        }

        internal TransmissionDomainModel(
            TransmissionDataM transmissionDataModel,
            IRepository<TransmissionDataM> transmissionRepository)
        {
            _transmissionRepository = transmissionRepository;
            _name = transmissionDataModel.Name;

            TransmissionId = transmissionDataModel.TransmissionId;
        }


		public void Save()
		{
			var transmissionDataModel = GetTransmissionDataModel();

            if (_isNew)
            {
                var result = _transmissionRepository.Create(transmissionDataModel);
                TransmissionId = result.TransmissionId;
            }
            else
            {
                _transmissionRepository.Update(transmissionDataModel);
            }
        }

		public void Delete()
		{
            var transmissionModel = GetTransmissionDataModel();
            if (!_isNew)
            {
                _transmissionRepository.Delete(transmissionModel);
            }
		}

        private TransmissionDataM GetTransmissionDataModel()
        {
            var transmissionDataM = new TransmissionDataM
            {
                TransmissionId = TransmissionId,
                Name = Name
			};
            return transmissionDataM;
		}
	}
}