using System.Reflection;
using System.Resources;


namespace Gravicode.Bos.UnitTests
{
    internal static partial class CoreStrings
    {
      

        private static global::System.Resources.ResourceManager s_resourceManager;
     
#if !NET20
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
        internal static string GetResourceString(string resourceKey, string defaultValue = null) => ResourceManager.GetString(resourceKey, Culture);

        private static string GetResourceString(string resourceKey, string[] formatterNames)
        {
            var value = GetResourceString(resourceKey);
            if (formatterNames != null)
            {
                for (var i = 0; i < formatterNames.Length; i++)
                {
                    value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
                }
            }
            return value;
        }

        /// <summary>Expected scalar value for key: "{key}".</summary>
        internal static string FormatExpectedYamlScalar(object key)
           => string.Format(Culture, GetResourceString("ExpectedYamlScalar", new[] { "key" }), key);

        /// <summary>Expected yaml sequence for key: "{key}".</summary>
        internal static string FormatExpectedYamlSequence(object key)
           => string.Format(Culture, GetResourceString("ExpectedYamlSequence", new[] { "key" }), key);

        /// <summary>Cannot have multiple {0} bindings without names. Please specify names for each {0} binding.</summary>
        internal static string FormatMultipleBindingWithoutName(object p0)
           => string.Format(Culture, GetResourceString("MultipleBindingWithoutName"), p0);

        /// <summary>Cannot have multiple {0} bindings with the same name.</summary>
        internal static string FormatMultipleBindingWithSameName(object p0)
           => string.Format(Culture, GetResourceString("MultipleBindingWithSameName"), p0);

        /// <summary>Cannot have multiple {0} bindings with the same port.</summary>
        internal static string FormatMultipleBindingWithSamePort(object p0)
           => string.Format(Culture, GetResourceString("MultipleBindingWithSamePort"), p0);

        /// <summary>A prober must be configured for the {0} probe.</summary>
        internal static string FormatProberRequired(object p0)
           => string.Format(Culture, GetResourceString("ProberRequired"), p0);

        /// <summary>"successThreshold" for {0} probe must be set to "1".</summary>
        internal static string FormatSuccessThresholdMustBeOne(object p0)
           => string.Format(Culture, GetResourceString("SuccessThresholdMustBeOne"), p0);

        /// <summary>"{value}" must be a boolean value (true/false).</summary>
        internal static string FormatMustBeABoolean(object value)
           => string.Format(Culture, GetResourceString("MustBeABoolean", new[] { "value" }), value);
        /// <summary>"{value}" value must be an integer.</summary>
        internal static string FormatMustBeAnInteger(object value)
           => string.Format(Culture, GetResourceString("MustBeAnInteger", new[] { "value" }), value);

        /// <summary>"{value}" value must be an IP address, "*" or "localhost".</summary>
        internal static string FormatMustBeAnIPAddress(object value)
           => string.Format(Culture, GetResourceString("MustBeAnIPAddress", new[] { "value" }), value);

        /// <summary>"{value}" value cannot be negative.</summary>
        internal static string FormatMustBePositive(object value)
           => string.Format(Culture, GetResourceString("MustBePositive", new[] { "value" }), value);

        /// <summary>"{value}" value must be greater than zero.</summary>
        internal static string FormatMustBeGreaterThanZero(object value)
           => string.Format(Culture, GetResourceString("MustBeGreaterThanZero", new[] { "value" }), value);

        /// <summary>Cannot have both "{0}" and "{1}" set for a service. Only one of project, image, and executable can be set for a given service.</summary>
        internal static string FormatProjectImageExecutableExclusive(object p0, object p1)
           => string.Format(Culture, GetResourceString("ProjectImageExecutableExclusive"), p0, p1);

        /// <summary>Unexpected node type in the bos configuration file. Expected "{expected}" but got "{actual}".</summary>
        internal static string FormatUnexpectedType(object expected, object actual)
           => string.Format(Culture, GetResourceString("UnexpectedType", new[] { "expected", "actual" }), expected, actual);

        /// <summary>Unexpected key "{key}" in the bos configuration file.</summary>
        internal static string FormatUnrecognizedKey(object key)
           => string.Format(Culture, GetResourceString("UnrecognizedKey", new[] { "key" }), key);

        /// <summary>Unexpected node type in the bos configuration file. Expected one of ({expected}) but got "{actual}".</summary>
        internal static string FormatUnexpectedTypes(object expected, object actual)
           => string.Format(Culture, GetResourceString("UnexpectedTypes", new[] { "expected", "actual" }), expected, actual);

        /// <summary>Path "{path}" was not found.</summary>
        internal static string FormatPathNotFound(object path)
           => string.Format(Culture, GetResourceString("PathNotFound", new[] { "path" }), path);

        /// <summary>Expected a value for environment variable "{key}".</summary>
        internal static string FormatExpectedEnvironmentVariableValue(object key)
           => string.Format(Culture, GetResourceString("ExpectedEnvironmentVariableValue", new[] { "key" }), key);


    }
}
