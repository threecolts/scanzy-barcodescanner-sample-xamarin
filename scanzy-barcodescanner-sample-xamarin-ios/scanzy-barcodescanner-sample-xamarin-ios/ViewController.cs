using Foundation;
using System;
using UIKit;
using ScanzyBarcodeScannerSDKBindings;

namespace ios2
{

    public class BarcodeDelegate : ScanzyBarcodeScannedProtocolDelegate
    {
        private UIViewController _uIViewController;
        public BarcodeDelegate(UIViewController uIViewController)
        {
            this._uIViewController = uIViewController;
        }

        public override void GetBarcode(string barcode, string barcodeType)
        {
            var okAlertController = UIAlertController.Create("Barcode", barcode+","+barcodeType, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this._uIViewController.PresentViewController(okAlertController, true, null);
        }
    }

    public partial class ViewController : UIViewController
    {
        public ScanzyBarcodeScannedProtocolDelegate mydelegate;

        partial void scan(NSObject sender)
        {
            ScanzyBarcodeOptions options = new ScanzyBarcodeOptions(
                true,
                true,
                true,
                false,
                ScanzyBarcodeFormat.Code128 | ScanzyBarcodeFormat.Ean13);

            mydelegate = new BarcodeDelegate(this);
            ScanzyBarcodeManager.Scan(options, this, mydelegate);

           
        }

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
