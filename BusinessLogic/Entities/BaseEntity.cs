using System;

namespace BusinessLogic.Entities
{
    [Serializable]
    public class BaseEntity<TId>
    {
        public virtual TId Id { get; set; }
        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity<TId>);
        }
        private static bool IsTransient(BaseEntity<TId> obj)
        {
            return obj != null &&
                   Equals(obj.Id, default(TId));
        }
        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(BaseEntity<TId> other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!IsTransient(this) && !IsTransient(other) && Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
            }

            return false;
        }


        public override int GetHashCode()
        {
            if (Equals(Id, default(TId)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }
    }

}
