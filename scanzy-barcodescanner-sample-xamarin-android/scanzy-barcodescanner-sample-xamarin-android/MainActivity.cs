﻿using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;

using Com.Scanzy.Datacapture.Barcodescanner;
using Java.Util;

namespace scanzy_barcodescanner_sample_xamarin_android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            Android.Widget.Button btnScan = FindViewById<Android.Widget.Button>(Resource.Id.btnScan);
 
            btnScan.Click += BtnScan_Click;
            ScanzyBarcodeManager.SetLicense(this.ApplicationContext, "BdyCh9eyxw$9#k2qX79Z");
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            ScanzyBarcodeOptions barcodeOptions = new ScanzyBarcodeOptions(
                false,false,false,false,EnumSet.Of(
                    ScanzyBarcodeFormat.Ean13,
                    ScanzyBarcodeFormat.Upca,
                    ScanzyBarcodeFormat.Code128));

            ScanzyBarcodeManager manager = new ScanzyBarcodeManager(this.ApplicationContext,barcodeOptions);
            var intent = manager.GetBarcodeScannerIntent(this);

            StartActivityForResult(intent, ScanzyBarcodeManager.RcBarcodeCapture);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Android.Content.Intent data)
        {
            if(requestCode == ScanzyBarcodeManager.RcBarcodeCapture)
            {
                if(resultCode == BarcodeScanStatus.Success)
                {
                    if(data != null)
                    {
                        string barcode = data.GetStringExtra("barcode");
                        string barcode_type = data.GetStringExtra("barcode_type");
                        if (barcode.Equals("permission_missing"))
                        {
                            //You don't have valid license key
                        }
                        else
                        {
                            Android.App.AlertDialog.Builder alertDialog = new Android.App.AlertDialog.Builder(this);
                            alertDialog.SetMessage(barcode+","+barcode_type).SetTitle("SCAN RESULT");
                            Android.App.AlertDialog dialog = alertDialog.Create();
                            dialog.Show();
                        }
                    }
                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}

