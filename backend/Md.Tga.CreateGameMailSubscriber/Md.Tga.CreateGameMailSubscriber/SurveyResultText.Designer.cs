﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Md.Tga.CreateGameMailSubscriber {
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
    public class SurveyResultText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SurveyResultText() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Md.Tga.CreateGameMailSubscriber.SurveyResultText", typeof(SurveyResultText).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html lang=&quot;de&quot;&gt;
        ///    &lt;head&gt;
        ///        &lt;meta charset=&quot;utf-8&quot;&gt;
        ///        &lt;meta name=&quot;viewport&quot; content=&quot;width=device-width&quot; initial-scale=&quot;1&quot;&gt;
        ///        &lt;meta name=&quot;google&quot; content=&quot;notranslate&quot;&gt;
        ///    &lt;/head&gt;
        ///    &lt;body&gt;
        ///        &lt;h1&gt;Hej {0}!&lt;/h1&gt;
        ///        &lt;p&gt;Die Seiten wurden ausgewählt und es kann endlich wieder losgehen.&lt;/p&gt;
        ///        &lt;h2&gt;Wer spielt was?&lt;/h2&gt;
        ///           &lt;ul&gt;{1}&lt;/ul&gt;
        ///        &lt;p&gt;Viele Grüße,&lt;br&gt;&lt;br&gt;{2}&lt;/p&gt;
        ///    &lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        public static string BodyHtml {
            get {
                return ResourceManager.GetString("BodyHtml", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;li&gt;{0}: {1}&lt;/li&gt;.
        /// </summary>
        public static string BodyHtmlEntry {
            get {
                return ResourceManager.GetString("BodyHtmlEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hej {0},
        ///
        ///die Seiten wurden ausgewählt und es kann endlich wieder losgehen.
        ///
        ///Wer spielt was?
        ///
        ///{1}
        ///
        ///Viele Grüße,
        ///
        ///{2}.
        /// </summary>
        public static string BodyText {
            get {
                return ResourceManager.GetString("BodyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to      - {0}: {1}
        ///.
        /// </summary>
        public static string BodyTextEntry {
            get {
                return ResourceManager.GetString("BodyTextEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Feuer frei für {0}.
        /// </summary>
        public static string Subject {
            get {
                return ResourceManager.GetString("Subject", resourceCulture);
            }
        }
    }
}