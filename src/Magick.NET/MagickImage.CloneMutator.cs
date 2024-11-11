// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.Drawing;

namespace ImageMagick;

/// <content />
public partial class MagickImage
{
    private class CloneMutator : IMagickImageCloneMutator, IDisposable
    {
        private IntPtr _result = IntPtr.Zero;

        public CloneMutator(NativeMagickImage nativeMagickImage)
            => NativeMagickImage = nativeMagickImage;

        protected NativeMagickImage NativeMagickImage { get; }

        public void Dispose()
        {
            if (_result != IntPtr.Zero)
                NativeMagickImage.DisposeInstance(_result);
        }

        public IntPtr GetResult()
        {
            var result = _result;
            _result = IntPtr.Zero;
            return result;
        }

        public void AdaptiveBlur()
            => AdaptiveBlur(0.0, 1.0);

        public void AdaptiveBlur(double radius)
            => AdaptiveBlur(radius, 1.0);

        public void AdaptiveBlur(double radius, double sigma)
            => SetResult(NativeMagickImage.AdaptiveBlur(radius, sigma));

        public void AdaptiveResize(uint width, uint height)
            => AdaptiveResize(new MagickGeometry(width, height));

        public void AdaptiveResize(IMagickGeometry geometry)
        {
            Throw.IfNull(nameof(geometry), geometry);

            SetResult(NativeMagickImage.AdaptiveResize(geometry.ToString()));
        }

        public void AdaptiveSharpen()
            => AdaptiveSharpen(0.0, 1.0);

        public void AdaptiveSharpen(Channels channels)
            => AdaptiveSharpen(0.0, 1.0, channels);

        public void AdaptiveSharpen(double radius, double sigma)
            => AdaptiveSharpen(radius, sigma, ImageMagick.Channels.Undefined);

        public void AdaptiveSharpen(double radius, double sigma, Channels channels)
            => SetResult(NativeMagickImage.AdaptiveSharpen(radius, sigma, channels));

        public void AdaptiveThreshold(uint width, uint height)
            => AdaptiveThreshold(width, height, 0.0, ImageMagick.Channels.Undefined);

        public void AdaptiveThreshold(uint width, uint height, Channels channels)
            => AdaptiveThreshold(width, height, 0.0, channels);

        public void AdaptiveThreshold(uint width, uint height, double bias)
            => AdaptiveThreshold(width, height, bias, ImageMagick.Channels.Undefined);

        public void AdaptiveThreshold(uint width, uint height, double bias, Channels channels)
            => SetResult(NativeMagickImage.AdaptiveThreshold(width, height, bias, channels));

        public void AdaptiveThreshold(uint width, uint height, Percentage biasPercentage)
            => AdaptiveThreshold(width, height, biasPercentage, ImageMagick.Channels.Undefined);

        public void AdaptiveThreshold(uint width, uint height, Percentage biasPercentage, Channels channels)
            => AdaptiveThreshold(width, height, PercentageHelper.ToQuantum(nameof(biasPercentage), biasPercentage), channels);

        public void AddNoise(NoiseType noiseType)
            => AddNoise(noiseType, ImageMagick.Channels.Undefined);

        public void AddNoise(NoiseType noiseType, Channels channels)
            => AddNoise(noiseType, 1.0, channels);

        public void AddNoise(NoiseType noiseType, double attenuate)
            => AddNoise(noiseType, attenuate, ImageMagick.Channels.Undefined);

        public void AddNoise(NoiseType noiseType, double attenuate, Channels channels)
            => SetResult(NativeMagickImage.AddNoise(noiseType, attenuate, channels));

        public void AffineTransform(IDrawableAffine affineMatrix)
        {
            Throw.IfNull(nameof(affineMatrix), affineMatrix);

            SetResult(NativeMagickImage.AffineTransform(affineMatrix.ScaleX, affineMatrix.ScaleY, affineMatrix.ShearX, affineMatrix.ShearY, affineMatrix.TranslateX, affineMatrix.TranslateY));
        }

        public void AutoOrient()
            => SetResult(NativeMagickImage.AutoOrient());

        public void BilateralBlur(uint width, uint height)
        {
            var intensitySigma = Math.Sqrt((width * width) + (height * height));
            BilateralBlur(width, height, intensitySigma, intensitySigma * 0.25);
        }

        public void BilateralBlur(uint width, uint height, double intensitySigma, double spatialSigma)
            => SetResult(NativeMagickImage.BilateralBlur(width, height, intensitySigma, spatialSigma));

        public void BlueShift()
            => BlueShift(1.5);

        public void BlueShift(double factor)
            => SetResult(NativeMagickImage.BlueShift(factor));

        public void Blur()
            => Blur(0.0, 1.0);

        public void Blur(Channels channels)
            => Blur(0.0, 1.0, channels);

        public void Blur(double radius, double sigma)
            => Blur(radius, sigma, ImageMagick.Channels.Undefined);

        public void Blur(double radius, double sigma, Channels channels)
            => SetResult(NativeMagickImage.Blur(radius, sigma, channels));

        public void Resize(uint width, uint height)
            => Resize(new MagickGeometry(width, height));

        public void Resize(IMagickGeometry geometry)
        {
            Throw.IfNull(nameof(geometry), geometry);

            SetResult(NativeMagickImage.Resize(geometry.ToString()));
        }

        public void Resize(Percentage percentage)
            => Resize(new MagickGeometry(percentage, percentage));

        public void Resize(Percentage percentageWidth, Percentage percentageHeight)
            => Resize(new MagickGeometry(percentageWidth, percentageHeight));

        protected virtual void SetResult(IntPtr result)
        {
            if (_result != IntPtr.Zero)
                throw new InvalidOperationException("Only a single operation can be executed.");

            _result = result;
        }
    }
}
