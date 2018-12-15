using StaticModel.ViewModelObjects;
using System;

namespace LayrCake.StaticModel.ViewModelObjects
{
    public abstract class BaseViewModelObject : ViewModelObject
    {
        public int CreatedBy { get; set; }

        public DateTimeOffset Created { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTimeOffset? Updated { get; set; }

        public int? DeletedBy { get; set; }

        public DateTimeOffset? Deleted { get; set; }
    }
}
