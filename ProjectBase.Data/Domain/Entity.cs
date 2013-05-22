using System;

namespace ProjectBase.Data.Domain
{

    /// <summary>
    ///     Provides a base class for your objects which will be persisted to the database.
    ///     Benefits include the addition of an Id property along with a consistent manner for comparing
    ///     entities.
    /// 
    ///     Since nearly all of the entities you create will have a type of int Id, this 
    ///     base class leverages this assumption.  If you want an entity with a type other 
    ///     than int, such as string, then use <see cref = "EntityWithTypedId{IdT}" /> instead.
    /// </summary>
    [Serializable]
    public abstract class Entity : EntityWithTypedId<int>
    {
        private int _version;
        private DateTime _dateCreated;

        /// <summary>
        /// Version
        /// </summary>
        public int Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }
    }
}