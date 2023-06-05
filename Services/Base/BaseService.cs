using VenatorWebApp.Models.Abstracts.Base;
using VenatorWebApp.Services.Util;

namespace VenatorWebApp.Services.Base
{
    public abstract class BaseService
    {
        private readonly IFillModelsService _fillModelsService;

        protected BaseService(IFillModelsService fillModelsService)
        {
            _fillModelsService = fillModelsService;
        }

        protected T Fill<T>(T entity) where T : BaseEntity
        {
            if (entity == null) { return null; }
            _fillModelsService.Fill(entity);
            return entity;
        }
    }
}
