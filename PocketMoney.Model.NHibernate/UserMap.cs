﻿using FluentNHibernate;
using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class UserMap : VersionedClassMap<User>
    {
        public UserMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.UserName).Not.Nullable().Length(100).UniqueKey("UX_UserName");
            Map(x => x.ConfirmCode).Not.Nullable().Length(User.CONFIRM_CODE_LENGTH);
            Map(x => x.AdditionalName).Nullable().Length(100);
            Map(x => x.Active).Not.Nullable();
            Map(x => x.TokenKey).Not.Nullable().Length(User.TOKEN_KEY_LENGTH);

            Map(x => x.LastLoginDate);
            Map(Reveal.Member<User>("_password")).Column("Password").Length(255);
            Map(Reveal.Member<User>("_roles")).Column("Role").Not.Nullable();

            Component(x => x.Points);
            Component<TaskCount>(x => x.TaskCount,
                m =>
                {
                    m.Map(x => x.Completed).Column("CompletedTaskCount").Not.Nullable();
                    m.Map(x => x.Grabbed).Column("GrabbedTaskCount").Not.Nullable();
                    m.ParentReference(x => x.Parent);
                    m.HasMany<int>(Reveal.Member<TaskCount>("_taskTypeCounts"))
                        .Table("TaskCountsUser")
                        .Element("TaskTypeCount")
                        .KeyColumn("UserId")
                        .ForeignKeyConstraintName("FK_User_TaskTypeCounts");
                });

            References(x => x.Family).Not.Nullable().Not.LazyLoad().ForeignKey("FK_User_Family").UniqueKey("UX_UserName");
            References(x => x.Email).Nullable().ForeignKey("FK_User_Email");
            References(x => x.Phone).Nullable().ForeignKey("FK_User_Phone");

            HasMany(x => x.Connections).LazyLoad().Cascade.Delete().ForeignKeyConstraintName("FK_User_Connection");
            HasMany(x => x.AssignedTasks).LazyLoad().ForeignKeyConstraintName("FK_Performer_User");
        }
    }
}
