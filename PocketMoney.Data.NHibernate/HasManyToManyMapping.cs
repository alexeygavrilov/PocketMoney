namespace PocketMoney.Data.NHibernate
{
    using System;
    using System.Linq.Expressions;

    using FluentNHibernate.Mapping;

    public abstract class HasManyToManyMapping<TL, TR> : ClassMap<HasManyToManyMapping<TL, TR>.HasManyEntity<TL, TR>>    
    {
        public class HasManyEntity<L, R> : IEquatable<HasManyEntity<L, R>>
        {
            public virtual object Id { get; set; }
            public virtual L ParentKey { get; set; }
            public virtual R ChildKey { get; set; }
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }
                if (obj.GetType() != typeof(HasManyEntity<L, R>))
                {
                    return false;
                }
                return Equals((HasManyEntity<L, R>)obj);
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public virtual bool Equals(HasManyEntity<L, R> other)
            {
                if (ReferenceEquals(null, other))
                {
                    return false;
                }
                if (ReferenceEquals(this, other))
                {
                    return true;
                }
                return Equals(other.ParentKey, this.ParentKey) && Equals(other.ChildKey, this.ChildKey);
            }

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object"/>.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode()
            {
                unchecked
                {
                    return (this.ParentKey.GetHashCode() * 397) ^ this.ChildKey.GetHashCode();
                }
            }

            public static bool operator ==(HasManyEntity<L, R> left, HasManyEntity<L, R> right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(HasManyEntity<L, R> left, HasManyEntity<L, R> right)
            {
                return !Equals(left, right);
            }
        }

        protected HasManyToManyMapping(string tableName, string leftColumnName, string rightColumnName, Expression<Func<HasManyEntity<TL, TR>, object>> leftId, Expression<Func<HasManyEntity<TL, TR>, object>> rightId)
        {
            this.Table(tableName);
            this.CompositeId().KeyReference(leftId, leftColumnName).KeyReference(rightId, rightColumnName);
        }
    }
}