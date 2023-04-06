# scanzy-barcodescanner-sample-xamarin
Scanzy barcode scanner sample for xamarin (native iOS, Android), it implements the barcode capture capabilities of the ScanzyBarcodeScannerSDK for iOS and Android. It supports reading a large number of different barcode symbologies, such as Code39, Code93, Code128, Codabar, UPC-A, UPC-E, EAN-8, EAN-13, ITF, QRCode, Aztec, PDF-417, Data Matrix, etc.

If you have any questions or need help, check out official website [scanzy.com](https://scanzy.com). Get a free trial license, you'll be able to integrate Scanzy SDK to your app in less than an hour, and it's insanely simple!


## Prerequisites

.NET is a developer platform made up of tools, programming languages, and libraries for building many different types of applications. Xamarin extends the .NET developer platform with tools and libraries specifically for building apps for Android, iOS, tvOS, watchOS, macOS, and Windows.

Follow the Microsoft official docs to install, configure the IDE for Xamarin: [Xamazin Docs](https://learn.microsoft.com/en-us/xamarin/get-started/installation/?pivots=windows-vs2022)


## Installation

### Xamarin Native - iOS

Install nuget package ScanzyBarcodeScannerSDK from visual studio nuget manager.

### Xamarin Native - Android

Install nuget package ScanzyBarcodeScannerSDK from visual studio nuget manager.

## Quick Start

### iOS

First, It's better to activate the SDK in your app's startup, although it's fine to call this function every single time a barcode is scanned. set the license you obtained from [scanzy.com](https://scanzy.com) for free trial.

```csharp

ScanzyBarcodeManager.SetLicense("your-valid-licensekey");

```

To get the barcode result from the SDK, you should implement the delegate and override the GetBarcode method to follow your actual logic, such as displaying an alert dialog. For example:

```csharp

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

```

Then, insert the below code snippet into the place where you are scanning barcodes, such as a button click event:

```csharp

   //enableVibration: true, vibrate your phone when barcode detected
   //enableBeep: true, play the beep sound when barcode detected
   //enableAutoZoom: the library will zoom in/out automatcially to scan the barcode
   //enableScanRectOnly: only scan the view finder area
   
    ScanzyBarcodeOptions options = new ScanzyBarcodeOptions(
                true,
                true,
                true,
                false,
                ScanzyBarcodeFormat.Code128 | ScanzyBarcodeFormat.Ean13);  //support Code128, Ean13

    mydelegate = new BarcodeDelegate(this);
    ScanzyBarcodeManager.Scan(options, this, mydelegate);
  
```

### Android

First, set the license. It's better to do it in your app's startup, but it's fine to call this function every single time a barcode is scanned.

```csharp

 ScanzyBarcodeManager.SetLicense(this.ApplicationContext,"Your-license-key");

```

Then, insert the below code snippet into the place where you are scanning barcodes, such as a button click event:

```csharp

   //enableVibration: true, vibrate your phone when barcode detected
   //enableBeep: true, play the beep sound when barcode detected
   //enableAutoZoom: the library will zoom in/out automatcially to scan the barcode
   //enableScanRectOnly: only scan the view finder area
   //support Code128, Ean13
   ScanzyBarcodeOptions barcodeOptions = new ScanzyBarcodeOptions(
                EnumSet.Of(
                    ScanzyBSBarcodeFormat.Ean13,
                    ScanzyBSBarcodeFormat.Code128),true,true,true,false);

            ScanzyBarcodeManager manager = new ScanzyBarcodeManager(this.ApplicationContext,barcodeOptions);
            var intent = manager.GetBarcodeScannerIntent(this);

            StartActivityForResult(intent, ScanzyBarcodeManager.RcBarcodeCapture);
             
```

To get the barcode result from the SDK, you should override the OnActivityResult. For example:

```csharp

     protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Android.Content.Intent data)
        {
            if(requestCode == ScanzyBarcodeManager.RcBarcodeCapture)
            {
                if(resultCode == BarcodeScanStatus.Success)
                {
                    if(data != null)
                    {
                        string barcode = data.GetStringExtra("barcode");
                        if(barcode.Equals("permission_missing"))
                        {
                            //You don't have valid license key
                        }
                        else
                        {
                            Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);
                            alertDialog.SetMessage(barcode).SetTitle("SCAN RESULT");
                            Android.App.AlertDialog dialog = alertDialog.Create();
                            dialog.Show();
                        }
                    }
                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

```

