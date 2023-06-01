### DirectXTexNetImpl

This library comes from the following repository. All credit goes to its author and contributors.  
https://github.com/deng0/DirectXTexNet

This DLL is included so that it can be excluded from assembly trimming via:
```msbuild
<TrimmerRootAssembly Include="DirectXTexNetImplDummy" />
```

It has been renamed to DirectXTexNetImplDummy to prevent conflicts with the assembly from the NuGet package.

While there may be a better way to handle this, this was the only approach I could come up with that worked.

---

### DirectXTexNet License

*Retrieved from https://github.com/deng0/DirectXTexNet/blob/master/LICENSE on 1 June 2023*

```
MIT License

New DirectXTexNet
Copyright (c) 2021 Dennis Gocke

Original DirectXTexNet
Copyright (c) 2016 Simon Taylor

DirectXTex
Copyright (c) 2011-2021 Microsoft Corp

Permission is hereby granted, free of charge, to any person obtaining a copy of this
software and associated documentation files (the "Software"), to deal in the Software
without restriction, including without limitation the rights to use, copy, modify,
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be included in all copies
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```