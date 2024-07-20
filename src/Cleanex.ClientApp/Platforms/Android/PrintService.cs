
using Android.Content;
using Android.Print;
using Tourist.Domain;

namespace Cleanex.ClientApp.Platforms.Droid
{
	public class PrintService : Infrastructure.IPrintService
    {
		public void Print(WebView webView)
		{
			//var droidViewToPrint = Platform.CreateRenderer(webView).ViewGroup.GetChildAt(0) as Android.Webkit.WebView;
			var droidViewToPrint = webView.Handler.PlatformView as Android.Webkit.WebView;
			if (droidViewToPrint != null)
			{
			   var version = Android.OS.Build.VERSION.SdkInt;

			   if (version >= Android.OS.BuildVersionCodes.Kitkat)
			   {
			       var printMgr = MainActivity.Instance.GetSystemService(Context.PrintService) as PrintManager;

			       PrintDocumentAdapter printDocumentAdapter;
			       string jobName = "Print";

			       if (version >= Android.OS.BuildVersionCodes.Lollipop)
			       {
			           printDocumentAdapter = droidViewToPrint.CreatePrintDocumentAdapter(jobName);

			       }
			       else
			       {
			           printDocumentAdapter = droidViewToPrint.CreatePrintDocumentAdapter();
			       }
			       printMgr.Print("Print", printDocumentAdapter, null);
			   }
			}
		}

		public void Print(List<Models.ShipmentItemModel> shipment)
		{
			throw new NotImplementedException();
		}
	}
}
