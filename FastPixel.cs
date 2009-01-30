using System;
using System.Drawing;
using System.Drawing.Imaging;

class FastPixel {
    private BitmapData bmpData;
    private bool locked = false;

    private bool _isAlpha = false;
    private Bitmap _bitmap;
    private int _width;
    private int _height;
    private int bytesPerPixel;

    public int Width {
        get { return this._width; }
    }

    public int Height {
        get { return this._height; }
    }

    public bool IsAlphaBitmap {
        get { return this._isAlpha; }
    }

    public Bitmap Bitmap {
        get { return this._bitmap; }
    }

    public FastPixel(Bitmap bitmap) {
        if (bitmap.PixelFormat == (bitmap.PixelFormat | PixelFormat.Indexed))
            throw new Exception("Cannot lock an Indexed image.");

        this._bitmap = bitmap;
        this._isAlpha = (this.Bitmap.PixelFormat == (this.Bitmap.PixelFormat | PixelFormat.Alpha));
        this._width = bitmap.Width;
        this._height = bitmap.Height;
        this.bytesPerPixel = (this._isAlpha)?4:3;
    }

    public void Lock() {
        if (this.locked)
            throw new Exception("Bitmap already locked.");

        Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
        this.bmpData = this.Bitmap.LockBits(rect, ImageLockMode.ReadWrite, this.Bitmap.PixelFormat);
        this.locked = true;
    }

    public void Unlock(bool setPixels) {
        if (!this.locked)
            throw new Exception("Bitmap not locked.");

        // Unlock the bits.;
        this.Bitmap.UnlockBits(bmpData);
        this.locked = false;
    }

    public unsafe void Clear(Color color) {
        if (!this.locked)
            throw new Exception("Bitmap not locked.");

        byte* ptr = (byte*)this.bmpData.Scan0;
        int offset = this.bmpData.Stride - this.bmpData.Width * this.bytesPerPixel;
        for(int y = 0; y < this.Height; y++, ptr += offset) {
            for(int x = 0; x < this.Width; x++, ptr += this.bytesPerPixel) {
                ptr[0] = color.B;
                ptr[1] = color.G;
                ptr[2] = color.R;
                if (this.bytesPerPixel == 4) ptr[3] = color.A;
            }
        }
    }

    public void SetPixel(Point location, Color colour) {
        this.SetPixel(location.X, location.Y, colour);
    }

    public unsafe void SetPixel(int x, int y, Color color) {
        if (!this.locked)
            throw new Exception("Bitmap not locked.");

        byte* ptr = (byte*)this.bmpData.Scan0;
        ptr += getMemoryOffset(x, y);        

        ptr[0] = color.B;
        ptr[1] = color.G;
        ptr[2] = color.R;
        if (this.bytesPerPixel == 4) ptr[3] = color.A;
    }

    public Color GetPixel(Point location) {
        return this.GetPixel(location.X, location.Y);
    }

    public unsafe Color GetPixel(int x, int y) {
        if (!this.locked)
            throw new Exception("Bitmap not locked.");

        byte* ptr = (byte*)this.bmpData.Scan0;
        ptr += getMemoryOffset(x, y);        

        int b = ptr[0];
        int g = ptr[1];
        int r = ptr[2];
        if (this.bytesPerPixel == 4) {
            int a = ptr[3];
            return Color.FromArgb(a, r, g, b);
        }
        return Color.FromArgb(r, g, b);
    }
    
    protected int getMemoryOffset(int x, int y) {
        // int offset = this.bmpData.Stride - this.bmpData.Width * this.bytesPerPixel;
        int result = y*this.bmpData.Stride + x*this.bytesPerPixel;
        return result;
    }
}