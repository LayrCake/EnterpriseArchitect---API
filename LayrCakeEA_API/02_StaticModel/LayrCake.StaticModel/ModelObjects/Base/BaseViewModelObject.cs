using StaticModel.ViewModelObjects;
using System;

namespace LayrCake.StaticModel.ViewModelObjects
{
    public abstract class BaseViewModelObject : ViewModelObject
    {
        public int CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public int? DeletedBy { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
