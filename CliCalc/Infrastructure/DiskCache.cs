using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace CliCalc.Infrastructure;

internal sealed class DiskCache : IDisposable
{
    internal sealed record CacheKey(string Key, DateTime ValidTill);

    private readonly ConcurrentDictionary<CacheKey, string> _data;
    private readonly JsonSerializerOptions _options;
    private readonly System.Timers.Timer _timer;
    private readonly string _filePath;
    private bool _isDirty;

    public DiskCache(string name)
    {
        _timer = new System.Timers.Timer(TimeSpan.FromSeconds(2));
        _timer.Elapsed += OnTimerElapsed;
        _timer.Disposed += OnTimerDispose;
        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };
        _data = new ConcurrentDictionary<CacheKey, string>();
        _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), name);
        if (File.Exists(_filePath))
        {
            Load();
        }
        _timer.Start();
    }

    private void OnTimerDispose(object? sender, EventArgs e) => Save();

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e) => Save();

    private void Load()
    {
        using var stream = File.OpenRead(_filePath);
        var data = JsonSerializer.Deserialize<KeyValuePair<CacheKey, string>[]>(stream, _options);
        if (data != null)
        {
            foreach (var item in data)
            {
                if (item.Key.ValidTill > DateTime.Now)
                {
                    _data.TryAdd(item.Key, item.Value);
                }
                else
                {
                    _isDirty = true;
                }
            }
        }
        else
        {
            _isDirty = true;
        }
    }

    private void Save()
    {
        if (!_isDirty) return;
        var contents = _data.ToArray();
        using var stream = File.Create(_filePath);
        JsonSerializer.Serialize(stream, contents, _options);
    }

    public void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
    }
}
