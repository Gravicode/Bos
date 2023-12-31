﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gravicode.Bos.Core {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal static partial class CoreStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        //internal static CoreStrings() {
        //}
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gravicode.Bos.Core.CoreStrings", typeof(CoreStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected a value for environment variable &quot;{key}&quot;..
        /// </summary>
        internal static string ExpectedEnvironmentVariableValue {
            get {
                return ResourceManager.GetString("ExpectedEnvironmentVariableValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected scalar value for key: &quot;{key}&quot;..
        /// </summary>
        internal static string ExpectedYamlScalar {
            get {
                return ResourceManager.GetString("ExpectedYamlScalar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected yaml sequence for key: &quot;{key}&quot;..
        /// </summary>
        internal static string ExpectedYamlSequence {
            get {
                return ResourceManager.GetString("ExpectedYamlSequence", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configuration validation failed. Extensions must provide a name..
        /// </summary>
        internal static string ExtensionMustProvideAName {
            get {
                return ResourceManager.GetString("ExtensionMustProvideAName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ingress bindings must be http or https..
        /// </summary>
        internal static string IngressBindingMustBeHttpOrHttps {
            get {
                return ResourceManager.GetString("IngressBindingMustBeHttpOrHttps", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ingress rules references a service that does not exist..
        /// </summary>
        internal static string IngressRuleMustReferenceService {
            get {
                return ResourceManager.GetString("IngressRuleMustReferenceService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot have multiple {0} bindings without names. Please specify names for each {0} binding..
        /// </summary>
        internal static string MultipleBindingWithoutName {
            get {
                return ResourceManager.GetString("MultipleBindingWithoutName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot have multiple {0} bindings with the same name..
        /// </summary>
        internal static string MultipleBindingWithSameName {
            get {
                return ResourceManager.GetString("MultipleBindingWithSameName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot have multiple {0} bindings with the same port..
        /// </summary>
        internal static string MultipleBindingWithSamePort {
            get {
                return ResourceManager.GetString("MultipleBindingWithSamePort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{value}&quot; must be a boolean value (true/false)..
        /// </summary>
        internal static string MustBeABoolean {
            get {
                return ResourceManager.GetString("MustBeABoolean", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{value}&quot; value must be an integer..
        /// </summary>
        internal static string MustBeAnInteger {
            get {
                return ResourceManager.GetString("MustBeAnInteger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{value}&quot; value must be an IP address, &quot;*&quot; or &quot;localhost&quot;..
        /// </summary>
        internal static string MustBeAnIPAddress {
            get {
                return ResourceManager.GetString("MustBeAnIPAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{value}&quot; value must be greater than zero..
        /// </summary>
        internal static string MustBeGreaterThanZero {
            get {
                return ResourceManager.GetString("MustBeGreaterThanZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;{value}&quot; value cannot be negative..
        /// </summary>
        internal static string MustBePositive {
            get {
                return ResourceManager.GetString("MustBePositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Path &quot;{path}&quot; was not found..
        /// </summary>
        internal static string PathNotFound {
            get {
                return ResourceManager.GetString("PathNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A prober must be configured for the {0} probe..
        /// </summary>
        internal static string ProberRequired {
            get {
                return ResourceManager.GetString("ProberRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot have both &quot;{0}&quot; and &quot;{1}&quot; set for a service. Only one of project, image, and executable can be set for a given service..
        /// </summary>
        internal static string ProjectImageExecutableExclusive {
            get {
                return ResourceManager.GetString("ProjectImageExecutableExclusive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Services must have unique names..
        /// </summary>
        internal static string ServiceMustHaveUniqueNames {
            get {
                return ResourceManager.GetString("ServiceMustHaveUniqueNames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;successThreshold&quot; for {0} probe must be set to &quot;1&quot;..
        /// </summary>
        internal static string SuccessThresholdMustBeOne {
            get {
                return ResourceManager.GetString("SuccessThresholdMustBeOne", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected node type in the bos configuration file. Expected &quot;{expected}&quot; but got &quot;{actual}&quot;..
        /// </summary>
        internal static string UnexpectedType {
            get {
                return ResourceManager.GetString("UnexpectedType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected node type in the bos configuration file. Expected one of ({expected}) but got &quot;{actual}&quot;..
        /// </summary>
        internal static string UnexpectedTypes {
            get {
                return ResourceManager.GetString("UnexpectedTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected key &quot;{key}&quot; in the bos configuration file..
        /// </summary>
        internal static string UnrecognizedKey {
            get {
                return ResourceManager.GetString("UnrecognizedKey", resourceCulture);
            }
        }
    }
}