using Identity.CustomIdentityDB.Repository;

namespace Identity.CustomIdentityDB.Service
{
    public abstract class BaseService
    {
        protected IIdentityUnitOfWork _uow;

        public BaseService(IIdentityUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}