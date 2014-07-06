using System;
using Castle.Services.Transaction;
using PocketMoney.Data;
using PocketMoney.Data.Security;
using PocketMoney.Model.Internal;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.Service
{
    public abstract class BaseService : IBaseService
    {

        //[Transaction(TransactionMode.Requires)]
        //protected delegate void ActionRef<TResult>(ref TResult result);

        //        protected readonly IFileService _fileService;
        protected readonly IRepository<User, UserId, Guid> _userRepository;
        protected readonly IRepository<Family, FamilyId, Guid> _familyRepository;
        protected IAuthorization _authorization;
        protected ICurrentUserProvider _currentUserProvider;

        public BaseService(IRepository<User, UserId, Guid> userRepository,
            IRepository<Family, FamilyId, Guid> familyRepository,
            ICurrentUserProvider currentUserProvider)
        {
            _userRepository = userRepository;
            _familyRepository = familyRepository;
            _currentUserProvider = currentUserProvider;
        }

        public IAuthorization Authorization
        {
            get { return _authorization; }
            set { _authorization = value; }
        }

        public ICurrentUserProvider CurrentUserProvider
        {
            get { return _currentUserProvider; }
            set { _currentUserProvider = value; }
        }

        //protected TResult Process<TRequest, TResult>(
        //    ref TRequest request,
        //    Func<TResult> create,
        //    ActionRef<TResult> process)
        //    where TResult : Result
        //    where TRequest : Request
        //{
        //    TResult result = create();
        //    try
        //    {
        //        if (request == null)
        //        {
        //            result.SetErrorMessage<TResult>(REQUEST_ERROR_MESSAGE_NULL);
        //        }

        //        foreach (var varRes in request.Validate(null))
        //        {
        //            result.SetErrorMessage(varRes.ErrorMessage);
        //        }

        //        if (!result.IsSuccess)
        //            return result;

        //        process(ref result);

        //    }
        //    catch (UserLevelException ue)
        //    {
        //        result = result.SetErrorMessage<TResult>(ue.Message);
        //    }
        //    catch (SystemLevelException se)
        //    {
        //        result = result.SetErrorMessage<TResult>(se.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        result = result.SetErrorMessage<TResult>(e.Message);
        //        e.LogError();
        //    }
        //    return result;
        //}

    }
}
