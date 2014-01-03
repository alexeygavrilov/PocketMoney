using System;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model
{
    public static class IUserExtensions
    {
        public static User To(this IUser user)
        {
            if (user == null) return null;
            if (user is User)
                return (User)user;
            else
            {
                var userRepository = ServiceLocator.Current.GetInstance<IRepository<User, UserId, Guid>>();
                var member = userRepository.One(new UserId(user.Id));
                if (member == null)
                    throw new DataNotFoundException("User");
                return member;
            }
        }

        public static WrapperUser From(this IUser user)
        {
            if (user == null) return null;
            if (user is WrapperUser)
                return (WrapperUser)user;
            else
                return new WrapperUser(user);
        }
    }
}
