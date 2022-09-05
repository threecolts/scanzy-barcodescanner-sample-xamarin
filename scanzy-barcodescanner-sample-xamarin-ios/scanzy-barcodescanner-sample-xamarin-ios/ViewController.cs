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

        public override void GetBarcode(string barcode)
        {
            var okAlertController = UIAlertController.Create("Barcode", barcode, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this._uIViewController.PresentViewController(okAlertController, true, null);
        }
    }

    public partial class ViewController : UIViewController
    {
        public ScanzyBarcodeScannedProtocolDelegate mydelegate;

        partial void scan(NSObject sender)
        {
            ScanzyBSBarcodeOptions options = new ScanzyBSBarcodeOptions(
                ScanzyBSBarcodeFormat.Code128 | ScanzyBSBarcodeFormat.Ean13,
                true,
                true,
                true,
                false);

            mydelegate = new BarcodeDelegate(this);
            ScanzyBarcodeManager.Scan(options, this, mydelegate);

            //ScanzyBSBarcodePicker picker = new ScanzyBSBarcodePicker(options);
            //mydelegate = new BarcodeDelegate(this);
            //picker.Delegate = mydelegate;
            //this.PresentViewController(picker, true, null);
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
