// Authors:
//   Jose Medrano <josmed@microsoft.com>
//
// Copyright (C) 2018 Microsoft, Corp
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the
// following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN
// NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
// USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

using AppKit;
using CoreAnimation;
using CoreGraphics;
using Microsoft.Mac.Svg;
using Microsoft.Mac.Svg.Operations;

namespace Microsoft.Mac.Svg
{
    public static class PathExtensions
    {
        public static CAShapeLayer ToShape(this GPath element)
        {
            var shape = new CAShapeLayer();
            foreach (var item in element.Paths)
                if (item is GPath gp)
                    shape.AddSublayer(gp.ToShape());
                else if (item is Microsoft.Mac.Svg.Path pa)
                    shape.AddSublayer(pa.ToShape());
                else if (item is CirclePath cir)
                    shape.AddSublayer(cir.ToShape());
                else if (item is RectanglePath rec)
                    shape.AddSublayer(rec.ToShape());
                else if (item is LinePath line)
                    shape.AddSublayer(line.ToShape());
                else if (item is TextPath text)
                    shape.AddSublayer(text.ToShape());
            return shape;
        }

        public static CAShapeLayer ToShape(this RectanglePath element)
        {
            var shape = new CAShapeLayer();
            shape.Frame = new CGRect(0,0, element.Width, element.Height);
            //var bezierPath = NSBezierPath.FromRect (new CGRect(0, 0, element.Width, element.Height));
            //shape.Path = bezierPath.ToCGPath();
            

            if (!string.IsNullOrEmpty(element.Stroke))
                shape.StrokeColor = XExtensions.ConvertToNSColor(element.Stroke).CGColor;

            if (!string.IsNullOrEmpty(element.Fill))
            {
                shape.FillColor = XExtensions.ConvertToNSColor(element.Fill).CGColor;
                shape.BackgroundColor = shape.FillColor;
            }

            shape.LineWidth = element.StrokeWidth * 2;

            ConfigureTransformation(shape, element);
            //shape.Bounds = new CGRect(0, 0, element.Width, element.Height);
            return shape;
        }

        public static void ConfigureTransformation(this CAShapeLayer layer, LayerHtmlElement element)
        {
            if (string.IsNullOrEmpty(element.Transform))
                return;

            var operation = OperationBuilder.Build(element.Transform);
            if (operation is Translate translate)
            {
                layer.Transform = CATransform3D.MakeTranslation(translate.X, translate.Y, 0);
            } else
            {
                throw new NotImplementedException(operation.GetType().FullName);
            }
        }

        public static CAShapeLayer ToShape(this LinePath element)
        {
            var line = new CAShapeLayer();

            var bezierPath = new NSBezierPath();
            bezierPath.MoveTo(new CGPoint(element.X1, element.Y1));
            bezierPath.LineTo (new CGPoint(element.X2, element.Y2));
            line.Path = bezierPath.ToCGPath();

            if (!string.IsNullOrEmpty(element.Stroke))
                line.StrokeColor = XExtensions.ConvertToNSColor(element.Stroke).CGColor;

            if (!string.IsNullOrEmpty(element.Fill))
                line.FillColor = XExtensions.ConvertToNSColor(element.Fill).CGColor;

            line.LineWidth = element.StrokeWidth;

            var width = Math.Max(element.X1, element.X2) - Math.Min(element.X1, element.X2);
            var height = Math.Max(element.Y1, element.Y2) - Math.Min(element.Y1, element.Y2);
            line.Bounds = new CGRect(0, 0, width, height);

            return line;
        }

        public static CATextLayer ToShape(this TextPath element)
        {
            var text = new CATextLayer();
            text.FontSize = element.FontSize;
            return text;
        }

        public static CAShapeLayer ToShape(this Microsoft.Mac.Svg.Path element)
        {
            var shape = new CAShapeLayer();
         
            if (!string.IsNullOrEmpty(element.d))
                shape.Path = PathBuilder.Build(element.d);

            if (!string.IsNullOrEmpty(element.Stroke))
                shape.StrokeColor = XExtensions.ConvertToNSColor(element.Stroke).CGColor;

            if (!string.IsNullOrEmpty(element.Fill))
                shape.FillColor = XExtensions.ConvertToNSColor(element.Fill).CGColor;
            
            shape.LineWidth = element.StrokeWidth * 2;

            return shape;
        }

        public static CAShapeLayer ToShape(this CirclePath element)
        {
            var shape = new CAShapeLayer();
            var bezierPath = NSBezierPath.FromOvalInRect(new CGRect(0, 0, element.Radio * 2, element.Radio * 2));
            shape.Path = bezierPath.ToCGPath();

            if (!string.IsNullOrEmpty(element.Stroke))
                shape.StrokeColor = XExtensions.ConvertToNSColor(element.Stroke).CGColor;

            shape.LineWidth = element.StrokeWidth;

            if (!string.IsNullOrEmpty(element.Fill))
                shape.FillColor = XExtensions.ConvertToNSColor(element.Fill).CGColor;

            var diameter = element.Radio * 2;
            shape.Bounds = new CGRect(0, 0, diameter, diameter);

            return shape;
        }

        //TODO: we should move this to a shared place
        public static CGPath ToCGPath(this NSBezierPath path)
        {
            var numElements = path.ElementCount;
            if (numElements == 0)
            {
                return null;
            }

            CGPath result = new CGPath();
            bool didClosePath = true;


            for (int i = 0; i < numElements; i++)
            {
                CGPoint[] points;
                var element = path.ElementAt(i, out points);
                if (element == NSBezierPathElement.MoveTo)
                {
                    result.MoveToPoint(points[0].X, points[0].Y);
                }
                else if (element == NSBezierPathElement.LineTo)
                {
                    result.AddLineToPoint(points[0].X, points[0].Y);
                    didClosePath = false;

                }
                else if (element == NSBezierPathElement.CurveTo)
                {
                    result.AddCurveToPoint(points[0].X, points[0].Y,
                                            points[1].X, points[1].Y,
                                            points[2].X, points[2].Y);
                    didClosePath = false;
                }
                else if (element == NSBezierPathElement.ClosePath)
                {
                    result.CloseSubpath();
                }
            }

            // Be sure the path is closed or Quartz may not do valid hit detection.
            if (!didClosePath)
            {
                result.CloseSubpath();
            }
            return result;
        }
    }
}
