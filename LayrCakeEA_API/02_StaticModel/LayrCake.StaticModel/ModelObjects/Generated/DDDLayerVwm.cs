﻿/*------------------------------------------------------------------------------
<auto-generated>
     This code was generated by a tool.
	    Code originates from EA Uml ClassTemplate.t4
     Changes to this file will be lost if the code is regenerated.
	    Code Generated Date: 	08 June 2018
	    ProjectModel: 			LayrCake
	    Requested Namespace:	Model$3. Service Model$LayrCake.ActionService$LayrCake$ActionService$DataTransferObjects$Implementation
</auto-generated>
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using StaticModel.ViewModelObjects;

namespace LayrCake.StaticModel.ViewModelObjects.Implementation
{
	// Render the class
	public partial class DDDLayerVwm : BaseViewModelObject, IViewModelObject
	{
		public int DDDLayerID { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int? DDDSolution_Ref { get; set; }

		public int? DDDLayerTemplate_Ref { get; set; }

		public string MobileId { get; set; }

		public DDDSolutionVwm DDDSolution { get; set; }

		public List<DDDPackageVwm> DDDPackages { get; set; }

		public DDDLayerVwm()
		{
		    DDDPackages = new List<DDDPackageVwm>();
		}
	}
}
