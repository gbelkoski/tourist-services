using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.Domain;

namespace Cleanex.ClientApp.Infrastructure
{
	internal interface IPrintService
	{
		void Print(List<Models.ShipmentItemModel> lineItems);
	}
}
