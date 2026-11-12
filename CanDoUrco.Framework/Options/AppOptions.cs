// CanDoUrco.Framework - A framework for building applications with rendering support.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Drawing;

namespace CanDoUrco.Framework.Options;

public record AppOptions
{
    public string AppName { get; set; } = "CanDoUrco Application";

    public string AppID { get; set; } = "com.github.vitimiti.candourco.application";

    public string AppVersion { get; set; } = "0.1.0";

    public string WindowTitle { get; set; } = "CanDoUrco Application";

    public Size WindowSize { get; set; } = new Size(800, 600);

    public RendererType Renderer { get; set; } = RendererType.Software;

    public bool Fullscreen { get; set; } = false;

    public bool Resizable { get; set; } = true;
}
