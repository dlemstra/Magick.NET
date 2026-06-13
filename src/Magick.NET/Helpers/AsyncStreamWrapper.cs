// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick;

internal class AsyncStreamWrapper : StreamWrapperBase
{
    private readonly Stream _stream;
    private readonly SemaphoreSlim _performRead = new(0, 1);
    private readonly SemaphoreSlim _readDone = new(0, 1);
    private readonly SemaphoreSlim _performWrite = new(0, 1);
    private readonly SemaphoreSlim _writeDone = new(0, 1);
    private int _readCount = 0;
    private int _writeCount = 0;
    private bool _exceptionThrown;

    public AsyncStreamWrapper(Stream stream)
        : base(stream)
    {
        _stream = stream;
    }

    public static AsyncStreamWrapper CreateForReading(Stream stream)
    {
        Throw.IfFalse(stream.CanRead, nameof(stream), "The stream should be readable.");

        return new(stream);
    }

    public static AsyncStreamWrapper CreateForWriting(Stream stream)
    {
        Throw.IfFalse(stream.CanWrite, nameof(stream), "The stream should be writable.");

        return new(stream);
    }

    public async Task ReadAsync(Action action, CancellationToken cancellationToken)
    {
        var actionTask = Task.Run(
            () =>
            {
                try
                {
                    action();
                }
                finally
                {
                    _readCount = -1;
                    _performRead.Release();
                }
            },
            CancellationToken.None);
        var readTask = ReadAsync(cancellationToken);

        await Task.WhenAll(actionTask, readTask).ConfigureAwait(false);

        if (_exceptionThrown)
            cancellationToken.ThrowIfCancellationRequested();
    }

    public async Task WriteAsync(Action action, CancellationToken cancellationToken)
    {
        var actionTask = Task.Run(
            () =>
            {
                try
                {
                    action();
                }
                finally
                {
                    _readCount = -1;
                    _performRead.Release();
                    _writeCount = -1;
                    _performWrite.Release();
                }
            },
            CancellationToken.None);
        var readTask = ReadAsync(cancellationToken);
        var writeTask = WriteAsync(cancellationToken);

        await Task.WhenAll(actionTask, readTask, writeTask).ConfigureAwait(false);

        if (_exceptionThrown)
            cancellationToken.ThrowIfCancellationRequested();
    }

    protected override int Read(int count)
    {
        if (_exceptionThrown)
            return -1;

        _readCount = count;
        _performRead.Release();
        _readDone.Wait();

        if (_exceptionThrown)
            return -1;

        return _readCount;
    }

    protected override long Seek(long offset, SeekOrigin origin)
        => _stream.Seek(offset, origin);

    protected override long Tell()
        => _stream.Position;

    protected override bool Write(int count)
    {
        if (_exceptionThrown)
            return false;

        _writeCount = count;
        _performWrite.Release();
        _writeDone.Wait();

        if (_exceptionThrown)
            return false;

        return true;
    }

    private async Task ReadAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            try
            {
                await _performRead.WaitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                _exceptionThrown = true;
                if (_performRead.Wait(0))
                    _readDone.Release();
                return;
            }

            if (_readCount == -1)
                return;

            try
            {
                _readCount = await _stream.ReadAsync(Data, 0, _readCount, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                _exceptionThrown = true;
            }
            finally
            {
                _readDone.Release();
            }
        }
    }

    private async Task WriteAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            try
            {
                await _performWrite.WaitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                _exceptionThrown = true;
                if (_performWrite.Wait(0))
                    _writeDone.Release();
                return;
            }

            if (_writeCount == -1)
                return;

            try
            {
                await _stream.WriteAsync(Data, 0, _writeCount, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                _exceptionThrown = true;
                return;
            }
            finally
            {
                _writeDone.Release();
            }
        }
    }
}
