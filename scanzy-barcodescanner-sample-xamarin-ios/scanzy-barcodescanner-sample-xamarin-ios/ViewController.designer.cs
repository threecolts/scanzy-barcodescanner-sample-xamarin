// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ios2
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton btnScan { get; set; }

		[Action ("scan:")]
		partial void scan (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnScan != null) {
				btnScan.Dispose ();
				btnScan = null;
			}
		}
	}
}
