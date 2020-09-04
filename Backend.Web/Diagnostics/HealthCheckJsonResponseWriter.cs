using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Backend.Web.Diagnostics
{
    public static class HealthCheckJsonResponseWriter
    {
        private const string Text = "{}";

        private const string DefaultContentType = "application/json";

        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true,
            Converters =
            {
                new JsonStringEnumConverter(),
                new TimeSpanConverter()
            }
        };

        public static Task WriteHealthCheckJsonResponse(HttpContext httpContext, HealthReport report)
        {
            httpContext.Response.ContentType = DefaultContentType;
            return httpContext.Response.WriteAsync(report == null ? Text : JsonSerializer.Serialize(HealthReportModel.CreateFrom(report), JsonSerializerOptions));
        }

        private class TimeSpanConverter : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => TimeSpan.Parse(reader.GetString(), CultureInfo.InvariantCulture);

            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
        }

        private enum HealthStatusModel
        {
            Unhealthy = 0,

            Degraded = 1,

            Healthy = 2
        }

        private class HealthReportEntryModel
        {
            public IReadOnlyDictionary<string, object> Data { get; set; }

            public string Description { get; set; } = string.Empty;

            public TimeSpan Duration { get; set; }

            public string Exception { get; set; } = string.Empty;

            public HealthStatusModel StatusModel { get; set; }
        }

        private class HealthReportModel
        {
            public HealthReportModel(Dictionary<string, HealthReportEntryModel> entries, TimeSpan totalDuration)
            {
                Entries = entries;
                TotalDuration = totalDuration;
            }

            public HealthStatusModel StatusModel { get; set; }

            public TimeSpan TotalDuration { get; }

            public Dictionary<string, HealthReportEntryModel> Entries { get; }

            public static HealthReportModel CreateFrom(HealthReport report)
            {
                var uiReport = new HealthReportModel(new Dictionary<string, HealthReportEntryModel>(), report.TotalDuration)
                {
                    StatusModel = (HealthStatusModel)report.Status
                };

                foreach ((string key, HealthReportEntry value) in report.Entries)
                {
                    var entry = new HealthReportEntryModel
                    {
                        Data = value.Data,
                        Description = value.Description ?? string.Empty,
                        Duration = value.Duration,
                        StatusModel = (HealthStatusModel)value.Status
                    };

                    if (value.Exception != null)
                    {
                        string message = value.Exception?.Message ?? string.Empty;
                        entry.Exception = message;
                        entry.Description = value.Description ?? message;
                    }

                    uiReport.Entries.Add(key, entry);
                }

                return uiReport;
            }
        }
    }
}