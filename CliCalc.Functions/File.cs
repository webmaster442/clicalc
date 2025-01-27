// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

namespace CliCalc.Functions;

/// <summary>
/// Represents a file on the file system.
/// </summary>
public sealed class File
{
    private readonly string _path;

    /// <summary>
    /// Initializes a new instance of the File class.
    /// </summary>
    /// <param name="path">A path on the file system</param>
    public File(string path)
    {
        _path = path;
    }

    /// <summary>
    /// Retunrs true if the file exists on the file system.
    /// </summary>
    public bool Exists
        => System.IO.File.Exists(_path);

    /// <summary>
    /// Opens the file for reading.
    /// </summary>
    /// <returns>A FileStream</returns>
    public FileStream OpenRead()
        => System.IO.File.OpenRead(_path);
}
