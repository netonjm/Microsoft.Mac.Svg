using Microsoft.Mac.Svg.Helpers;
using ObjCRuntime;

namespace Microsoft.Mac.Svg.Sample;

public partial class ViewController : NSViewController {
    protected ViewController (NativeHandle handle) : base (handle)
    {
    }

    public override void ViewDidLoad ()
    {
        base.ViewDidLoad ();

        // Do any additional setup after loading the view.
        var svgFile = new SvgView() { Frame = new CGRect(10,10, 96, 105), Scaling = PathScaling.Fill };
        svgFile.Layer.BackgroundColor = NSColor.Yellow.CGColor;
        View.AddSubview(svgFile);

        //var btn = new NSButton(new CGRect(70,10, 100, 30)) { Title = "update" };
        //View.AddSubview(btn);

        var resource = FileHelper.GetManifestResource(typeof(ViewController).Assembly, "SuperIcon.svg");
        svgFile.Load(resource);
    }

    public override NSObject RepresentedObject {
        get => base.RepresentedObject;
        set {
            base.RepresentedObject = value;

            // Update the view, if already loaded.
        }
    }
}

