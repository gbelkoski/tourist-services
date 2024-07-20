
using Android.Content;
using Android.Print;

namespace Tourist.ShipmentMobile.Platforms.Droid
{
	public static class PrintService
	{
		public static void Print(WebView webView)
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
	}
}
