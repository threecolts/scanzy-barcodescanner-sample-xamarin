# scanzy-barcodescanner-sample-xamarin
scanzy barcode scanner sample for xamarin (native iOS, Android)

## Environment Setup

.NET is a developer platform made up of tools, programming languages, and libraries for building many different types of applications. Xamarin extends the .NET developer platform with tools and libraries specifically for building apps for Android, iOS, tvOS, watchOS, macOS, and Windows.

Follow the Microsoft official docs to install, configure the IDE for Xamarin: [Xamazin Docs](https://learn.microsoft.com/en-us/xamarin/get-started/installation/?pivots=windows-vs2022)


## Get Started

# Xamarin Native - iOS

Install nuget package ScanzyBarcodeScannerSDK from visual studio nuget manager.

To use this plugin:

Firstly, set the license, it's better to do it in your app's startup, although it's fine to call this function every single time to scan the barcode.

```csharp

ScanzyBarcodeManager.SetLicense("your-valid-licensekey");

```

To get the barcode result from SDK, you should implement the delegate and override the GetBarcode method to follow your actual logic, such as display the alert dialog. For example,

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

Then, insert below code snippet into the place to scan barcode, such as button click event:

```csharp

  //support Code128, Ean13
  
   ScanzyBarcodeFormat format = ScanzyBarcodeFormat.Code128 | ScanzyBarcodeFormat.Ean13;

   //enableVibration: true, vibrate your phone when barcode detected
   //enableBeep: true, play the beep sound when barcode detected
   //enableAutoZoom: the library will zoom in/out automatcially to scan the barcode
   //enableScanRectOnly: only scan the view finder area
   ScanzyBarcodeOptions options = new ScanzyBarcodeOptions(true,true,true,false, format);
   ScanzyBarcodePicker picker = new ScanzyBarcodePicker(options);
   mydelegate = new BarcodeDelegate(this);
   picker.Delegate = mydelegate;
            
   this.PresentViewController(picker, true, null);
  
```
# Xamarin Native - Android

Install nuget package ScanzyBarcodeScannerSDK from visual studio nuget manager.


To use this plugin in Javascript:

Firstly, set the license, it's better to do it in your app's startup, although it's fine to call this function every single time to scan the barcode.

```csharp

 ScanzyBarcodeManager.SetLicense(this.ApplicationContext,"Your-license-key");

```

Then, insert below code snippet into the place to scan barcode, such as button click event:

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

To get the barcode result from SDK, you should override the OnActivityResult. For example,

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

That is all you need to use the plugin. :joy:

