﻿/*------------------------------------------------------------------------------
<auto-generated>
     This code was generated by a tool.
	    Code originates from EA Uml ClassTemplate.t4
     Changes to this file will be lost if the code is regenerated.
	    Code Generated Date: 	08 June 2018
	    ProjectModel: 			LayrCake
	    Requested Namespace:	Model$2. Hosting Model$LayrCake.StaticModel$LayrCake$StaticModel$ViewModelObjects$Implementation
</auto-generated>
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LayrCake.WebApi.Models.Implementation
{
	// Render the class
	public partial class DDDConnector : AzureEntityModel, IAzureTableData // IApiModelObject
	{
		public int DDDConnectorID { get; set; }

		public string Name { get; set; }

		public int FromElement_Ref { get; set; }

		public int ToElement_Ref { get; set; }

		public Nullable<int> FromMethod_Ref { get; set; }

		public Nullable<int> ToMethod_Ref { get; set; }

		public int IsNavigable { get; set; }

		public int IsArray { get; set; }

		public string Type { get; set; }

		public string Notes { get; set; }

		public int DDDPackage_Ref { get; set; }

		public DDDConnector()
		{
		}
	}
}
