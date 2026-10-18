// A common framework for the projects and engines of Can Do Urco
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using CanDoUrco.Framework.Native.SDL3;
using CanDoUrco.Framework.Options;
using CanDoUrco.Framework.Time;
using CanDoUrco.Framework.Video;
using CanDoUrco.NativeLibraryLoaderConfigurer;

namespace CanDoUrco.Framework;

public sealed class Game : IDisposable
{
    public event Action? Initialized;
    public event Action<GameTime>? UpdateStart;
    public event Action<GameTime>? UpdateEnd;
    public event Action<GameTime>? DrawStart;
    public event Action<GameTime>? DrawEnd;
    public event Action<GameTime>? Quit;

    private readonly GameTime _gameTime = new();

    private bool _running = true;
    private bool _disposedValue;

    public Window Window { get; private set; }

    public unsafe Game(Action<GameOptions>? options = null)
    {
        CommonNativeLibraryLoader.Set();
        var gameOptions = new GameOptions();
        options?.Invoke(gameOptions);

        _gameTime.TargetFramesPerSecond = gameOptions.TimeOptions.FrameRate;

        try
        {
            if (!Sdl.Init(Sdl.InitVideo))
            {
                throw new InvalidOperationException($"Failed to initialize SDL: {Sdl.GetError()}.");
            }

            Window = new Window(gameOptions.WindowOptions);

            Initialized?.Invoke();
        }
        finally
        {
            GC.KeepAlive(this);
        }
    }

    public void Dispose()
    {
        if (_disposedValue)
        {
            return;
        }

        Window.Dispose();
        _disposedValue = true;
    }

    public void Run()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);

        _gameTime.Start();
        while (_running)
        {
            Update(_gameTime);
            Draw(_gameTime);

            ProcessEvents();
            _gameTime.Update();
        }
    }

    private void Update(GameTime gameTime)
    {
        UpdateStart?.Invoke(gameTime);
        UpdateEnd?.Invoke(gameTime);
    }

    private void Draw(GameTime gameTime)
    {
        DrawStart?.Invoke(gameTime);

        Window.StartFrame();
        Window.EndFrame();

        DrawEnd?.Invoke(gameTime);
    }

    private void ProcessEvents()
    {
        while (Sdl.PollEvent(out var ev))
        {
            switch (ev.Type)
            {
                case Sdl.EventType.Quit:
                    _running = false;
                    Quit?.Invoke(_gameTime);
                    break;
                default:
                    break;
            }
        }
    }
}
