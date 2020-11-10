using System;
using Cables;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class CableTwistSchemePainter
    {
        private readonly SKCanvasView canvasView;
        private float singleElementRadius;
        private double R; // Радиус окружности для каждого повива, на которой лежат центры каждого элемента в повиве.
        private float centerX;
        private float centerY;
        private readonly SKPaint circleFill;
        private readonly float defaultRadius;
        private readonly float edgeOffset;
        private readonly SKColor lightBlueSK;
        private readonly SKColor darkBlueSK;
        public Color BackgroundColor { get; set; }
        public TwistInfo CurrentTwistInfo { get; set; }
        public View CanvasView { get => canvasView; }

        public CableTwistSchemePainter()
        {
            canvasView = new SKCanvasView();
            defaultRadius = 70;
            edgeOffset = 30;
            circleFill = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
            };
            lightBlueSK = SKColor.Parse("#8a88f8");
            darkBlueSK = Color.DarkBlue.ToSKColor();
            canvasView.PaintSurface += CanvasView_PaintSurface;
        }

        public void DrawTwistScheme(TwistInfo currentTwistInfo)
        {
            CurrentTwistInfo = currentTwistInfo;
            canvasView.InvalidateSurface();
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var layersElementsCount = CurrentTwistInfo.LayersElementsCount;
            // получаем текущую поверхность из аргументов
            var surface = e.Surface;
            // Получаем холст на котором будет рисовать
            var canvas = surface.Canvas;
            var minSize = Math.Min(canvas.LocalClipBounds.Width, canvas.LocalClipBounds.Height);
            var sizableRadius = (minSize - edgeOffset) / (2 * CurrentTwistInfo.TwistCoefficient);
            singleElementRadius = (float)(sizableRadius > defaultRadius ? defaultRadius : sizableRadius);
            centerX = canvas.LocalClipBounds.MidX;
            centerY = canvas.LocalClipBounds.MidY;

            // Очищаем холст
            canvas.Clear(BackgroundColor.ToSKColor());
            var firstR = 0d;
            circleFill.Color = layersElementsCount.Length % 2 == 0 ? lightBlueSK : darkBlueSK;
            for (int i = 0; i < layersElementsCount.Length; i++)
            {
                R = GetR(layersElementsCount[i]);
                if (i > 0 && R < firstR + 2 * singleElementRadius) R = firstR + 2 * singleElementRadius;
                var centerPoint = GetCenterPoint(layersElementsCount[i]);
                for (int j = 0; j < layersElementsCount[i]; j++)
                {
                    canvas.DrawCircle(centerPoint, singleElementRadius, circleFill);
                    canvas.RotateDegrees(360f / layersElementsCount[i], centerX, centerY);
                }
                firstR = R;
                circleFill.Color = circleFill.Color == darkBlueSK ? lightBlueSK : darkBlueSK;
            }
        }
        private double GetR(int countElements)
        {
            var alpha = Math.PI / countElements;
            var beta = Math.PI * (1 - 1 / countElements) / 2;
            return singleElementRadius * Math.Sin(beta) / Math.Sin(alpha);
        }

        private SKPoint GetCenterPoint(int countElements)
        {
            switch (countElements)
            {
                case 1: return new SKPoint(centerX, centerY);
                case 2: return new SKPoint(centerX - singleElementRadius, centerY);
                case 4: return new SKPoint(centerX - singleElementRadius, centerY - singleElementRadius);
                default: return new SKPoint(centerX, centerY - (float)R);
            }
        }
    }
}
