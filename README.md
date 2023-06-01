# Normal Map Mirror

This is a small tool that provides the ability to mirror the 3D information of normal map textures.

Mirroring the texture in image editing software will not work correctly, as all this does is reverses the order of the 3D vectors encoded in the texture. This tool works by translating the pixel information back into 3D vectors, mirroring the vectors on the chosen axis, then converting the new vectors back to pixel information.  
&nbsp;

|Original|Mirrored on X|Mirrored on Y|Mirrored on Z|
|---|---|---|---|
|![original](https://github.com/Kizari/NormalMapMirror/assets/25322543/7a08fbd7-ee30-49b7-8f2a-067ffde85eeb)|![xmirror](https://github.com/Kizari/NormalMapMirror/assets/25322543/39062d5b-7b57-4b6f-a902-43923dc7eb17)|![ymirror](https://github.com/Kizari/NormalMapMirror/assets/25322543/abe45e4e-dc6a-43b8-8e28-b13cc42f8e0b)|![zmirror](https://github.com/Kizari/NormalMapMirror/assets/25322543/672dc262-6674-416d-a740-5dbd78281e40)|

&nbsp;

### Texture Support

This tool supports png, tga, dds, tif, tiff, jpg, jpeg, gif, bmp, wmp, and ico.  
&nbsp;

### Basic Usage

Download the tool on the [releases](https://github.com/Kizari/NormalMapMirror/releases/latest) page and extract the downloaded zip anywhere you like.

To mirror the normals on a texture, drag the image file and drop it onto `NormalMapMirror.exe`.

A file will be created in the same location as the original file for each axis. For example, if you drop `normal_map.png` onto the executable, you will get:
* `normal_map_XMirror.png` (this is the original texture with vector information mirrored across the X axis)
* `normal_map_YMirror.png` (this is the original texture with vector information mirrored across the Y axis)
* `normal_map_ZMirror.png` (this is the original texture with vector information mirrored across the Z axis)  
&nbsp;

### Configuration

If you want to change which files are generated, you can alter `config.ini`.

```
[Outputs]
XMirror = true
YMirror = true
ZMirror = true
```

The default configuration is shown above. For example, if you only want a copy of the image with the vectors mirrored along the X axis, you would set the other two axes to false:
```
[Outputs]
XMirror = true
YMirror = false
ZMirror = false
```

Save the file once you have made your changes. From now on, when you drop files onto the executable, only the XMirror variant will be generated.
