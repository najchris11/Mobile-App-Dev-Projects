using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Diagnostics;

namespace Graphics
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //Stopwatch stopwatch = new Stopwatch();
        //bool pageIsActive;
        //Color color = Color.Red;
        float redRatio = .5f,
            blueRatio = .5f;
        int redEntry = 0,
            blueEntry = 0;
        public MainPage()
        {
            InitializeComponent();
        }
        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    pageIsActive = true;
        //    await AnimationLoop();
        //}
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    pageIsActive = false;
        //}
        private static float PointsPerInch = Device.RuntimePlatform == "UWP" ? 96 : 160;
        private static float PPI = (float)DeviceDisplay.MainDisplayInfo.Density * PointsPerInch;
        private float PixelsToInches(float pixs)
        {
            return pixs / PPI;
        }
        private float InchesToPixels(float inches)
        {
            return inches * PPI;
        }
        void OnCanvas1ViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            SKPaint RedPaint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 3
            };

            SKPaint BluePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.Blue.ToSKColor(),
                StrokeWidth = 10
            };
            //float r = Math.Min(info.Width, info.Height) / 2.0f;
            //canvas.DrawCircle(info.Width / 2, info.Height / 2, r, paint);
            canvas.DrawRect(info.Width/5, (1-redRatio)*info.Height, info.Width/5, info.Height, RedPaint);
            canvas.DrawRect(info.Width / 1.6f, (1-blueRatio)*info.Height, info.Width / 5, info.Height, BluePaint);
        }
        //async Task AnimationLoop()
        //{
        //    stopwatch.Start();
        //    while (pageIsActive)
        //    {
        //        double t = stopwatch.Elapsed.TotalSeconds % 2;
        //        if (t < 1)
        //            color = Color.Red;
        //        else
        //            color = Color.Blue;
        //        view1.InvalidateSurface();
        //        await Task.Delay(TimeSpan.FromSeconds(1.0 / 30));
        //    }
        //    stopwatch.Stop();
        //}
        //void OnCanvas2ViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        //{
        //    SKImageInfo info = args.Info;
        //    SKSurface surface = args.Surface;
        //    SKCanvas canvas = surface.Canvas;
        //    canvas.Clear();
        //    SKPaint paint = new SKPaint
        //    {
        //        Style = SKPaintStyle.Stroke,
        //        Color = color.ToSKColor(),
        //        StrokeWidth = 3,
        //    };

        //    float r = InchesToPixels(1.0f);
        //    canvas.DrawCircle(0, 0, r, paint);
        //}
        //private void OnView2Tapped(object sender, EventArgs e)
        //{
        //    TappedEventArgs args = (TappedEventArgs)e;
        //    view2.InvalidateSurface();
        //}

        private void RedBarEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int32.TryParse(RedBarEntry.Text, out redEntry);
            
            
                redRatio = (float)redEntry / (redEntry + blueEntry);
                blueRatio = 1 - redRatio;
         
            view1.InvalidateSurface();
        }

        private void BlueBarEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int32.TryParse(BlueBarEntry.Text, out blueEntry);

            
                redRatio = (float)redEntry / (redEntry + blueEntry);
                blueRatio = 1 - redRatio;
            
            view1.InvalidateSurface();
        }
    }
}
