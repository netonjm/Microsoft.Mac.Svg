# Microsoft.Mac.Svg


This repository contains a library with tool to render in Microsoft.Mac UI toolkit Svg documents. Under the hood this is not using SkiaSharp, it generates native NSLayers embeded into a custom NSView, so you can access to it and modify as your wish

For now not all the .svg files are going to work, this document type is complex to render natively, and it could contain links or style tags which makes things a little more complex, but for simple scenarios works very nicely.. I will continue implementing more features in the future.

I included an example proyect to try the library, just copy the code in the source editor or drag the file into the .pdf viewer.

![image](https://user-images.githubusercontent.com/1587480/182373991-93c87719-87bf-4e3b-8c64-36c4eed3d91b.png)

![image](https://user-images.githubusercontent.com/1587480/182374009-93e66f40-b12d-4edb-845b-92020dc34911.png)

![image](https://user-images.githubusercontent.com/1587480/182374025-afeafe99-bb06-4f2e-9455-36ea7d4eeb1c.png)

![image](https://user-images.githubusercontent.com/1587480/182374033-f8cc97ca-2c3a-41cb-aa08-a804595ed005.png)


# Implemented:
Paths
Lines
Fonts
Circles
Rectangles
Group properties inherithing

# TODO:
Styles
Urls and embeded images
Shadows
Ovals
More properties (fill-rule, clip-rule ...)
What to expect:
Perfect to import simple draws for icons from Figma
Complex scenarios are out of scope for now
How to use with Figma
CleanShot 2022-07-30 at 18 38 25

# Nuget
No nuget yet but it will be published soon

Thank you!
